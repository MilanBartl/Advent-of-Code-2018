using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day25
{
    public class Point
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int T { get; set; }

        public Constellation Constellation { get; set; }

        public Point(string input)
        {
            var splits = input.Split(',');
            X = int.Parse(splits[0]);
            Y = int.Parse(splits[1]);
            Z = int.Parse(splits[2]);
            T = int.Parse(splits[3]);
        }
    }
}
