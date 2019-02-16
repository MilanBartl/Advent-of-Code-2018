using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day18
{
    public class Acre
    {
        public int X { get; set; }

        public int Y { get; set; }

        public AcreType Type { get; set; }

        public AcreType NextType { get; set; }

        public Acre(int x, int y, AcreType type)
        {
            X = x;
            Y = y;
            Type = type;
        }

        public void Change()
        {
            Type = NextType;
        }
    }

    public enum AcreType
    {
        OpenGround,
        Trees,
        Lumberyard
    }
}
