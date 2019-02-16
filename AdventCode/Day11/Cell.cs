using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day11
{
    public class Cell
    {
        public int GridSerial { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int RackId { get { return X + 10; } }

        private int _powerLevel = -100;

        public int PowerLevel
        {
            get
            {
                if (_powerLevel == -100)
                {
                    int power = ((RackId * Y) + GridSerial) * RackId;
                    power /= 100;
                    _powerLevel = power % 10 - 5;
                }
                return _powerLevel;
            }
        }

        public Cell(int x, int y, int serial)
        {
            GridSerial = serial;
            X = x;
            Y = y;
        }
    }
}
