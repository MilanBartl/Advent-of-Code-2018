using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day19
{
    public class Input
    {
        public string OperationName { get; set; }

        public int RegA { get; set; }

        public int RegB { get; set; }

        public int RegC { get; set; }

        public int[] Register { get; set; }

        public Input(string input)
        {
            var splits = input.Split(' ');
            OperationName = splits[0];
            RegA = int.Parse(splits[1]);
            RegB = int.Parse(splits[2]);
            RegC = int.Parse(splits[3]);
        }
    }
}
