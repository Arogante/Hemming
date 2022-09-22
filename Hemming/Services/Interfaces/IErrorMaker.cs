using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemming.Services.Interfaces
{
    interface IErrorMaker<T>
    {
        public T MakeError(T data, int count);
    }
}
