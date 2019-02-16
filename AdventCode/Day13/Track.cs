using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventCode.Day13.Enums;

namespace AdventCode.Day13
{
    public class Track
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Track Up { get; set; }

        public Track Down { get; set; }

        public Track Left { get; set; }

        public Track Right { get; set; }

        public bool IsIntersection { get; set; }

        public bool HasCrash { get; set; }

        public Track(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            string connections = "";
            connections += Up == null ? "" : "Up,";
            connections += Down == null ? "" : "Down,";
            connections += Left == null ? "" : "Left,";
            connections += Right == null ? "" : "Right,";
            return $"{X},{Y}; {connections}";
        }
    }
}
