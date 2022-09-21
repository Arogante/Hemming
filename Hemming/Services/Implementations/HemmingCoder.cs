﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hemming.Services.Interfaces;
using Hemming.Domain.Response;
namespace Hemming.Services.Implementations
{

    class HemmingCoder : ICoder<BitArray>
    {
        BitArray[] code;

        public BitArray[] Code(string text)
        {
            code= new BitArray[text.Length];
            for (int i = 0; i < text.Length; i++) {
            code[i]= CodeByte(text[i].ToString());
            }
            return code;
        }

        public IBaseResponse<string> Decode(BitArray[] code)
        {
            bool isErrors = false;//флаг были ли ошибки
            byte[] text=new byte[code.Length];
            for (int i = 0; i < code.Length; i++) {
                BaseResponse<byte> resp = DecodeByte(code[i]);
                if (resp.Description != "ok")
                    isErrors = true;
                text[i] = resp.Data;
            }
            string res = Encoding.ASCII.GetString(text);
            BaseResponse<string> finalResp= new BaseResponse<string>();
            if (isErrors)
                finalResp.Description = "обнаружены ошибки при декодировании";
            else
                finalResp.Description = "ошибок при декодировании не обнаружено";
            finalResp.Data = res;
            return finalResp;
        }
        public BitArray CodeByte(string symbol) {
            byte[] bytes = Encoding.ASCII.GetBytes(symbol);
            BitArray bits = new BitArray(bytes);//переводим в биты
            BitArray res = new BitArray(13, false);
            int controlInd = 1;
            int messageInd = 0;
            
            for (int i = 0; i < (res.Length - 1); i++) {//заполняем сообщение в информационные биты
                if (i+1==controlInd)
                {
                    controlInd *= 2;
                    continue;
                }
                res[i] = bits[messageInd];
                messageInd++;
            }
            res[7] = res[7] ^ res[8] ^ res[9] ^ res[10] ^ res[11];//считаем контрольные биты
            res[3] = res[3] ^ res[4] ^ res[5] ^ res[6] ^ res[11];
            res[1] = res[1] ^ res[2] ^ res[5] ^ res[6] ^ res[9] ^ res[10];
            res[0] = res[0] ^ res[2] ^ res[4] ^ res[6] ^ res[8] ^ res[10];
            bool control = false;
            for (int i = 0; i <(res.Length-1); i++)
                control = control ^ res[i];
            res[12] = control;
            return res;
        }
        public BaseResponse<byte> DecodeByte(BitArray code) {
            bool[] controls=new bool[5];//заного считаем контрольные биты
            controls[0]= (code[0] ^ code[2] ^ code[4] ^ code[6] ^ code[8] ^ code[10]) ;
            controls[1]= (code[1] ^ code[2] ^ code[5] ^ code[6] ^ code[9] ^ code[10]);
            controls[2]= (code[3] ^ code[4] ^ code[5] ^ code[6] ^ code[11]);
            controls[3]= (code[7] ^ code[8] ^ code[9] ^ code[10] ^ code[11]);
            bool control = false;
            for (int i = 0; i < code.Length; i++)//финальный бит четности
                control= control ^ code[i];
            controls[4] = control ;
            /*bool[] mistakes=new bool[5];//массив для несоответствий
            int controlInd = 1;
            for (int i=0; i < mistakes.Length-1; i++) {//сравниваем контрольные биты с вычисленными самостоятельно
                mistakes[i] = (controls[i] == code[controlInd-1]);
                controlInd *= 2;
            }
            mistakes[4]= controls[4] == code[code.Length-1];*/
            int missPosition = 0;
            int controlInd = 1;
            for (int i = 0; i < controls.Length - 1; i++) {//рассчитываем позицию ошибки
                if (controls[i])
                    missPosition += controlInd;
                controlInd *= 2;
            }
            BaseResponse<byte> resp=new BaseResponse<byte>();
            if (missPosition == 0 && controls[controls.Length - 1] == false)
                resp.Description = "ok";
            else if (missPosition != 0 && controls[controls.Length - 1] == true)
                resp.Description = "1 error";
            else
                resp.Description = "2 errors";

            controlInd = 1;
            BitArray symbol = new BitArray(8, false);
            int j = 0;
            for (int i = 0; i < code.Length-1; i++) {
                if (i == controlInd)
                {
                    controlInd *= 2;
                    continue;
                }
                symbol[j] = code[i];
                j++;
            }
            byte res = ConvertToByte(symbol);
            resp.Data = res;
            return resp;
            

        }

        byte ConvertToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
    }

}
