using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day3
{
    public class Coordinate
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Coordinate;
            if (other == null)
                return false;
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return X + (Y * 1000);
        }
    }
}
