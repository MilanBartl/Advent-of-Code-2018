using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day19
{
    public class Operation
    {
        public string Name { get; private set; }

        public int Id { get; set; }

        public Func<Input, int[]> Compute;

        public Operation(string name)
        {
            Name = name;
            Id = -1;
        }
    }
}
