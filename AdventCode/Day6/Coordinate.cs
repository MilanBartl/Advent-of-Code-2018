using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day6
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int LocationId { get; set; }

        public int MinDistance { get; set; }

        public int DistanceSum { get; set; }

        public bool IsShared { get; set; }

        public Coordinate()
        {
            LocationId = 0;
            MinDistance = int.MaxValue;
            DistanceSum = 0;
        }
    }
}
