using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day12
{
    public class Spread
    {
        public bool[] PreviousGeneration { get; set; }

        public bool HasPlant { get; set; }

        public Spread(string input)
        {
            var splits = input.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);

            string spread = splits[0].Trim();
            PreviousGeneration = new bool[5];
            for (int i = 0; i < PreviousGeneration.Length; i++)
            {
                PreviousGeneration[i] = input[i] == '#';
            }

            HasPlant = splits[1].Trim() == "#";
        }
    }
}
