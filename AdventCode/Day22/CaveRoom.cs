using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day22
{
    public class CaveRoom
    {
        public Enums.CaveType Type { get; set; }

        public int GeoIndex { get; set; }

        public int ErosionLvl { get; set; }

        public CaveRoom(Enums.CaveType type, int geoIndex, int erosion)
        {
            Type = type;
            GeoIndex = geoIndex;
            ErosionLvl = erosion;
        }
    }    
}
