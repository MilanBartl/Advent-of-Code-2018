using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day17
{
    /// <summary>
    /// Bad idea, it did leave some of the tanks half empty
    /// </summary>
    public class Water
    {
        private static int _countGen = 1;

        private int _count = _countGen++;

        public int X { get; set; }

        public int Y { get; set; }

        public bool CanFallDown { get; set; }

        public bool CanMoveLeft { get; set; }

        public bool CanMoveRight { get; set; }

        public bool MovesLeft { get; set; }

        public bool HasBounced { get; set; }

        public bool HasTurnedAround { get; set; }

        public Water(int x, int y, bool movesLeft)
        {
            X = x;
            Y = y;
            MovesLeft = movesLeft;
        }

        /// <summary>
        /// Flows water through the ground
        /// </summary>
        /// <returns>True if water overflows</returns>
        public bool Flow(Material[,] ground, int maxX, int maxY)
        {
            HasBounced = false;

            CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
            CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
            CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);

            while (CanFallDown || CanMoveLeft || CanMoveRight)
            {
                if (HasBounced && !HasTurnedAround)
                {
                    var cloneSplash = new Water(X, Y, !MovesLeft);
                    cloneSplash.Flow(ground, maxX, maxY);
                }

                if (MovesLeft)
                    TrickleLeftways(ground, maxX, maxY);
                else
                    TrickleRightways(ground, maxX, maxY);


                // trickle away
                if (Y + 1 >= maxY)
                {
                    ground[X, Y] = Material.WaterWashed;
                    return true;
                }
            }

            return false;
        }

        private void TrickleLeftways(Material[,] ground, int maxX, int maxY)
        {
            // trickle down
            if (CanFallDown)
            {
                HasBounced = false;
                HasTurnedAround = false;
                MoveDown(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
                CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);
            }
            // trickle leftwards
            else if (CanMoveLeft && !HasTurnedAround)
            {
                if (CanMoveRight && ground[X - 1, Y] != Material.WaterWashed)
                    HasBounced = true;
                MoveLeft(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
                CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);
            }
            // trickle rightwards
            else if (CanMoveRight)
            {
                HasTurnedAround = true;
                MoveRight(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                //CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
                CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);
            }
        }

        private void TrickleRightways(Material[,] ground, int maxX, int maxY)
        {
            // trickle down
            if (CanFallDown)
            {
                HasBounced = false;
                HasTurnedAround = false;
                MoveDown(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
                CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);
            }
            // trickle rightwards
            else if (CanMoveRight && !HasTurnedAround)
            {
                if (CanMoveRight && ground[X + 1, Y] != Material.WaterWashed)
                    HasBounced = true;
                MoveRight(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                CanMoveRight = X + 1 < maxX && (ground[X + 1, Y] != Material.Clay && ground[X + 1, Y] != Material.Water);
                CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
            }
            // trickle leftwards
            else if (CanMoveLeft)
            {
                HasTurnedAround = true;
                MoveLeft(ground);

                CanFallDown = Y + 1 < maxY && (ground[X, Y + 1] != Material.Clay && ground[X, Y + 1] != Material.Water);
                CanMoveLeft = X - 1 >= 0 && (ground[X - 1, Y] != Material.Clay && ground[X - 1, Y] != Material.Water);
            }
        }

        private void MoveDown(Material[,] ground)
        {
            ground[X, Y] = Material.WaterWashed;
            Y++;
            ground[X, Y] = Material.Water;
        }

        private void MoveLeft(Material[,] ground)
        {
            ground[X, Y] = Material.WaterWashed;
            X--;
            ground[X, Y] = Material.Water;
        }

        private void MoveRight(Material[,] ground)
        {
            ground[X, Y] = Material.WaterWashed;
            X++;
            ground[X, Y] = Material.Water;
        }
    }
}
