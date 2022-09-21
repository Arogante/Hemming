using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hemming.Domain.Response;

namespace Hemming.Services.Interfaces
{
        interface ICoder<T>
        {
            public T[] Code(string text);
            public IBaseResponse<string> Decode(T[] code);
        }
}
