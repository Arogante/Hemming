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
    class ConsoleLogger : ILogger
    {


        public void Log(string message)
        {
            Console.Write(message);
        }
    }
}
