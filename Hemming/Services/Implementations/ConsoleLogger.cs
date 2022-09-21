using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hemming.Services.Interfaces;
using Hemming.Domain.Response;
using System.Collections;

namespace Hemming.Services.Implementations
{
    class ConsoleLogger : ILogger<BitArray>
    {


        public void Log(IBaseResponse<BitArray> response)
        {
            throw new NotImplementedException();
        }
    }
}
