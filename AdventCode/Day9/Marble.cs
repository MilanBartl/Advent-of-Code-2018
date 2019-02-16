using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day9
{
    public class Marble
    {
        public int Value { get; set; }

        public Marble Previous { get; set; }

        public Marble Next { get; set; }

        public Marble(int val)
        {
            Value = val;
        }

        public override string ToString()
        {
            int nextVal = Next == null ? -1 : Next.Value;
            int prevVal = Previous == null ? -1 : Previous.Value;
            return $"This {Value}; Next {nextVal}; Previous {prevVal}";
        }
    }
}
