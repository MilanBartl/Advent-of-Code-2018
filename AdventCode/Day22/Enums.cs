using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day22
{
    public class Enums
    {
        public enum CaveType
        {
            Rocky,
            Wet,
            Narrow
        }

        public enum Tool
        {
            Torch = 0, 
            Climbing = 1,
            None = 2
        }

        public static Dictionary<CaveType, Tool[]> CaveToolMapping = new Dictionary<CaveType, Tool[]>
        {
            { CaveType.Rocky, new [] { Tool.Torch, Tool.Climbing } },
            { CaveType.Wet, new [] { Tool.Climbing, Tool.None } },
            { CaveType.Narrow, new [] { Tool.None, Tool.Torch } }
        };
    }
}
