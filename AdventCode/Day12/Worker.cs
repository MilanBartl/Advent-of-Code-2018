using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day12
{
    public class Worker
    {
        private bool[] _pots;

        private List<Spread> _spreads = new List<Spread>();

        private int _length;

        private int _negLength;

        private int _positiveAddition;

        public Worker()
        {
            _length = _initial.Length;
            _negLength = 100;   // large enough
            _positiveAddition = 5000;   // large enough

            _pots = new bool[_length + _negLength + _positiveAddition + 2];

            for (int i = 0; i < _length + _negLength; i++)
            {
                if (i < _negLength)
                    _pots[i] = false;
                else
                    _pots[i] = _initial[i - _negLength] == '#';
            }

            var splits = _spreadsInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string split in splits)
            {
                _spreads.Add(new Spread(split));
            }
        }

        public long Work1()
        {
            for (int g = 0; g < 20; g++)
            {
                RunSpread();
            }

            return CountPlants();
        }

        public long Work2()
        {
            long[] growth = new long[3000]; // large enough

            long count = 0;
            for (long g = 0; g < 50000000000; g++)
            {
                RunSpread();

                long currentCount = CountPlants();

                if (count != currentCount)
                {
                    growth[g] = currentCount - count;
                    count = currentCount;
                }

                if (g < 50)
                    continue;

                bool isConstantGrowth = true;
                for (int i = 1; i < 11; i++)
                {
                    isConstantGrowth = growth[g] == growth[g - i];
                    if (!isConstantGrowth)
                        break;
                }

                if (isConstantGrowth)
                    break;
            }

            return count + ((50000000000 * 62) - (62 * (growth.ToList().FindLastIndex(g => g != 0) + 1)));
        }

        private void RunSpread()
        {         
            bool[] newPots = new bool[_pots.Length];
            foreach (var spread in _spreads)
            {
                for (int i = 2; i < _length + _negLength + _positiveAddition; i++)
                {
                    bool[] prevGen = new bool[5];

                    // copy array part of interest
                    for (int j = 0; j < 5; j++)
                    {
                        prevGen[j] = _pots[j + i - 2];
                    }

                    if (prevGen.SequenceEqual(spread.PreviousGeneration))
                        newPots[i] = spread.HasPlant;
                }
            }
            _pots = newPots;
        }

        private long CountPlants()
        {
            long output = 0;
            for (int i = 0; i < _length + _negLength + _positiveAddition; i++)
            {
                if (_pots[i])
                    output += i - _negLength;
            }

            return output;
        }

        string _testInitial = @"#..#.#..##......###...###";

        string _testSpreadsInput = @"...## => #
..#.. => #
.#... => #
.#.#. => #
.#.## => #
.##.. => #
.#### => #
#.#.# => #
#.### => #
##.#. => #
##.## => #
###.. => #
###.# => #
####. => #";

        string _initial = @"##.##.##..#..#.#.#.#...#...#####.###...#####.##..#####.#..#.##..#..#.#...#...##.##...#.##......####";

        string _spreadsInput = @"##.#. => #
#.#.. => #
##... => .
...## => #
###.# => #
#.##. => #
#.### => #
####. => #
.#..# => #
...#. => .
#..#. => .
#.#.# => .
.##.# => .
..#.. => .
.#.## => #
..##. => .
.#.#. => #
#..## => #
..#.# => #
#.... => .
..### => .
#...# => .
##### => #
###.. => #
....# => .
##.## => #
.#### => .
..... => .
##..# => #
.##.. => .
.###. => .
.#... => #";
    }
}
