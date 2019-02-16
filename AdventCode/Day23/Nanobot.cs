using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day23
{
    public class Nanobot
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }

        public int Radius { get; set; }

        public Nanobot(string input)
        {
            var splits = input.Split(new[] { ">," }, StringSplitOptions.RemoveEmptyEntries);
            Radius = int.Parse(splits[1].Substring(3));

            string coords = splits[0].Substring(5);
            splits = coords.Split(',');
            X = int.Parse(splits[0]);
            Y = int.Parse(splits[1]);
            Z = int.Parse(splits[2]);
        }
    }
}
