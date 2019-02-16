using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day3
{
    public class Claim
    {
        public int Id { get; private set; }

        public Coordinate TopLeft { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public HashSet<Coordinate> Area { get; private set; }

        public Claim(string input)
        {
            var splits = input.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            Id = int.Parse(splits[0].Substring(1));
            TopLeft = new Coordinate(int.Parse(splits[2].Substring(0, splits[2].IndexOf(','))),
                int.Parse(splits[2].Substring(splits[2].IndexOf(',') + 1, splits[2].Length - splits[2].IndexOf(',') - 2)));
            Width = int.Parse(splits[3].Substring(0, splits[3].IndexOf('x')));
            Height = int.Parse(splits[3].Substring(splits[3].IndexOf('x') + 1));

            Area = new HashSet<Coordinate>();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Area.Add(new Coordinate(TopLeft.X + i, TopLeft.Y + j));
                }
            }
        }
    }
}
