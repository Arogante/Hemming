﻿using Hemming.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemming.Services.Interfaces
{
    interface ILogger
    {
        public void Log(string message);
    }
}
