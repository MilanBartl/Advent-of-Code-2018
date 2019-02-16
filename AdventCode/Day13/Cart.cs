using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventCode.Day13.Enums;

namespace AdventCode.Day13
{
    public class Cart
    {
        private static int _idGen = 1;

        private int _turns = 0;

        public int Id { get; private set; }

        public Track CurrentTrack { get; set; }

        public Direction Direction { get; set; }

        public Cart(Track current, Direction direction)
        {
            Id = _idGen++;
            CurrentTrack = current;
            Direction = direction;
        }

        public override string ToString()
        {
            return $"Cart {Id}: {CurrentTrack.X},{CurrentTrack.Y}";
        }

        public void Move()
        {
            if (CurrentTrack.IsIntersection)
            {
                switch (_turns)
                {
                    case 0:
                        TurnLeft();
                        _turns++;
                        break;
                    case 1:
                        MoveStraight();
                        _turns++;
                        break;
                    case 2:
                        TurnRight();
                        _turns = 0;
                        break;
                    default:
                        throw new Exception("This should never happen.");
                }
            }
            else
            {
                MoveStraight();
            }
        }

        private void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.Up:
                    CurrentTrack = CurrentTrack.Left;
                    Direction = Direction.Left;
                    break;
                case Direction.Down:
                    CurrentTrack = CurrentTrack.Right;
                    Direction = Direction.Right;
                    break;
                case Direction.Left:
                    CurrentTrack = CurrentTrack.Down;
                    Direction = Direction.Down;
                    break;
                case Direction.Right:
                    CurrentTrack = CurrentTrack.Up;
                    Direction = Direction.Up;
                    break;
                default:
                    throw new Exception("Can't move. This should never happen.");
            }
        }

        private void TurnRight()
        {
            switch (Direction)
            {
                case Direction.Up:
                    CurrentTrack = CurrentTrack.Right;
                    Direction = Direction.Right;
                    break;
                case Direction.Down:
                    CurrentTrack = CurrentTrack.Left;
                    Direction = Direction.Left;
                    break;
                case Direction.Left:
                    CurrentTrack = CurrentTrack.Up;
                    Direction = Direction.Up;
                    break;
                case Direction.Right:
                    CurrentTrack = CurrentTrack.Down;
                    Direction = Direction.Down;
                    break;
                default:
                    throw new Exception("Can't move. This should never happen.");
            }
        }

        private void MoveStraight()
        {
            switch (Direction)
            {
                case Direction.Up:
                    if (CurrentTrack.Up != null)
                        CurrentTrack = CurrentTrack.Up;
                    else if (CurrentTrack.Left != null)
                    {
                        CurrentTrack = CurrentTrack.Left;
                        Direction = Direction.Left;
                    }
                    else if (CurrentTrack.Right != null)
                    {
                        CurrentTrack = CurrentTrack.Right;
                        Direction = Direction.Right;
                    }
                    else
                        throw new Exception("Can't move. This should never happen.");
                    break;
                case Direction.Down:
                    if (CurrentTrack.Down != null)
                        CurrentTrack = CurrentTrack.Down;
                    else if (CurrentTrack.Left != null)
                    {
                        CurrentTrack = CurrentTrack.Left;
                        Direction = Direction.Left;
                    }
                    else if (CurrentTrack.Right != null)
                    {
                        CurrentTrack = CurrentTrack.Right;
                        Direction = Direction.Right;
                    }
                    else
                        throw new Exception("Can't move. This should never happen.");
                    break;
                case Direction.Left:
                    if (CurrentTrack.Left != null)
                        CurrentTrack = CurrentTrack.Left;
                    else if (CurrentTrack.Up != null)
                    {
                        CurrentTrack = CurrentTrack.Up;
                        Direction = Direction.Up;
                    }
                    else if (CurrentTrack.Down != null)
                    {
                        CurrentTrack = CurrentTrack.Down;
                        Direction = Direction.Down;
                    }
                    else
                        throw new Exception("Can't move. This should never happen.");
                    break;
                case Direction.Right:
                    if (CurrentTrack.Right != null)
                        CurrentTrack = CurrentTrack.Right;
                    else if (CurrentTrack.Up != null)
                    {
                        CurrentTrack = CurrentTrack.Up;
                        Direction = Direction.Up;
                    }
                    else if (CurrentTrack.Down != null)
                    {
                        CurrentTrack = CurrentTrack.Down;
                        Direction = Direction.Down;
                    }
                    else
                        throw new Exception("Can't move. This should never happen.");
                    break;
            }
        }
    }
}
