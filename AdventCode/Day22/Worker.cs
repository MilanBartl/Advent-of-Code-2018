using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventCode.Day15;

namespace AdventCode.Day22
{
    public class Worker
    {
        //private int _depth = 510;
        private int _depth = 11109;

        //private Coordinate _target = new Coordinate(10 + 1, 10 + 1);
        private Coordinate _target = new Coordinate(9 + 1, 731 + 1);

        private CaveRoom[,] _cave;

        private int _width;
        private int _height;

        public Worker()
        {
            _width = _target.X + 5;
            _height = _target.Y + 5;
            _cave = new CaveRoom[_width, _height];

            for (int i = 0; i < _target.Y + 5; i++)
            {
                for (int j = 0; j < _target.X + 5; j++)
                {
                    int gi;
                    if ((j == 0 && i == 0) || (j == _target.X && i == _target.Y))
                        gi = 0;
                    else if (j == 0)
                        gi = i * 48271;
                    else if (i == 0)
                        gi = j * 16807;
                    else
                        gi = _cave[j - 1, i].ErosionLvl * _cave[j, i - 1].ErosionLvl;

                    int erosion = (gi + _depth) % 20183;
                    Enums.CaveType type;
                    switch (erosion % 3)
                    {
                        case 0:
                            type = Enums.CaveType.Rocky;
                            break;
                        case 1:
                            type = Enums.CaveType.Wet;
                            break;
                        case 2:
                            type = Enums.CaveType.Narrow;
                            break;
                        default:
                            throw new Exception("This should not happen.");
                    }

                    _cave[j, i] = new CaveRoom(type, gi, erosion);
                }
            }

            DrawCave();
        }

        public int Work1()
        {
            int riskLvl = 0;

            for (int i = 0; i < _target.Y; i++)
            {
                for (int j = 0; j < _target.X; j++)
                {
                    if (i == _target.Y - 1 && j == _target.X - 1)
                        continue;

                    switch (_cave[j, i].Type)
                    {
                        case Enums.CaveType.Rocky:
                            break;
                        case Enums.CaveType.Wet:
                            riskLvl++;
                            break;
                        case Enums.CaveType.Narrow:
                            riskLvl += 2;
                            break;
                        default:
                            throw new Exception("Unreachable.");
                    }
                }
            }
            return riskLvl;
        }

        public int Work2()
        {
            throw new NotImplementedException();
        }

        private void DrawCave()
        {
            for (int i = 0; i < _target.Y; i++)
            {
                for (int j = 0; j < _target.X; j++)
                {
                    switch (_cave[j, i].Type)
                    {
                        case Enums.CaveType.Rocky:
                            Console.Write('.');
                            break;
                        case Enums.CaveType.Wet:
                            Console.Write('=');
                            break;
                        case Enums.CaveType.Narrow:
                            Console.Write('|');
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
