//program
using System.Collections;
using System.Text;
using Hemming.Domain.Response;
using Hemming.Services.Implementations;




HemmingCoder h = new HemmingCoder();
string text = "h";
BitArray[] arr=h.Code(text);


BaseResponse<string> r= (BaseResponse<string>)h.Decode(arr);
Console.WriteLine(r.Description);
