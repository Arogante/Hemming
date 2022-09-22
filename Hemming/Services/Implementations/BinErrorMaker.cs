using Hemming.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Hemming.Services.Implementations
{
    class BinErrorMaker : IErrorMaker<BitArray>
    {
        public BitArray MakeError(BitArray data, int count=1)
        {
            Random random = new Random();
            bool[] errorPositions = new bool[data.Length];
            for (int i = 0; i < count; i++) {
                int pos = random.Next(data.Length);
                if (errorPositions[pos])
                    continue;
                errorPositions[pos] = true;
                data[pos] = !data[pos];
            }
            return data;
        }
    }
}
