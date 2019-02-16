using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventCode.Day15;

namespace AdventCode.Day17
{
    public class Stream
    {
        public static Material[,] Ground { get; set; }

        public static int MaxX { get; set; }

        public static int MaxY { get; set; }

        public Coordinate StartPos { get; set; }

        /// <summary>
        /// Pointer to a current position of the current ~~~
        /// </summary>
        private Coordinate Pos { get; set; }

        public Stream(int x, int y)
        {
            StartPos = new Coordinate(x, y);
        }

        public void Gush()
        {
            bool canFallDown = true;
            bool canTrickleLeft = true;
            bool canTrickleRight = true;
            bool hasOverflown = false;

            var childStreams = new List<Stream>();

            Pos = StartPos;
            Ground[Pos.X, Pos.Y] = Material.WaterWashed;

            // fall down
            while (canFallDown && !hasOverflown) 
            {
                Pos = new Coordinate(Pos.X, Pos.Y + 1);
                Ground[Pos.X, Pos.Y] = Material.WaterWashed;

                canFallDown = Pos.Y + 1 < MaxY && (Ground[Pos.X, Pos.Y + 1] != Material.Clay && Ground[Pos.X, Pos.Y + 1] != Material.Water);
                hasOverflown = Pos.Y + 1 == MaxY;
            }

            if (hasOverflown)
                return;

            while (childStreams.Count == 0 && !hasOverflown)
            {
                var basinLevel = new List<Coordinate>();

                // fill left
                var basePos = Pos;
                do
                {
                    basinLevel.Add(Pos);

                    canTrickleLeft = Pos.X - 1 >= 0 && (Ground[Pos.X - 1, Pos.Y] != Material.Clay);
                    canFallDown = Pos.Y + 1 < MaxY && (Ground[Pos.X, Pos.Y + 1] != Material.Clay && Ground[Pos.X, Pos.Y + 1] != Material.Water);

                    Pos = new Coordinate(Pos.X - 1, Pos.Y);           
                } while (canTrickleLeft && !canFallDown);

                if (canFallDown)
                    childStreams.Add(new Stream(Pos.X + 1, Pos.Y));

                // fill right
                Pos = basePos;
                do
                {
                    basinLevel.Add(Pos);

                    canTrickleRight = Pos.X + 1 < MaxX && (Ground[Pos.X + 1, Pos.Y] != Material.Clay);
                    canFallDown = Pos.Y + 1 < MaxY && (Ground[Pos.X, Pos.Y + 1] != Material.Clay && Ground[Pos.X, Pos.Y + 1] != Material.Water);

                    Pos = new Coordinate(Pos.X + 1, Pos.Y);     
                } while (canTrickleRight && !canFallDown);

                if (canFallDown)
                    childStreams.Add(new Stream(Pos.X - 1, Pos.Y));

                if (Pos.Y - 1 == 0)
                    break;  // this should never happen

                if (childStreams.Count == 0)
                    basinLevel.ForEach(pos => Ground[pos.X, pos.Y] = Material.Water);
                else
                    basinLevel.ForEach(pos => Ground[pos.X, pos.Y] = Material.WaterWashed);

                // move one level up
                Pos = basePos;
                Pos = new Coordinate(Pos.X, Pos.Y - 1);
            }

            foreach (var child in childStreams)
            {
                child.Gush();
            }
        }
    }
}
