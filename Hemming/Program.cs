//program
using System.Collections;
using System.Text;
using Hemming.Domain.Response;
using Hemming.Services.Implementations;
using Hemming.Services.Interfaces;

IErrorMaker<BitArray> errorMaker = new BinErrorMaker();
ILogger logger=new ConsoleLogger();
IStringConverter<BitArray> converter=new BitToStringConverter();
HemmingCoder h = new HemmingCoder(converter,logger);


string text = "fghijk";
BitArray[] arr=h.Code(text);

for (int i = 0; i < arr.Length; i++) {
    arr[i] = errorMaker.MakeError(arr[i], 2);
}
BaseResponse<string> r= (BaseResponse<string>)h.Decode(arr);

Console.WriteLine(r.Description);
