using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day15
{
    public class Worker
    {
        private int _mapWidth, _mapHeight;
        private bool[,] _map;

        private List<Unit> _elves = new List<Unit>();

        private List<Unit> _goblins = new List<Unit>();

        public Worker()
        {
            var splits = _testInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            _mapWidth = splits[0].Length;
            _mapHeight = splits.Length;
            _map = new bool[_mapWidth, _mapHeight];

            for (int i = 0; i < splits.Length; i++)
            {
                for (int j = 0; j < splits[i].Length; j++)
                {
                    switch (splits[i][j])
                    {
                        case '#':
                            _map[j, i] = true;
                            break;
                        case '.':
                            _map[j, i] = false;
                            break;
                        case 'E':
                            _map[j, i] = true;
                            _elves.Add(new Unit(j, i, Race.Elf));
                            break;
                        case 'G':
                            _map[j, i] = true;
                            _goblins.Add(new Unit(j, i, Race.Goblin));
                            break;
                        default:
                            throw new Exception("This should not happen.");
                    }
                }
            }
        }

        public int Work1()
        {
            throw new NotImplementedException();
        }

        public int Work2()
        {
            throw new NotImplementedException();
        }

        string _testInput = @"#######
#G..#E#
#E#E.E#
#G.##.#
#...#E#
#...E.#
#######";

        string _input = @"################################
###################........#####
###################..G..#G..####
####################........####
##..G###############G......#####
###..G###############.....######
#####.######..######....G##..###
#####.........####............##
#########...#####.............##
#########...####..............##
#########E#####.......GE......##
#########............E...G...###
######.###....#####..G........##
#.G#....##...#######.........###
##.#....##GG#########.........##
#....G#....E#########....#....##
#...........#########.......####
#####..G....#########...##....##
#####....G..#########.#.......##
#######...G..#######G.....#...##
######....E...#####............#
######...GG.......E......#...E.#
#######.G...#....#..#...#.....##
#######..........#####..####.###
########.......E################
#######..........###############
########.............###########
#########...#...##....##########
#########.....#.#..E..##########
################.....###########
################.##E.###########
################################";
    }
}
