using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day6
{
    public class Location
    {
        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Size { get; set; }

        public bool IsInfinite { get; set; }

        public Location(int id, string input)
        {
            Id = id;
            var splits = input.Split(',');
            X = int.Parse(splits[0]);
            Y = int.Parse(splits[1].Substring(1));
        }
    }
}
