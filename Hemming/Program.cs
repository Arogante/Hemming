//program
using System.Collections;
using System.Text;
using Hemming.Domain.Response;
using Hemming.Services.Implementations;


BitToStringConverter converter=new BitToStringConverter();
HemmingCoder h = new HemmingCoder();


string text = "h";
BitArray[] arr=h.Code(text);

Console.WriteLine(converter.ToString(arr[0]));
BaseResponse<string> r= (BaseResponse<string>)h.Decode(arr);
Console.WriteLine(r.Description);
