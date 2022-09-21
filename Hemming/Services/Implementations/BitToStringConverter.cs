using Hemming.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemming.Services.Implementations
{
    class BitToStringConverter : IStringConverter<BitArray>
    {
        public string ToString(BitArray data)
        {
            StringBuilder sb=new StringBuilder();
            for (int i = 0; i < data.Length; i++) { 
                sb.Append(data[i]?1:0);
            }
            return sb.ToString();
        }
    }
}
