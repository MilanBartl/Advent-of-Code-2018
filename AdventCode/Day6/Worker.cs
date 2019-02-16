using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day6
{
    public class Worker
    {
        private Coordinate[,] _map;

        private List<Location> _locations;

        private int _dimension = 500;

        public Worker()
        {
            _map = new Coordinate[_dimension, _dimension];
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    _map[i, j] = new Coordinate();
                }
            }

            int id = 1;
            _locations = _input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => new Location(id++, x)).ToList();

            // init map using locations
            foreach (Location location in _locations)
            {
                for (int i = 0; i < _dimension; i++)
                {
                    for (int j = 0; j < _dimension; j++)
                    {
                        int distance = CalculateManhattanDistance(location.X, i, location.Y, j);
                        _map[i, j].DistanceSum += distance;

                        if (_map[i, j].MinDistance > distance)
                        {
                            _map[i, j].LocationId = location.Id;
                            _map[i, j].MinDistance = distance;
                            _map[i, j].IsShared = false;
                        }
                        else if (_map[i, j].MinDistance == distance)
                        {
                            _map[i, j].LocationId = 0;
                            _map[i, j].IsShared = true;
                        }
                    }
                }
            }

            // identify inifinite locations
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (_map[i, j].LocationId != 0)
                    {
                        var location = _locations.First(l => l.Id == _map[i, j].LocationId);

                        if (i == 0 || j == 0 || i == _dimension - 1 || j == _dimension - 1)
                            location.IsInfinite = true;
                        else
                            location.Size++;
                    }
                }
            }
        }

        public int Work1()
        {
            return _locations.Where(l => !l.IsInfinite).Max(l => l.Size);
        }

        public int Work2()
        {
            List<Coordinate> safePoints = new List<Coordinate>();
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    if (_map[i, j].DistanceSum < 10000)
                        safePoints.Add(_map[i, j]);
                }
            }
            return safePoints.Count;
        }

        public int CalculateManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        string _testInput = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";

        string _input = @"84, 212
168, 116
195, 339
110, 86
303, 244
228, 338
151, 295
115, 49
161, 98
60, 197
40, 55
55, 322
148, 82
86, 349
145, 295
243, 281
91, 343
280, 50
149, 129
174, 119
170, 44
296, 148
152, 160
115, 251
266, 281
269, 285
109, 242
136, 241
236, 249
338, 245
71, 101
254, 327
208, 231
289, 184
282, 158
352, 51
326, 230
88, 240
292, 342
352, 189
231, 141
280, 350
296, 185
226, 252
172, 235
137, 161
207, 90
101, 133
156, 234
241, 185";
    }
}
