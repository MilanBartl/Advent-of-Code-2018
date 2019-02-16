using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode.Day10
{
    public class Star
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int VelX { get; set; }

        public int VelY { get; set; }

        public Star(string input)
        {
            var splits = input.Split('>');

            string coordinates = splits[0].Split('<')[1];
            X = int.Parse(coordinates.Split(',')[0].Replace(" ", ""));
            Y = int.Parse(coordinates.Split(',')[1].Replace(" ", ""));

            string velocity = splits[1].Split('<')[1];
            VelX = int.Parse(velocity.Split(',')[0].Replace(" ", ""));
            VelY = int.Parse(velocity.Split(',')[1].Replace(" ", ""));
        }
    }
}
