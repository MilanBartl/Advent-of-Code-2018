using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day18
{
    public class Worker
    {
        private List<Acre> _acres;

        private int _dimension;

        public Worker()
        {
            var splits = _input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _dimension = splits.Length;
            _acres = new List<Acre>();

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    AcreType type;

                    switch (splits[i][j])
                    {
                        case '.':
                            type = AcreType.OpenGround;
                            break;
                        case '|':
                            type = AcreType.Trees;
                            break;
                        case '#':
                            type = AcreType.Lumberyard;
                            break;
                        default:
                            throw new Exception("This should not happen.");
                    }

                    _acres.Add(new Acre(j, i, type));
                }
            }

            //PrintGroud();
        }

        public int Work1()
        {
            for (int i = 0; i < 10; i++)
            {
                foreach (var acre in _acres)
                {
                    var adjacent = GetAdjacentAcres(acre);

                    if (acre.Type == AcreType.OpenGround && adjacent.Count(a => a.Type == AcreType.Trees) >= 3)
                        acre.NextType = AcreType.Trees;
                    else if (acre.Type == AcreType.Trees && adjacent.Count(a => a.Type == AcreType.Lumberyard) >= 3)
                        acre.NextType = AcreType.Lumberyard;
                    else if (acre.Type == AcreType.Lumberyard && !(adjacent.Any(a => a.Type == AcreType.Lumberyard) && adjacent.Any(a => a.Type == AcreType.Trees)))
                        acre.NextType = AcreType.OpenGround;
                    else
                        acre.NextType = acre.Type;
                }

                foreach (var acre in _acres)
                {
                    acre.Change();
                }

                //PrintGroud();
            }

            return _acres.Count(a => a.Type == AcreType.Trees) * _acres.Count(a => a.Type == AcreType.Lumberyard);
        }

        public int Work2()
        {
            int yards = 0;
            int trees = 0;
            int open = 0;
            int sum = 0;
            int output = 0;

            int queueSize = 3;
            var queue = new Queue<int>();
            var sums = new List<int>();
            bool firstThreeFound = false;

            for (int i = 0; i < 1000000000; i++)
            {
                foreach (var acre in _acres)
                {
                    var adjacent = GetAdjacentAcres(acre);

                    if (acre.Type == AcreType.OpenGround && adjacent.Count(a => a.Type == AcreType.Trees) >= 3)
                        acre.NextType = AcreType.Trees;
                    else if (acre.Type == AcreType.Trees && adjacent.Count(a => a.Type == AcreType.Lumberyard) >= 3)
                        acre.NextType = AcreType.Lumberyard;
                    else if (acre.Type == AcreType.Lumberyard && !(adjacent.Any(a => a.Type == AcreType.Lumberyard) && adjacent.Any(a => a.Type == AcreType.Trees)))
                        acre.NextType = AcreType.OpenGround;
                    else
                        acre.NextType = acre.Type;
                }

                foreach (var acre in _acres)
                {
                    acre.Change();
                }

                yards = _acres.Count(a => a.Type == AcreType.Lumberyard);
                trees = _acres.Count(a => a.Type == AcreType.Trees);
                open = _acres.Count(a => a.Type == AcreType.OpenGround);
                sum = trees * yards;

                if (queue.Count == queueSize)
                {
                    sums.Add(queue.First());
                    queue.Dequeue();
                }
                queue.Enqueue(sum);

                if (!firstThreeFound && queue.Count >= queueSize && sums.Contains(queue.ToList()))
                {
                    queueSize = 50;    // large enough
                    queue = new Queue<int>();
                    firstThreeFound = true;
                }
                else if (firstThreeFound && queue.Count >= queueSize && sums.Contains(queue.ToList()))
                {
                    int queueIndex = (1000000000 - i + 1) % queue.Count;
                    output = queue.ToList()[queueIndex + 1];
                    break;
                }

                //PrintGroud();
            }

            return output;
        }

        private IEnumerable<Acre> GetAdjacentAcres(Acre acre)
        {
            return _acres.Where(a => a != acre && a.X >= acre.X - 1 && a.X <= acre.X + 1 && a.Y >= acre.Y - 1 && a.Y <= acre.Y + 1);
        }

        private void PrintGroud()
        {
            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                    var acre = _acres.First(a => a.X == j && a.Y == i);
                    switch (acre.Type)
                    {
                        case AcreType.Lumberyard:
                            Console.Write('#');
                            break;
                        case AcreType.Trees:
                            Console.Write('|');
                            break;
                        case AcreType.OpenGround:
                            Console.Write('.');
                            break;
                        default:
                            throw new Exception("This should not happen.");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        string _testInput = @".#.#...|#.
.....#|##|
.|..|...#.
..|#.....#
#.#|||#|#|
...#.||...
.|....|...
||...#|.#|
|.||||..|.
...#.|..|.";

        string _input = @".|....|.##.#..|.#....|.#...#..#.#..|.|........#|#|
#....#...|..|#|....#.......#||#...#..|...###...#.#
......|#.|#....#...|...##|...|..#|#...#..#...#....
....|....|....#||...|.....|....|.|.#...##...|#.||#
...||..||.||..|...|.#.#.|##||...|#..|||.#|.|..|.|#
||..#.|...#..#.....|..#...|.#.#..#.#......||.|.#..
...||##..#|....#|||#|.|.#..|.|.....|.#.#.||#|....#
##..........|###..||...#....|##..#|...|||.....#|.#
.##|...|#.|..|......#...||##.#....|..#.....#..##..
.|..|#|...|#.#..|###.|..##.#...|........|.#.|..#..
.##....#|.......|..||##..|..|.|##|.#|....#|....|..
..#|#..|.......||.|..#..|..#..##..||.......###..|#
...##..|..#|....|....#.||..|..#.#....#|..#.#...#..
.#..|...#.|...##.|#.......#|#...#.......|..#....|#
#.##...|....||.||##||.|...#.#...#.#..|..#...#..#||
........||#.#..||.||..|..#.##......||..##.|#..|.|.
.....|.....#....|...#...#...||.##..|..#.........|.
.#....|.##....||.....#..||.|..##.||.|..###.....#..
#...#||||...|...#.|......|.#.###||.##|...|.....#|.
.....##.#.#...........|##.....#.....#..|...#|#.|#|
.|###.|.||.|.|#|.|#....|#.|....#||#...#....##..#..
...||..#..|.|........||...|..#..#..#..#.|#...||.|.
..||..|||.|........#.....|.....|....|.|...||.|#..#
#.#.###..|||..|#.#|#....#.|..|..........#....||#.|
.#..#..||||#....#....#|..........#...#.#.##.###|..
...#..|.|.||..|..##.|.#.|#....|.#..||..|.#..#|#...
.#.#.|..|..|#..|.|........|.##..|..##||#.#.|...|.|
|.....##.|...###||.##||.|#........#.|##.#..#...#|#
|..##.||##..#.|....||##...#..#....|..............#
#..#|..#|........|#.#|..#|..|......|..#|.......|..
|...|.#.#.||..|...|#...#.||#.||......##......##.#|
.....#|#.#||#.|.|..||#|##.|#|...||.#.#|.#......||#
..|.|..#|#...|#..#...|...|#..|.#.|.#.#.#.##.......
...|#|.|#.|..|..|....|##|#....#....|......#.||...#
.|...|..#.||#|#....#...|.....|.|....#|.#....#||##.
.....#.#...#.|.#|.|.##|||..|.#..|.||#..#...#..#.##
..||....#..|..|....##.#|#....|.#.|......|#.|#...##
#|....|..||..|.......|#...#|....##..|..|||..#...#.
.#...#....###||..|||...|..##...#|||......##||#.#..
|.||##.#..||..|||.........#|..|.#.|.|.#.|....|#|#|
.|..#||..#.|#.#|..|....#.....#|..##|#..#||#.#...##
...#||....|....#.....#||.#||..#|.##......|..#.....
|..|...#|......|||....#...#......##.#.##.....#....
.....#....|....###......|#||.#.....#.|..#...|....#
.|.|#...#|..|||....#....#.|#...|.#.#...#...#.|..|.
.|.|.....#.#.|##.....#...#..|....#.|..|##..#.#....
.#|..|...#..|...|..#|||.|..#.#..|.||....#.#|...#..
|.|#|.#.....||####..#.#..#|.##.#|....#||.....|.|.|
.|...#|.|.||||..#.|....|.....|.|#|#....|.....#..|#
.#.......|#.#..#...#.#.....#..|.#|.|#.#|...|#|||#.";
    }
}
