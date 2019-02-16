using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day15
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Distance { get; set; }

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
            return other.X == X && other.Y == Y;
        }

        public override int GetHashCode()
        {
            return 100 * X + Y;
        }
    }
}
