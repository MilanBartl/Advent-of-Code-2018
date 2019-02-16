using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day11
{
    public class Worker
    {
        private int _dimension = 300;
        private Cell[,] _cells;

        public Worker()
        {
            int serial = 1955;
            //int serial = 18;

            _cells = new Cell[_dimension, _dimension];

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    _cells[j, i] = new Cell(j, i, serial);
                }
            }
        }

        public string Work1()
        {
            int maxPower = -1000;
            string coordinates = "";

            for (int i = 0; i < _dimension - 3; i++)
            {
                for (int j = 0; j < _dimension - 3; j++)
                {
                    int squarePower = GetSquarePower(j, i, 3);
                    if (squarePower > maxPower)
                    {
                        maxPower = squarePower;
                        coordinates = $"{j},{i}";
                    }
                }
            }
            return coordinates;
        }

        public string Work2()
        {
            int maxPower = -1000;
            string coordinates = "";

            for (int s = 0; s < 300; s++)
            {
                int sizePower = maxPower;

                for (int i = 0; i < _dimension - s; i++)
                {
                    for (int j = 0; j < _dimension - s; j++)
                    {
                        int squarePower = GetSquarePower(j, i, s);
                        if (squarePower > maxPower)
                        {
                            maxPower = squarePower;
                            coordinates = $"{j},{i},{s}";
                        }
                    }
                }

                if (sizePower == maxPower)
                    break;
            }
            return coordinates;
        }

        private int GetSquarePower(int x, int y, int size)
        {
            int power = 0;

            for (int i = y; i < y + size; i++)
            {
                for (int j = x; j < x + size; j++)
                {
                    power += _cells[j, i].PowerLevel;
                }
            }
            return power;
        }
    }
}
