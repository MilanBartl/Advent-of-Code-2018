using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day16
{
    public class Worker
    {
        private List<Example> _examples = new List<Example>();

        private List<Operation> _operations = new List<Operation>();

        public Worker()
        {
            var splits = _input1.Split(new[] { $"{Environment.NewLine}{Environment.NewLine}" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var split in splits)
            {
                _examples.Add(new Example(split));
            }

            InitOps();
        }

        public int Work1()
        {
            /*var example = new Example(_testInput1);

            foreach (var operation in _operations)
            {
                var output = operation.Compute(example);
                if (Enumerable.SequenceEqual(output, example.After))
                    example.PossibleOperations++;
            }

            return example.PossibleOperations;*/

            foreach (var example in _examples)
            {
                foreach (var operation in _operations)
                {
                    var output = operation.Compute(example);
                    if (Enumerable.SequenceEqual(output, example.After))
                        example.PossibleOperations.Add(operation);
                }
            };

            return _examples.Where(ex => ex.PossibleOperationsCount > 2).Count();
        }

        public int Work2()
        {
            foreach (var example in _examples)
            {
                foreach (var operation in _operations)
                {
                    var output = operation.Compute(example);
                    if (Enumerable.SequenceEqual(output, example.After))
                        example.PossibleOperations.Add(operation);
                }
            };

            while (_operations.Any(x => x.Id == -1))
            {
                foreach (var example in _examples)
                {
                    if (example.PossibleOperationsCount == 1)
                    {
                        example.PossibleOperations[0].Id = example.OperationId;
                        foreach (var ex in _examples)
                        {
                            if (ex == example)
                                continue;
                            ex.PossibleOperations.Remove(example.PossibleOperations[0]);
                        }
                    }
                }
            }

            var splits = _input2.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var register = new int[4];

            foreach (string split in splits)
            {
                var digits = split.Split(' ');
                int opId = int.Parse(digits[0]);
                int regA = int.Parse(digits[1]);
                int regB = int.Parse(digits[2]);
                int regC = int.Parse(digits[3]);
                register = _operations.First(o => o.Id == opId).Compute(new Example(opId, regA, regB, regC, register));
            }

            return register[0];
        }

        private void InitOps()
        {
            _operations.Add(
                new Operation("addr")
                {
                    Compute = (Example example) => 
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] + output[example.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("addi")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] + example.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("mulr")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] * output[example.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("muli")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] * example.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("banr")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] & output[example.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("bani")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] & example.RegB;
                        return output;
                    }
                });
            _operations.Add(
               new Operation("borr")
               {
                   Compute = (Example example) =>
                   {
                       int[] output = (int[])example.Before.Clone();
                       output[example.RegC] = output[example.RegA] | output[example.RegB];
                       return output;
                   }
               });
            _operations.Add(
                new Operation("bori")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] | example.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("setr")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("seti")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = example.RegA;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtir")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = example.RegA > output[example.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtri")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] > example.RegB ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtrr")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] > output[example.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqir")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = example.RegA == output[example.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqri")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] == example.RegB ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqrr")
                {
                    Compute = (Example example) =>
                    {
                        int[] output = (int[])example.Before.Clone();
                        output[example.RegC] = output[example.RegA] == output[example.RegB] ? 1 : 0;
                        return output;
                    }
                });
        }

        string _testInput1 = @"Before: [3, 2, 1, 1]
9 2 1 2
After:  [3, 2, 2, 1]";

        string _input1 = @"Before: [0, 2, 2, 2]
4 2 3 2
After:  [0, 2, 5, 2]

Before: [2, 2, 1, 3]
3 1 0 2
After:  [2, 2, 1, 3]

Before: [0, 2, 0, 0]
8 1 2 0
After:  [4, 2, 0, 0]

Before: [2, 2, 2, 0]
2 1 2 2
After:  [2, 2, 1, 0]

Before: [3, 1, 2, 2]
11 1 0 3
After:  [3, 1, 2, 3]

Before: [1, 1, 2, 3]
12 0 3 0
After:  [0, 1, 2, 3]

Before: [3, 1, 2, 2]
15 0 3 3
After:  [3, 1, 2, 6]

Before: [1, 2, 2, 2]
6 1 0 1
After:  [1, 1, 2, 2]

Before: [0, 1, 3, 1]
5 1 0 2
After:  [0, 1, 1, 1]

Before: [2, 2, 1, 0]
3 1 0 1
After:  [2, 1, 1, 0]

Before: [3, 3, 1, 2]
6 3 3 1
After:  [3, 0, 1, 2]

Before: [2, 2, 3, 0]
3 1 1 0
After:  [1, 2, 3, 0]

Before: [0, 2, 2, 1]
15 3 1 0
After:  [2, 2, 2, 1]

Before: [0, 1, 3, 1]
5 1 0 0
After:  [1, 1, 3, 1]

Before: [0, 1, 0, 3]
5 1 0 0
After:  [1, 1, 0, 3]

Before: [3, 2, 2, 3]
7 1 0 3
After:  [3, 2, 2, 1]

Before: [0, 2, 3, 0]
10 0 0 1
After:  [0, 0, 3, 0]

Before: [0, 1, 1, 0]
4 2 3 0
After:  [4, 1, 1, 0]

Before: [2, 1, 2, 3]
14 0 2 1
After:  [2, 4, 2, 3]

Before: [2, 1, 3, 2]
1 3 1 2
After:  [2, 1, 3, 2]

Before: [2, 2, 2, 1]
1 3 2 0
After:  [3, 2, 2, 1]

Before: [0, 0, 3, 1]
10 0 0 0
After:  [0, 0, 3, 1]

Before: [0, 2, 2, 3]
2 1 2 3
After:  [0, 2, 2, 1]

Before: [2, 1, 0, 3]
12 0 3 2
After:  [2, 1, 0, 3]

Before: [3, 0, 3, 3]
4 2 2 1
After:  [3, 5, 3, 3]

Before: [0, 3, 3, 1]
3 1 2 0
After:  [1, 3, 3, 1]

Before: [2, 0, 0, 3]
12 0 3 1
After:  [2, 0, 0, 3]

Before: [2, 0, 3, 2]
4 2 2 2
After:  [2, 0, 5, 2]

Before: [1, 3, 3, 0]
3 1 2 1
After:  [1, 1, 3, 0]

Before: [1, 1, 2, 2]
4 0 3 0
After:  [4, 1, 2, 2]

Before: [1, 2, 2, 3]
15 0 1 3
After:  [1, 2, 2, 2]

Before: [3, 2, 3, 0]
7 1 0 3
After:  [3, 2, 3, 1]

Before: [0, 1, 2, 0]
10 0 0 2
After:  [0, 1, 0, 0]

Before: [0, 3, 2, 1]
10 0 0 0
After:  [0, 3, 2, 1]

Before: [2, 1, 3, 2]
9 1 3 0
After:  [3, 1, 3, 2]

Before: [1, 1, 3, 0]
13 1 0 2
After:  [1, 1, 1, 0]

Before: [3, 1, 2, 2]
6 3 3 2
After:  [3, 1, 0, 2]

Before: [3, 1, 3, 2]
15 2 3 2
After:  [3, 1, 6, 2]

Before: [3, 2, 2, 2]
7 1 0 1
After:  [3, 1, 2, 2]

Before: [1, 1, 0, 2]
13 1 0 2
After:  [1, 1, 1, 2]

Before: [1, 1, 2, 3]
13 1 0 0
After:  [1, 1, 2, 3]

Before: [1, 1, 2, 1]
9 2 1 2
After:  [1, 1, 3, 1]

Before: [1, 2, 2, 2]
2 1 2 1
After:  [1, 1, 2, 2]

Before: [1, 0, 1, 2]
0 1 0 3
After:  [1, 0, 1, 1]

Before: [1, 2, 2, 2]
6 3 3 0
After:  [0, 2, 2, 2]

Before: [2, 2, 2, 2]
3 1 1 1
After:  [2, 1, 2, 2]

Before: [1, 3, 0, 3]
9 0 3 0
After:  [3, 3, 0, 3]

Before: [0, 2, 2, 1]
2 1 2 1
After:  [0, 1, 2, 1]

Before: [3, 2, 1, 0]
7 1 0 0
After:  [1, 2, 1, 0]

Before: [2, 0, 3, 1]
6 3 3 0
After:  [0, 0, 3, 1]

Before: [2, 2, 2, 2]
6 3 3 1
After:  [2, 0, 2, 2]

Before: [1, 1, 2, 0]
13 1 0 1
After:  [1, 1, 2, 0]

Before: [3, 3, 1, 3]
11 2 0 2
After:  [3, 3, 3, 3]

Before: [2, 2, 3, 0]
3 1 0 2
After:  [2, 2, 1, 0]

Before: [1, 2, 0, 3]
12 0 3 2
After:  [1, 2, 0, 3]

Before: [0, 2, 3, 1]
10 0 0 0
After:  [0, 2, 3, 1]

Before: [1, 0, 2, 2]
0 1 0 3
After:  [1, 0, 2, 1]

Before: [1, 0, 1, 0]
0 1 0 3
After:  [1, 0, 1, 1]

Before: [0, 1, 1, 2]
5 1 0 3
After:  [0, 1, 1, 1]

Before: [0, 3, 0, 2]
11 0 3 2
After:  [0, 3, 2, 2]

Before: [3, 2, 3, 2]
15 2 3 1
After:  [3, 6, 3, 2]

Before: [1, 1, 2, 3]
13 1 0 1
After:  [1, 1, 2, 3]

Before: [3, 1, 0, 1]
11 1 0 2
After:  [3, 1, 3, 1]

Before: [1, 1, 0, 0]
13 1 0 0
After:  [1, 1, 0, 0]

Before: [0, 2, 2, 1]
2 1 2 3
After:  [0, 2, 2, 1]

Before: [3, 1, 2, 1]
1 3 2 3
After:  [3, 1, 2, 3]

Before: [3, 3, 3, 2]
6 3 3 3
After:  [3, 3, 3, 0]

Before: [1, 0, 3, 2]
11 1 2 0
After:  [3, 0, 3, 2]

Before: [1, 0, 1, 2]
0 1 0 1
After:  [1, 1, 1, 2]

Before: [1, 3, 0, 3]
9 0 3 2
After:  [1, 3, 3, 3]

Before: [3, 3, 3, 3]
3 1 2 0
After:  [1, 3, 3, 3]

Before: [3, 2, 1, 1]
7 1 0 3
After:  [3, 2, 1, 1]

Before: [3, 2, 2, 3]
7 1 0 0
After:  [1, 2, 2, 3]

Before: [3, 1, 2, 0]
14 2 2 2
After:  [3, 1, 4, 0]

Before: [0, 0, 1, 2]
10 0 0 0
After:  [0, 0, 1, 2]

Before: [2, 3, 2, 3]
2 1 3 3
After:  [2, 3, 2, 1]

Before: [3, 2, 3, 1]
4 0 3 2
After:  [3, 2, 6, 1]

Before: [2, 1, 0, 2]
6 3 3 3
After:  [2, 1, 0, 0]

Before: [3, 2, 0, 2]
7 1 0 0
After:  [1, 2, 0, 2]

Before: [1, 0, 0, 1]
0 1 0 2
After:  [1, 0, 1, 1]

Before: [1, 1, 0, 0]
4 0 3 2
After:  [1, 1, 4, 0]

Before: [2, 2, 2, 3]
8 2 3 1
After:  [2, 6, 2, 3]

Before: [1, 0, 2, 2]
0 1 0 0
After:  [1, 0, 2, 2]

Before: [0, 2, 1, 0]
10 0 0 1
After:  [0, 0, 1, 0]

Before: [0, 3, 2, 3]
14 2 2 3
After:  [0, 3, 2, 4]

Before: [1, 3, 0, 3]
12 0 3 3
After:  [1, 3, 0, 0]

Before: [3, 2, 3, 3]
7 1 0 0
After:  [1, 2, 3, 3]

Before: [1, 1, 1, 3]
9 2 3 2
After:  [1, 1, 3, 3]

Before: [3, 2, 0, 1]
7 1 0 3
After:  [3, 2, 0, 1]

Before: [0, 2, 1, 3]
11 2 1 1
After:  [0, 3, 1, 3]

Before: [0, 0, 0, 1]
4 3 1 3
After:  [0, 0, 0, 2]

Before: [1, 1, 0, 2]
1 3 1 3
After:  [1, 1, 0, 3]

Before: [0, 1, 2, 1]
5 1 0 2
After:  [0, 1, 1, 1]

Before: [0, 2, 1, 0]
4 1 3 1
After:  [0, 5, 1, 0]

Before: [3, 3, 0, 3]
2 1 3 1
After:  [3, 1, 0, 3]

Before: [1, 0, 2, 1]
0 1 0 0
After:  [1, 0, 2, 1]

Before: [1, 1, 2, 1]
13 1 0 1
After:  [1, 1, 2, 1]

Before: [0, 3, 2, 1]
4 1 1 2
After:  [0, 3, 4, 1]

Before: [0, 3, 0, 3]
9 2 3 2
After:  [0, 3, 3, 3]

Before: [2, 2, 0, 3]
8 1 2 2
After:  [2, 2, 4, 3]

Before: [2, 2, 0, 0]
3 1 1 2
After:  [2, 2, 1, 0]

Before: [0, 1, 2, 3]
5 1 0 1
After:  [0, 1, 2, 3]

Before: [1, 1, 1, 1]
13 1 0 3
After:  [1, 1, 1, 1]

Before: [3, 1, 3, 1]
6 3 3 2
After:  [3, 1, 0, 1]

Before: [0, 2, 0, 2]
6 3 3 2
After:  [0, 2, 0, 2]

Before: [0, 3, 0, 2]
10 0 0 0
After:  [0, 3, 0, 2]

Before: [2, 3, 2, 3]
14 2 2 0
After:  [4, 3, 2, 3]

Before: [1, 2, 1, 0]
3 1 1 0
After:  [1, 2, 1, 0]

Before: [2, 0, 1, 1]
11 0 2 3
After:  [2, 0, 1, 3]

Before: [2, 2, 1, 3]
12 0 3 2
After:  [2, 2, 0, 3]

Before: [0, 2, 0, 3]
8 1 2 3
After:  [0, 2, 0, 4]

Before: [2, 1, 3, 2]
8 3 2 0
After:  [4, 1, 3, 2]

Before: [1, 0, 0, 2]
0 1 0 1
After:  [1, 1, 0, 2]

Before: [1, 1, 1, 0]
13 1 0 2
After:  [1, 1, 1, 0]

Before: [3, 1, 2, 2]
14 2 2 3
After:  [3, 1, 2, 4]

Before: [1, 2, 2, 1]
14 1 2 2
After:  [1, 2, 4, 1]

Before: [3, 3, 2, 3]
2 1 3 0
After:  [1, 3, 2, 3]

Before: [3, 2, 2, 0]
11 3 1 3
After:  [3, 2, 2, 2]

Before: [2, 2, 0, 2]
3 1 1 0
After:  [1, 2, 0, 2]

Before: [1, 3, 3, 2]
4 2 1 3
After:  [1, 3, 3, 4]

Before: [1, 3, 2, 3]
2 1 3 1
After:  [1, 1, 2, 3]

Before: [1, 0, 3, 1]
0 1 0 3
After:  [1, 0, 3, 1]

Before: [1, 0, 1, 3]
0 1 0 0
After:  [1, 0, 1, 3]

Before: [3, 2, 0, 1]
7 1 0 0
After:  [1, 2, 0, 1]

Before: [3, 2, 1, 2]
7 1 0 0
After:  [1, 2, 1, 2]

Before: [0, 2, 2, 2]
2 1 2 1
After:  [0, 1, 2, 2]

Before: [0, 1, 2, 3]
5 1 0 2
After:  [0, 1, 1, 3]

Before: [1, 0, 2, 3]
0 1 0 1
After:  [1, 1, 2, 3]

Before: [0, 1, 0, 2]
5 1 0 3
After:  [0, 1, 0, 1]

Before: [2, 1, 3, 3]
11 1 0 3
After:  [2, 1, 3, 3]

Before: [1, 3, 1, 1]
6 3 3 1
After:  [1, 0, 1, 1]

Before: [3, 0, 3, 1]
1 3 2 3
After:  [3, 0, 3, 3]

Before: [1, 2, 3, 0]
9 0 2 2
After:  [1, 2, 3, 0]

Before: [2, 2, 2, 0]
11 3 0 1
After:  [2, 2, 2, 0]

Before: [2, 2, 2, 1]
14 0 2 3
After:  [2, 2, 2, 4]

Before: [3, 2, 0, 2]
7 1 0 3
After:  [3, 2, 0, 1]

Before: [0, 2, 1, 3]
9 2 3 2
After:  [0, 2, 3, 3]

Before: [1, 1, 2, 2]
9 0 3 2
After:  [1, 1, 3, 2]

Before: [2, 1, 3, 3]
12 0 3 2
After:  [2, 1, 0, 3]

Before: [0, 2, 2, 0]
14 1 2 3
After:  [0, 2, 2, 4]

Before: [2, 2, 0, 3]
12 0 3 2
After:  [2, 2, 0, 3]

Before: [0, 0, 3, 2]
15 2 3 1
After:  [0, 6, 3, 2]

Before: [0, 3, 0, 1]
4 1 3 0
After:  [6, 3, 0, 1]

Before: [2, 2, 2, 3]
12 0 3 0
After:  [0, 2, 2, 3]

Before: [0, 1, 1, 0]
5 1 0 1
After:  [0, 1, 1, 0]

Before: [0, 3, 1, 3]
2 1 3 3
After:  [0, 3, 1, 1]

Before: [1, 1, 2, 0]
14 2 2 2
After:  [1, 1, 4, 0]

Before: [0, 0, 2, 1]
10 0 0 3
After:  [0, 0, 2, 0]

Before: [3, 3, 3, 1]
3 1 2 0
After:  [1, 3, 3, 1]

Before: [3, 2, 1, 1]
11 1 2 3
After:  [3, 2, 1, 3]

Before: [2, 2, 1, 1]
6 1 2 2
After:  [2, 2, 1, 1]

Before: [0, 0, 1, 0]
10 0 0 2
After:  [0, 0, 0, 0]

Before: [3, 3, 3, 3]
3 1 0 0
After:  [1, 3, 3, 3]

Before: [3, 0, 2, 1]
1 3 2 2
After:  [3, 0, 3, 1]

Before: [1, 1, 2, 3]
13 1 0 2
After:  [1, 1, 1, 3]

Before: [2, 1, 1, 1]
6 2 3 0
After:  [0, 1, 1, 1]

Before: [1, 1, 2, 2]
1 3 1 3
After:  [1, 1, 2, 3]

Before: [1, 2, 3, 3]
15 0 1 0
After:  [2, 2, 3, 3]

Before: [0, 1, 2, 2]
5 1 0 3
After:  [0, 1, 2, 1]

Before: [1, 2, 0, 3]
12 0 3 3
After:  [1, 2, 0, 0]

Before: [1, 0, 3, 1]
6 3 3 1
After:  [1, 0, 3, 1]

Before: [3, 0, 1, 2]
15 0 3 2
After:  [3, 0, 6, 2]

Before: [3, 3, 2, 0]
8 1 2 2
After:  [3, 3, 6, 0]

Before: [0, 1, 1, 2]
1 3 1 1
After:  [0, 3, 1, 2]

Before: [2, 1, 3, 3]
9 0 1 1
After:  [2, 3, 3, 3]

Before: [2, 3, 2, 3]
12 0 3 3
After:  [2, 3, 2, 0]

Before: [2, 1, 0, 3]
9 1 3 2
After:  [2, 1, 3, 3]

Before: [3, 0, 2, 1]
9 1 2 2
After:  [3, 0, 2, 1]

Before: [2, 0, 1, 3]
8 0 3 0
After:  [6, 0, 1, 3]

Before: [1, 0, 2, 1]
0 1 0 1
After:  [1, 1, 2, 1]

Before: [2, 1, 2, 3]
8 2 3 3
After:  [2, 1, 2, 6]

Before: [1, 0, 3, 3]
12 0 3 3
After:  [1, 0, 3, 0]

Before: [0, 3, 3, 0]
4 2 2 2
After:  [0, 3, 5, 0]

Before: [0, 3, 3, 3]
10 0 0 3
After:  [0, 3, 3, 0]

Before: [3, 2, 2, 2]
14 2 2 2
After:  [3, 2, 4, 2]

Before: [0, 3, 1, 3]
8 3 3 3
After:  [0, 3, 1, 9]

Before: [0, 2, 0, 1]
3 1 1 3
After:  [0, 2, 0, 1]

Before: [0, 2, 2, 1]
14 2 2 0
After:  [4, 2, 2, 1]

Before: [1, 3, 0, 3]
2 1 3 1
After:  [1, 1, 0, 3]

Before: [2, 3, 2, 3]
2 1 3 2
After:  [2, 3, 1, 3]

Before: [3, 2, 2, 1]
7 1 0 2
After:  [3, 2, 1, 1]

Before: [1, 3, 1, 3]
12 0 3 0
After:  [0, 3, 1, 3]

Before: [1, 1, 2, 3]
8 1 2 1
After:  [1, 2, 2, 3]

Before: [1, 2, 3, 0]
15 0 1 0
After:  [2, 2, 3, 0]

Before: [1, 0, 0, 2]
0 1 0 2
After:  [1, 0, 1, 2]

Before: [0, 2, 2, 3]
3 1 1 0
After:  [1, 2, 2, 3]

Before: [3, 2, 2, 0]
3 1 1 0
After:  [1, 2, 2, 0]

Before: [1, 3, 2, 1]
1 3 2 0
After:  [3, 3, 2, 1]

Before: [1, 1, 3, 0]
13 1 0 1
After:  [1, 1, 3, 0]

Before: [1, 1, 2, 1]
13 1 0 3
After:  [1, 1, 2, 1]

Before: [1, 0, 1, 1]
0 1 0 0
After:  [1, 0, 1, 1]

Before: [2, 3, 3, 2]
4 2 2 3
After:  [2, 3, 3, 5]

Before: [3, 2, 1, 2]
7 1 0 3
After:  [3, 2, 1, 1]

Before: [1, 0, 3, 2]
0 1 0 0
After:  [1, 0, 3, 2]

Before: [0, 2, 0, 3]
10 0 0 1
After:  [0, 0, 0, 3]

Before: [3, 1, 2, 3]
9 1 3 0
After:  [3, 1, 2, 3]

Before: [0, 0, 3, 1]
1 3 2 0
After:  [3, 0, 3, 1]

Before: [0, 3, 2, 1]
1 3 2 1
After:  [0, 3, 2, 1]

Before: [3, 2, 3, 1]
15 3 1 3
After:  [3, 2, 3, 2]

Before: [3, 2, 1, 1]
15 3 1 3
After:  [3, 2, 1, 2]

Before: [2, 0, 0, 3]
12 0 3 0
After:  [0, 0, 0, 3]

Before: [2, 2, 1, 2]
4 2 3 3
After:  [2, 2, 1, 4]

Before: [0, 1, 2, 3]
8 3 3 2
After:  [0, 1, 9, 3]

Before: [1, 2, 2, 3]
12 0 3 3
After:  [1, 2, 2, 0]

Before: [2, 2, 0, 3]
8 1 2 3
After:  [2, 2, 0, 4]

Before: [1, 0, 0, 3]
0 1 0 2
After:  [1, 0, 1, 3]

Before: [0, 2, 0, 2]
10 0 0 2
After:  [0, 2, 0, 2]

Before: [2, 2, 3, 2]
3 1 0 0
After:  [1, 2, 3, 2]

Before: [2, 0, 0, 3]
12 0 3 2
After:  [2, 0, 0, 3]

Before: [2, 0, 2, 3]
8 2 3 3
After:  [2, 0, 2, 6]

Before: [1, 0, 2, 1]
0 1 0 2
After:  [1, 0, 1, 1]

Before: [1, 2, 2, 3]
14 1 2 3
After:  [1, 2, 2, 4]

Before: [0, 3, 1, 3]
11 0 1 2
After:  [0, 3, 3, 3]

Before: [1, 0, 3, 2]
15 2 3 0
After:  [6, 0, 3, 2]

Before: [0, 1, 1, 1]
5 1 0 3
After:  [0, 1, 1, 1]

Before: [0, 1, 3, 2]
5 1 0 1
After:  [0, 1, 3, 2]

Before: [3, 2, 1, 2]
8 1 2 3
After:  [3, 2, 1, 4]

Before: [2, 1, 2, 3]
12 0 3 3
After:  [2, 1, 2, 0]

Before: [0, 1, 0, 0]
5 1 0 2
After:  [0, 1, 1, 0]

Before: [1, 1, 2, 1]
4 0 3 2
After:  [1, 1, 4, 1]

Before: [1, 0, 3, 1]
4 2 2 1
After:  [1, 5, 3, 1]

Before: [1, 1, 3, 1]
13 1 0 1
After:  [1, 1, 3, 1]

Before: [3, 2, 3, 1]
7 1 0 3
After:  [3, 2, 3, 1]

Before: [1, 1, 0, 2]
13 1 0 1
After:  [1, 1, 0, 2]

Before: [3, 2, 0, 3]
7 1 0 3
After:  [3, 2, 0, 1]

Before: [3, 2, 2, 1]
15 3 1 2
After:  [3, 2, 2, 1]

Before: [0, 3, 0, 2]
15 1 3 1
After:  [0, 6, 0, 2]

Before: [3, 2, 0, 3]
7 1 0 0
After:  [1, 2, 0, 3]

Before: [3, 2, 0, 2]
7 1 0 1
After:  [3, 1, 0, 2]

Before: [0, 2, 2, 3]
9 0 3 1
After:  [0, 3, 2, 3]

Before: [0, 2, 2, 1]
11 0 3 1
After:  [0, 1, 2, 1]

Before: [2, 2, 1, 0]
3 1 1 1
After:  [2, 1, 1, 0]

Before: [2, 3, 3, 2]
15 2 3 1
After:  [2, 6, 3, 2]

Before: [0, 2, 3, 2]
10 0 0 0
After:  [0, 2, 3, 2]

Before: [1, 0, 2, 2]
9 0 3 2
After:  [1, 0, 3, 2]

Before: [2, 1, 1, 3]
8 3 3 0
After:  [9, 1, 1, 3]

Before: [1, 2, 3, 3]
9 0 2 0
After:  [3, 2, 3, 3]

Before: [1, 2, 2, 1]
2 1 2 3
After:  [1, 2, 2, 1]

Before: [1, 2, 0, 2]
15 0 1 1
After:  [1, 2, 0, 2]

Before: [0, 2, 3, 0]
10 0 0 3
After:  [0, 2, 3, 0]

Before: [2, 2, 0, 2]
3 1 1 2
After:  [2, 2, 1, 2]

Before: [3, 2, 2, 2]
14 2 2 0
After:  [4, 2, 2, 2]

Before: [0, 0, 3, 2]
15 2 3 0
After:  [6, 0, 3, 2]

Before: [0, 3, 0, 3]
10 0 0 0
After:  [0, 3, 0, 3]

Before: [0, 1, 1, 2]
6 3 3 3
After:  [0, 1, 1, 0]

Before: [1, 2, 0, 3]
11 0 1 3
After:  [1, 2, 0, 3]

Before: [0, 2, 1, 0]
6 1 2 3
After:  [0, 2, 1, 1]

Before: [1, 0, 0, 0]
0 1 0 1
After:  [1, 1, 0, 0]

Before: [3, 3, 1, 1]
6 3 3 2
After:  [3, 3, 0, 1]

Before: [0, 1, 3, 3]
9 0 1 1
After:  [0, 1, 3, 3]

Before: [1, 0, 1, 1]
0 1 0 1
After:  [1, 1, 1, 1]

Before: [1, 1, 2, 0]
13 1 0 0
After:  [1, 1, 2, 0]

Before: [2, 2, 2, 2]
14 2 2 1
After:  [2, 4, 2, 2]

Before: [0, 1, 1, 3]
10 0 0 2
After:  [0, 1, 0, 3]

Before: [0, 2, 1, 1]
10 0 0 3
After:  [0, 2, 1, 0]

Before: [3, 3, 0, 3]
2 1 3 2
After:  [3, 3, 1, 3]

Before: [0, 1, 3, 3]
10 0 0 0
After:  [0, 1, 3, 3]

Before: [1, 0, 3, 3]
0 1 0 2
After:  [1, 0, 1, 3]

Before: [0, 2, 3, 3]
10 0 0 1
After:  [0, 0, 3, 3]

Before: [2, 3, 1, 2]
11 2 1 3
After:  [2, 3, 1, 3]

Before: [3, 3, 3, 2]
4 0 1 2
After:  [3, 3, 4, 2]

Before: [0, 0, 2, 0]
9 0 2 1
After:  [0, 2, 2, 0]

Before: [1, 0, 1, 3]
0 1 0 2
After:  [1, 0, 1, 3]

Before: [1, 2, 0, 1]
15 3 1 0
After:  [2, 2, 0, 1]

Before: [1, 3, 3, 3]
12 0 3 2
After:  [1, 3, 0, 3]

Before: [3, 3, 0, 3]
2 1 3 3
After:  [3, 3, 0, 1]

Before: [0, 3, 1, 2]
11 0 1 1
After:  [0, 3, 1, 2]

Before: [2, 0, 1, 0]
11 0 2 3
After:  [2, 0, 1, 3]

Before: [2, 2, 2, 3]
2 1 2 0
After:  [1, 2, 2, 3]

Before: [2, 0, 3, 1]
1 3 2 1
After:  [2, 3, 3, 1]

Before: [0, 0, 3, 1]
10 0 0 2
After:  [0, 0, 0, 1]

Before: [3, 2, 0, 0]
7 1 0 1
After:  [3, 1, 0, 0]

Before: [1, 1, 0, 3]
13 1 0 3
After:  [1, 1, 0, 1]

Before: [1, 2, 1, 3]
12 0 3 0
After:  [0, 2, 1, 3]

Before: [1, 0, 2, 2]
14 3 2 0
After:  [4, 0, 2, 2]

Before: [2, 2, 2, 1]
14 2 2 0
After:  [4, 2, 2, 1]

Before: [2, 3, 1, 3]
11 2 0 2
After:  [2, 3, 3, 3]

Before: [0, 1, 3, 3]
5 1 0 2
After:  [0, 1, 1, 3]

Before: [3, 3, 3, 3]
2 1 3 3
After:  [3, 3, 3, 1]

Before: [1, 1, 2, 2]
13 1 0 1
After:  [1, 1, 2, 2]

Before: [0, 1, 2, 2]
5 1 0 1
After:  [0, 1, 2, 2]

Before: [1, 0, 1, 1]
4 2 3 3
After:  [1, 0, 1, 4]

Before: [2, 2, 1, 1]
15 3 1 1
After:  [2, 2, 1, 1]

Before: [1, 3, 0, 1]
11 0 1 3
After:  [1, 3, 0, 3]

Before: [3, 3, 2, 3]
8 2 3 3
After:  [3, 3, 2, 6]

Before: [3, 1, 1, 1]
6 2 3 0
After:  [0, 1, 1, 1]

Before: [0, 3, 0, 2]
6 3 3 1
After:  [0, 0, 0, 2]

Before: [0, 2, 3, 1]
15 3 1 0
After:  [2, 2, 3, 1]

Before: [1, 0, 3, 3]
12 0 3 2
After:  [1, 0, 0, 3]

Before: [0, 1, 3, 1]
1 3 2 3
After:  [0, 1, 3, 3]

Before: [2, 1, 2, 2]
9 1 2 3
After:  [2, 1, 2, 3]

Before: [2, 1, 2, 2]
9 0 1 3
After:  [2, 1, 2, 3]

Before: [1, 0, 1, 2]
0 1 0 2
After:  [1, 0, 1, 2]

Before: [1, 0, 1, 0]
0 1 0 2
After:  [1, 0, 1, 0]

Before: [3, 2, 2, 1]
3 1 1 3
After:  [3, 2, 2, 1]

Before: [0, 3, 2, 3]
2 1 3 3
After:  [0, 3, 2, 1]

Before: [1, 2, 3, 3]
6 1 0 1
After:  [1, 1, 3, 3]

Before: [2, 2, 2, 2]
2 1 2 3
After:  [2, 2, 2, 1]

Before: [2, 2, 2, 1]
3 1 1 2
After:  [2, 2, 1, 1]

Before: [1, 1, 0, 2]
13 1 0 0
After:  [1, 1, 0, 2]

Before: [2, 0, 0, 3]
12 0 3 3
After:  [2, 0, 0, 0]

Before: [1, 1, 1, 1]
6 2 3 0
After:  [0, 1, 1, 1]

Before: [3, 2, 1, 3]
11 2 0 0
After:  [3, 2, 1, 3]

Before: [1, 3, 1, 2]
15 1 3 1
After:  [1, 6, 1, 2]

Before: [0, 2, 1, 0]
3 1 1 0
After:  [1, 2, 1, 0]

Before: [1, 1, 1, 3]
12 0 3 3
After:  [1, 1, 1, 0]

Before: [3, 2, 3, 2]
3 1 1 2
After:  [3, 2, 1, 2]

Before: [1, 0, 2, 0]
9 0 2 1
After:  [1, 3, 2, 0]

Before: [3, 3, 1, 2]
8 3 2 0
After:  [4, 3, 1, 2]

Before: [1, 1, 0, 3]
13 1 0 0
After:  [1, 1, 0, 3]

Before: [1, 1, 3, 0]
13 1 0 0
After:  [1, 1, 3, 0]

Before: [3, 0, 2, 3]
8 3 2 3
After:  [3, 0, 2, 6]

Before: [2, 3, 3, 2]
15 2 3 2
After:  [2, 3, 6, 2]

Before: [1, 1, 0, 2]
13 1 0 3
After:  [1, 1, 0, 1]

Before: [0, 1, 2, 1]
1 3 2 2
After:  [0, 1, 3, 1]

Before: [3, 0, 2, 1]
6 3 3 2
After:  [3, 0, 0, 1]

Before: [3, 2, 1, 0]
7 1 0 3
After:  [3, 2, 1, 1]

Before: [3, 2, 1, 2]
15 0 3 1
After:  [3, 6, 1, 2]

Before: [1, 2, 1, 1]
11 1 2 1
After:  [1, 3, 1, 1]

Before: [3, 0, 2, 0]
14 2 2 1
After:  [3, 4, 2, 0]

Before: [0, 2, 3, 1]
1 3 2 3
After:  [0, 2, 3, 3]

Before: [3, 1, 3, 0]
11 1 0 0
After:  [3, 1, 3, 0]

Before: [2, 3, 1, 1]
6 2 3 1
After:  [2, 0, 1, 1]

Before: [3, 2, 0, 0]
7 1 0 2
After:  [3, 2, 1, 0]

Before: [1, 1, 3, 1]
13 1 0 2
After:  [1, 1, 1, 1]

Before: [0, 3, 1, 3]
2 1 3 2
After:  [0, 3, 1, 3]

Before: [0, 1, 2, 2]
5 1 0 2
After:  [0, 1, 1, 2]

Before: [1, 2, 3, 3]
12 0 3 3
After:  [1, 2, 3, 0]

Before: [0, 1, 0, 1]
6 3 3 2
After:  [0, 1, 0, 1]

Before: [2, 2, 3, 1]
4 1 3 3
After:  [2, 2, 3, 5]

Before: [1, 2, 3, 3]
9 0 3 2
After:  [1, 2, 3, 3]

Before: [1, 0, 0, 0]
0 1 0 3
After:  [1, 0, 0, 1]

Before: [2, 1, 2, 1]
4 2 3 2
After:  [2, 1, 5, 1]

Before: [0, 3, 3, 3]
2 1 3 2
After:  [0, 3, 1, 3]

Before: [1, 0, 0, 1]
0 1 0 1
After:  [1, 1, 0, 1]

Before: [2, 2, 3, 1]
1 3 2 2
After:  [2, 2, 3, 1]

Before: [1, 0, 1, 0]
0 1 0 1
After:  [1, 1, 1, 0]

Before: [2, 0, 0, 0]
11 1 0 0
After:  [2, 0, 0, 0]

Before: [2, 0, 1, 1]
8 0 2 1
After:  [2, 4, 1, 1]

Before: [3, 2, 2, 1]
2 1 2 2
After:  [3, 2, 1, 1]

Before: [3, 2, 3, 2]
7 1 0 1
After:  [3, 1, 3, 2]

Before: [3, 3, 3, 3]
4 3 2 3
After:  [3, 3, 3, 5]

Before: [1, 2, 2, 2]
2 1 2 3
After:  [1, 2, 2, 1]

Before: [2, 3, 0, 3]
2 1 3 2
After:  [2, 3, 1, 3]

Before: [0, 3, 2, 1]
11 0 3 0
After:  [1, 3, 2, 1]

Before: [1, 1, 2, 0]
13 1 0 3
After:  [1, 1, 2, 1]

Before: [3, 2, 1, 3]
7 1 0 0
After:  [1, 2, 1, 3]

Before: [0, 1, 3, 2]
5 1 0 0
After:  [1, 1, 3, 2]

Before: [3, 1, 0, 0]
11 1 0 2
After:  [3, 1, 3, 0]

Before: [2, 3, 2, 1]
1 3 2 2
After:  [2, 3, 3, 1]

Before: [0, 1, 1, 0]
5 1 0 3
After:  [0, 1, 1, 1]

Before: [0, 1, 2, 3]
14 2 2 2
After:  [0, 1, 4, 3]

Before: [1, 0, 3, 2]
0 1 0 2
After:  [1, 0, 1, 2]

Before: [3, 1, 1, 1]
6 3 3 1
After:  [3, 0, 1, 1]

Before: [2, 0, 3, 2]
11 1 2 1
After:  [2, 3, 3, 2]

Before: [0, 2, 2, 1]
14 1 2 0
After:  [4, 2, 2, 1]

Before: [0, 1, 1, 2]
10 0 0 3
After:  [0, 1, 1, 0]

Before: [0, 0, 0, 1]
10 0 0 1
After:  [0, 0, 0, 1]

Before: [0, 1, 0, 1]
9 0 1 3
After:  [0, 1, 0, 1]

Before: [0, 2, 0, 0]
10 0 0 3
After:  [0, 2, 0, 0]

Before: [1, 0, 2, 0]
11 2 0 1
After:  [1, 3, 2, 0]

Before: [1, 2, 0, 0]
11 0 1 1
After:  [1, 3, 0, 0]

Before: [1, 1, 2, 1]
13 1 0 0
After:  [1, 1, 2, 1]

Before: [1, 1, 3, 3]
9 1 2 3
After:  [1, 1, 3, 3]

Before: [0, 0, 3, 2]
10 0 0 1
After:  [0, 0, 3, 2]

Before: [1, 3, 1, 3]
12 0 3 1
After:  [1, 0, 1, 3]

Before: [0, 3, 2, 1]
14 2 2 1
After:  [0, 4, 2, 1]

Before: [3, 2, 3, 1]
7 1 0 0
After:  [1, 2, 3, 1]

Before: [0, 2, 2, 0]
3 1 1 0
After:  [1, 2, 2, 0]

Before: [1, 1, 3, 0]
9 0 2 1
After:  [1, 3, 3, 0]

Before: [2, 0, 2, 1]
6 3 3 0
After:  [0, 0, 2, 1]

Before: [2, 0, 2, 1]
1 3 2 1
After:  [2, 3, 2, 1]

Before: [1, 2, 2, 3]
3 1 1 3
After:  [1, 2, 2, 1]

Before: [2, 2, 2, 2]
2 1 2 2
After:  [2, 2, 1, 2]

Before: [1, 3, 1, 3]
2 1 3 0
After:  [1, 3, 1, 3]

Before: [2, 3, 1, 2]
8 3 2 2
After:  [2, 3, 4, 2]

Before: [1, 1, 2, 3]
13 1 0 3
After:  [1, 1, 2, 1]

Before: [0, 2, 1, 3]
4 3 2 0
After:  [5, 2, 1, 3]

Before: [2, 1, 1, 3]
12 0 3 0
After:  [0, 1, 1, 3]

Before: [0, 0, 3, 1]
4 3 3 0
After:  [4, 0, 3, 1]

Before: [2, 0, 3, 2]
15 2 3 1
After:  [2, 6, 3, 2]

Before: [1, 0, 3, 3]
0 1 0 1
After:  [1, 1, 3, 3]

Before: [0, 2, 0, 1]
10 0 0 2
After:  [0, 2, 0, 1]

Before: [2, 1, 2, 2]
1 3 1 2
After:  [2, 1, 3, 2]

Before: [3, 2, 2, 0]
7 1 0 3
After:  [3, 2, 2, 1]

Before: [2, 3, 2, 3]
12 0 3 1
After:  [2, 0, 2, 3]

Before: [0, 0, 3, 0]
10 0 0 2
After:  [0, 0, 0, 0]

Before: [3, 2, 3, 3]
7 1 0 2
After:  [3, 2, 1, 3]

Before: [2, 2, 3, 2]
3 1 1 2
After:  [2, 2, 1, 2]

Before: [1, 0, 3, 1]
4 2 2 2
After:  [1, 0, 5, 1]

Before: [2, 0, 1, 1]
6 3 3 3
After:  [2, 0, 1, 0]

Before: [2, 0, 3, 2]
15 2 3 2
After:  [2, 0, 6, 2]

Before: [1, 2, 1, 3]
12 0 3 3
After:  [1, 2, 1, 0]

Before: [3, 1, 1, 2]
15 0 3 1
After:  [3, 6, 1, 2]

Before: [1, 0, 0, 3]
4 0 1 1
After:  [1, 2, 0, 3]

Before: [0, 3, 0, 0]
10 0 0 0
After:  [0, 3, 0, 0]

Before: [1, 3, 2, 2]
9 0 2 1
After:  [1, 3, 2, 2]

Before: [0, 2, 1, 0]
10 0 0 3
After:  [0, 2, 1, 0]

Before: [0, 1, 0, 1]
6 3 3 3
After:  [0, 1, 0, 0]

Before: [2, 0, 3, 3]
12 0 3 2
After:  [2, 0, 0, 3]

Before: [2, 0, 2, 2]
9 1 2 2
After:  [2, 0, 2, 2]

Before: [0, 1, 0, 1]
5 1 0 1
After:  [0, 1, 0, 1]

Before: [0, 3, 2, 2]
14 3 2 1
After:  [0, 4, 2, 2]

Before: [1, 1, 3, 3]
13 1 0 0
After:  [1, 1, 3, 3]

Before: [3, 2, 2, 1]
8 0 2 2
After:  [3, 2, 6, 1]

Before: [3, 2, 1, 3]
11 2 0 2
After:  [3, 2, 3, 3]

Before: [3, 3, 1, 3]
3 1 0 2
After:  [3, 3, 1, 3]

Before: [2, 0, 0, 1]
6 3 3 3
After:  [2, 0, 0, 0]

Before: [0, 0, 3, 2]
10 0 0 2
After:  [0, 0, 0, 2]

Before: [1, 1, 3, 1]
6 3 3 1
After:  [1, 0, 3, 1]

Before: [0, 0, 2, 2]
10 0 0 3
After:  [0, 0, 2, 0]

Before: [0, 0, 0, 2]
6 3 3 0
After:  [0, 0, 0, 2]

Before: [1, 0, 2, 1]
14 2 2 2
After:  [1, 0, 4, 1]

Before: [1, 1, 1, 2]
13 1 0 3
After:  [1, 1, 1, 1]

Before: [1, 3, 1, 3]
11 2 1 2
After:  [1, 3, 3, 3]

Before: [2, 1, 1, 2]
1 3 1 1
After:  [2, 3, 1, 2]

Before: [3, 0, 1, 0]
11 1 0 1
After:  [3, 3, 1, 0]

Before: [0, 2, 3, 2]
11 0 2 0
After:  [3, 2, 3, 2]

Before: [1, 0, 1, 3]
12 0 3 2
After:  [1, 0, 0, 3]

Before: [1, 1, 0, 0]
13 1 0 1
After:  [1, 1, 0, 0]

Before: [0, 1, 2, 0]
5 1 0 2
After:  [0, 1, 1, 0]

Before: [3, 1, 0, 2]
15 0 3 2
After:  [3, 1, 6, 2]

Before: [2, 0, 1, 0]
8 0 2 3
After:  [2, 0, 1, 4]

Before: [3, 1, 1, 2]
1 3 1 2
After:  [3, 1, 3, 2]

Before: [1, 2, 0, 1]
15 3 1 3
After:  [1, 2, 0, 2]

Before: [2, 1, 3, 2]
9 1 3 3
After:  [2, 1, 3, 3]

Before: [0, 3, 0, 1]
10 0 0 1
After:  [0, 0, 0, 1]

Before: [3, 0, 3, 1]
1 3 2 2
After:  [3, 0, 3, 1]

Before: [2, 0, 3, 1]
4 0 3 1
After:  [2, 5, 3, 1]

Before: [2, 3, 2, 1]
14 0 2 2
After:  [2, 3, 4, 1]

Before: [3, 2, 2, 3]
3 1 1 1
After:  [3, 1, 2, 3]

Before: [0, 1, 0, 3]
5 1 0 2
After:  [0, 1, 1, 3]

Before: [1, 3, 1, 3]
12 0 3 3
After:  [1, 3, 1, 0]

Before: [1, 1, 1, 3]
13 1 0 0
After:  [1, 1, 1, 3]

Before: [0, 0, 3, 0]
10 0 0 1
After:  [0, 0, 3, 0]

Before: [2, 2, 0, 2]
3 1 1 1
After:  [2, 1, 0, 2]

Before: [0, 0, 0, 0]
10 0 0 3
After:  [0, 0, 0, 0]

Before: [0, 1, 2, 3]
14 2 2 1
After:  [0, 4, 2, 3]

Before: [0, 1, 2, 1]
1 3 2 0
After:  [3, 1, 2, 1]

Before: [1, 2, 0, 1]
15 0 1 3
After:  [1, 2, 0, 2]

Before: [1, 3, 2, 3]
8 1 2 0
After:  [6, 3, 2, 3]

Before: [2, 2, 2, 2]
14 0 2 3
After:  [2, 2, 2, 4]

Before: [3, 0, 2, 2]
14 3 2 2
After:  [3, 0, 4, 2]

Before: [3, 2, 0, 1]
6 3 3 2
After:  [3, 2, 0, 1]

Before: [1, 1, 2, 3]
8 3 2 2
After:  [1, 1, 6, 3]

Before: [0, 1, 0, 3]
10 0 0 1
After:  [0, 0, 0, 3]

Before: [3, 3, 0, 2]
15 1 3 0
After:  [6, 3, 0, 2]

Before: [1, 0, 0, 0]
0 1 0 2
After:  [1, 0, 1, 0]

Before: [1, 3, 3, 3]
2 1 3 0
After:  [1, 3, 3, 3]

Before: [3, 3, 2, 1]
3 1 0 2
After:  [3, 3, 1, 1]

Before: [3, 2, 2, 1]
7 1 0 3
After:  [3, 2, 2, 1]

Before: [3, 2, 2, 0]
9 3 2 3
After:  [3, 2, 2, 2]

Before: [1, 1, 1, 3]
12 0 3 2
After:  [1, 1, 0, 3]

Before: [2, 2, 0, 3]
12 0 3 0
After:  [0, 2, 0, 3]

Before: [3, 1, 2, 1]
1 3 2 0
After:  [3, 1, 2, 1]

Before: [3, 2, 0, 3]
9 2 3 2
After:  [3, 2, 3, 3]

Before: [1, 2, 0, 0]
15 0 1 0
After:  [2, 2, 0, 0]

Before: [1, 2, 1, 2]
6 1 0 3
After:  [1, 2, 1, 1]

Before: [2, 0, 3, 1]
1 3 2 2
After:  [2, 0, 3, 1]

Before: [0, 0, 2, 3]
8 3 3 3
After:  [0, 0, 2, 9]

Before: [3, 2, 3, 0]
11 3 1 1
After:  [3, 2, 3, 0]

Before: [2, 2, 2, 2]
3 1 0 1
After:  [2, 1, 2, 2]

Before: [1, 2, 0, 1]
15 3 1 1
After:  [1, 2, 0, 1]

Before: [3, 2, 2, 0]
14 2 2 3
After:  [3, 2, 2, 4]

Before: [0, 0, 0, 1]
6 3 3 0
After:  [0, 0, 0, 1]

Before: [1, 0, 3, 0]
0 1 0 3
After:  [1, 0, 3, 1]

Before: [3, 2, 3, 2]
7 1 0 2
After:  [3, 2, 1, 2]

Before: [0, 3, 3, 3]
3 1 2 3
After:  [0, 3, 3, 1]

Before: [1, 0, 1, 2]
8 3 2 2
After:  [1, 0, 4, 2]

Before: [0, 2, 1, 3]
10 0 0 2
After:  [0, 2, 0, 3]

Before: [2, 3, 2, 2]
14 3 2 3
After:  [2, 3, 2, 4]

Before: [3, 0, 2, 1]
1 3 2 0
After:  [3, 0, 2, 1]

Before: [0, 0, 2, 1]
6 3 3 1
After:  [0, 0, 2, 1]

Before: [0, 1, 1, 2]
9 0 1 0
After:  [1, 1, 1, 2]

Before: [3, 2, 0, 2]
7 1 0 2
After:  [3, 2, 1, 2]

Before: [2, 2, 1, 2]
4 2 3 0
After:  [4, 2, 1, 2]

Before: [0, 2, 3, 2]
3 1 1 0
After:  [1, 2, 3, 2]

Before: [3, 1, 3, 1]
4 2 3 0
After:  [6, 1, 3, 1]

Before: [2, 3, 0, 1]
6 3 3 0
After:  [0, 3, 0, 1]

Before: [2, 2, 3, 1]
4 2 3 2
After:  [2, 2, 6, 1]

Before: [0, 1, 2, 1]
1 3 2 1
After:  [0, 3, 2, 1]

Before: [2, 2, 2, 3]
2 1 2 3
After:  [2, 2, 2, 1]

Before: [3, 0, 1, 2]
8 3 2 1
After:  [3, 4, 1, 2]

Before: [2, 2, 3, 3]
3 1 1 3
After:  [2, 2, 3, 1]

Before: [3, 2, 2, 2]
7 1 0 3
After:  [3, 2, 2, 1]

Before: [3, 2, 1, 2]
4 0 1 1
After:  [3, 4, 1, 2]

Before: [1, 0, 0, 2]
6 3 3 0
After:  [0, 0, 0, 2]

Before: [2, 3, 2, 1]
1 3 2 1
After:  [2, 3, 2, 1]

Before: [3, 2, 1, 0]
7 1 0 2
After:  [3, 2, 1, 0]

Before: [1, 1, 0, 0]
13 1 0 3
After:  [1, 1, 0, 1]

Before: [3, 2, 2, 2]
2 1 2 0
After:  [1, 2, 2, 2]

Before: [0, 2, 3, 2]
4 3 3 1
After:  [0, 5, 3, 2]

Before: [0, 0, 3, 3]
9 0 3 0
After:  [3, 0, 3, 3]

Before: [2, 2, 3, 0]
8 1 2 3
After:  [2, 2, 3, 4]

Before: [1, 2, 1, 3]
3 1 1 1
After:  [1, 1, 1, 3]

Before: [0, 1, 1, 3]
5 1 0 3
After:  [0, 1, 1, 1]

Before: [1, 3, 2, 1]
14 2 2 0
After:  [4, 3, 2, 1]

Before: [1, 0, 1, 2]
0 1 0 0
After:  [1, 0, 1, 2]

Before: [0, 1, 2, 1]
11 0 3 1
After:  [0, 1, 2, 1]

Before: [3, 3, 3, 2]
15 1 3 0
After:  [6, 3, 3, 2]

Before: [1, 0, 3, 1]
0 1 0 1
After:  [1, 1, 3, 1]

Before: [3, 3, 2, 1]
4 3 1 0
After:  [2, 3, 2, 1]

Before: [2, 2, 1, 3]
3 1 1 1
After:  [2, 1, 1, 3]

Before: [2, 3, 0, 2]
4 3 3 1
After:  [2, 5, 0, 2]

Before: [1, 1, 0, 2]
1 3 1 2
After:  [1, 1, 3, 2]

Before: [3, 2, 2, 2]
7 1 0 2
After:  [3, 2, 1, 2]

Before: [3, 2, 3, 3]
7 1 0 1
After:  [3, 1, 3, 3]

Before: [3, 0, 0, 2]
6 3 3 3
After:  [3, 0, 0, 0]

Before: [2, 3, 1, 3]
2 1 3 2
After:  [2, 3, 1, 3]

Before: [2, 2, 0, 3]
12 0 3 3
After:  [2, 2, 0, 0]

Before: [2, 3, 1, 3]
2 1 3 1
After:  [2, 1, 1, 3]

Before: [0, 3, 3, 3]
2 1 3 0
After:  [1, 3, 3, 3]

Before: [1, 1, 1, 2]
13 1 0 1
After:  [1, 1, 1, 2]

Before: [2, 0, 3, 1]
6 3 3 3
After:  [2, 0, 3, 0]

Before: [1, 2, 1, 1]
11 2 1 2
After:  [1, 2, 3, 1]

Before: [1, 3, 0, 2]
9 0 3 0
After:  [3, 3, 0, 2]

Before: [1, 3, 3, 3]
8 3 3 2
After:  [1, 3, 9, 3]

Before: [3, 3, 3, 2]
3 1 2 2
After:  [3, 3, 1, 2]

Before: [3, 2, 3, 3]
3 1 1 3
After:  [3, 2, 3, 1]

Before: [3, 3, 2, 2]
14 3 2 2
After:  [3, 3, 4, 2]

Before: [0, 2, 3, 0]
3 1 1 1
After:  [0, 1, 3, 0]

Before: [0, 1, 3, 1]
10 0 0 2
After:  [0, 1, 0, 1]

Before: [0, 0, 0, 3]
10 0 0 3
After:  [0, 0, 0, 0]

Before: [2, 0, 2, 3]
14 2 2 0
After:  [4, 0, 2, 3]

Before: [1, 0, 3, 2]
4 3 3 0
After:  [5, 0, 3, 2]

Before: [0, 1, 2, 1]
5 1 0 3
After:  [0, 1, 2, 1]

Before: [0, 3, 2, 3]
2 1 3 1
After:  [0, 1, 2, 3]

Before: [3, 1, 2, 0]
14 2 2 3
After:  [3, 1, 2, 4]

Before: [1, 3, 0, 3]
11 0 1 0
After:  [3, 3, 0, 3]

Before: [3, 2, 2, 3]
7 1 0 2
After:  [3, 2, 1, 3]

Before: [2, 2, 1, 3]
8 0 3 2
After:  [2, 2, 6, 3]

Before: [0, 2, 3, 1]
10 0 0 2
After:  [0, 2, 0, 1]

Before: [1, 0, 2, 1]
1 3 2 0
After:  [3, 0, 2, 1]

Before: [3, 2, 2, 1]
2 1 2 1
After:  [3, 1, 2, 1]

Before: [3, 2, 1, 3]
7 1 0 1
After:  [3, 1, 1, 3]

Before: [1, 1, 2, 1]
1 3 2 1
After:  [1, 3, 2, 1]

Before: [0, 1, 3, 2]
5 1 0 2
After:  [0, 1, 1, 2]

Before: [3, 1, 0, 3]
4 0 2 1
After:  [3, 5, 0, 3]

Before: [1, 0, 3, 2]
0 1 0 1
After:  [1, 1, 3, 2]

Before: [0, 2, 0, 1]
10 0 0 0
After:  [0, 2, 0, 1]

Before: [1, 1, 0, 1]
13 1 0 0
After:  [1, 1, 0, 1]

Before: [1, 0, 3, 3]
4 2 2 1
After:  [1, 5, 3, 3]

Before: [0, 1, 2, 2]
10 0 0 3
After:  [0, 1, 2, 0]

Before: [2, 2, 3, 1]
4 2 2 2
After:  [2, 2, 5, 1]

Before: [1, 3, 1, 3]
2 1 3 1
After:  [1, 1, 1, 3]

Before: [0, 1, 3, 3]
5 1 0 1
After:  [0, 1, 3, 3]

Before: [0, 0, 1, 1]
10 0 0 0
After:  [0, 0, 1, 1]

Before: [2, 0, 2, 3]
12 0 3 2
After:  [2, 0, 0, 3]

Before: [0, 2, 3, 1]
3 1 1 2
After:  [0, 2, 1, 1]

Before: [0, 3, 1, 0]
4 1 2 3
After:  [0, 3, 1, 5]

Before: [1, 2, 1, 3]
6 1 0 3
After:  [1, 2, 1, 1]

Before: [0, 1, 0, 0]
5 1 0 3
After:  [0, 1, 0, 1]

Before: [0, 0, 3, 2]
11 1 2 2
After:  [0, 0, 3, 2]

Before: [1, 1, 3, 3]
12 0 3 3
After:  [1, 1, 3, 0]

Before: [2, 3, 1, 2]
11 2 1 2
After:  [2, 3, 3, 2]

Before: [1, 1, 3, 3]
12 0 3 2
After:  [1, 1, 0, 3]

Before: [1, 2, 2, 2]
2 1 2 2
After:  [1, 2, 1, 2]

Before: [2, 3, 2, 2]
15 1 3 0
After:  [6, 3, 2, 2]

Before: [0, 0, 2, 1]
4 3 1 2
After:  [0, 0, 2, 1]

Before: [3, 3, 1, 3]
2 1 3 3
After:  [3, 3, 1, 1]

Before: [0, 1, 1, 3]
11 0 2 0
After:  [1, 1, 1, 3]

Before: [1, 1, 1, 3]
13 1 0 1
After:  [1, 1, 1, 3]

Before: [1, 3, 2, 1]
14 2 2 2
After:  [1, 3, 4, 1]

Before: [0, 1, 2, 2]
5 1 0 0
After:  [1, 1, 2, 2]

Before: [2, 2, 0, 0]
3 1 0 0
After:  [1, 2, 0, 0]

Before: [0, 2, 3, 1]
10 0 0 3
After:  [0, 2, 3, 0]

Before: [1, 3, 2, 3]
12 0 3 1
After:  [1, 0, 2, 3]

Before: [1, 3, 3, 1]
1 3 2 1
After:  [1, 3, 3, 1]

Before: [0, 2, 0, 3]
4 3 2 2
After:  [0, 2, 5, 3]

Before: [1, 2, 2, 1]
14 2 2 3
After:  [1, 2, 2, 4]

Before: [2, 3, 3, 1]
3 1 2 0
After:  [1, 3, 3, 1]

Before: [3, 2, 3, 2]
7 1 0 3
After:  [3, 2, 3, 1]

Before: [1, 0, 3, 2]
11 1 3 0
After:  [2, 0, 3, 2]

Before: [3, 0, 2, 1]
8 3 2 1
After:  [3, 2, 2, 1]

Before: [2, 1, 3, 2]
15 2 3 3
After:  [2, 1, 3, 6]

Before: [1, 2, 2, 0]
2 1 2 2
After:  [1, 2, 1, 0]

Before: [3, 3, 0, 2]
3 1 0 2
After:  [3, 3, 1, 2]

Before: [2, 2, 1, 1]
6 1 2 0
After:  [1, 2, 1, 1]

Before: [1, 0, 3, 3]
0 1 0 0
After:  [1, 0, 3, 3]

Before: [1, 1, 1, 0]
13 1 0 0
After:  [1, 1, 1, 0]

Before: [2, 2, 2, 1]
1 3 2 2
After:  [2, 2, 3, 1]

Before: [2, 2, 2, 1]
2 1 2 2
After:  [2, 2, 1, 1]

Before: [3, 1, 1, 1]
6 3 3 2
After:  [3, 1, 0, 1]

Before: [3, 1, 3, 3]
9 1 3 1
After:  [3, 3, 3, 3]

Before: [0, 2, 3, 2]
10 0 0 2
After:  [0, 2, 0, 2]

Before: [0, 3, 1, 1]
11 0 2 3
After:  [0, 3, 1, 1]

Before: [1, 0, 3, 1]
1 3 2 2
After:  [1, 0, 3, 1]

Before: [2, 1, 2, 2]
1 3 1 1
After:  [2, 3, 2, 2]

Before: [3, 2, 2, 3]
2 1 2 0
After:  [1, 2, 2, 3]

Before: [1, 0, 0, 2]
0 1 0 0
After:  [1, 0, 0, 2]

Before: [0, 1, 0, 1]
10 0 0 0
After:  [0, 1, 0, 1]

Before: [1, 0, 0, 3]
12 0 3 3
After:  [1, 0, 0, 0]

Before: [1, 1, 0, 3]
13 1 0 1
After:  [1, 1, 0, 3]

Before: [1, 0, 0, 3]
0 1 0 0
After:  [1, 0, 0, 3]

Before: [3, 2, 1, 3]
7 1 0 3
After:  [3, 2, 1, 1]

Before: [0, 3, 3, 3]
2 1 3 3
After:  [0, 3, 3, 1]

Before: [3, 2, 1, 2]
7 1 0 1
After:  [3, 1, 1, 2]

Before: [3, 2, 0, 1]
4 3 3 0
After:  [4, 2, 0, 1]

Before: [3, 2, 1, 0]
6 1 2 2
After:  [3, 2, 1, 0]

Before: [0, 1, 1, 2]
5 1 0 1
After:  [0, 1, 1, 2]

Before: [3, 3, 3, 3]
2 1 3 1
After:  [3, 1, 3, 3]

Before: [1, 3, 3, 2]
15 2 3 2
After:  [1, 3, 6, 2]

Before: [3, 2, 3, 1]
15 3 1 2
After:  [3, 2, 2, 1]

Before: [3, 3, 0, 3]
4 1 1 2
After:  [3, 3, 4, 3]

Before: [0, 0, 2, 3]
9 0 3 1
After:  [0, 3, 2, 3]

Before: [1, 3, 3, 1]
1 3 2 2
After:  [1, 3, 3, 1]

Before: [3, 0, 2, 2]
15 0 3 0
After:  [6, 0, 2, 2]

Before: [0, 1, 3, 3]
9 0 1 3
After:  [0, 1, 3, 1]

Before: [1, 3, 3, 2]
4 1 1 3
After:  [1, 3, 3, 4]

Before: [1, 0, 2, 2]
0 1 0 1
After:  [1, 1, 2, 2]

Before: [1, 2, 2, 1]
15 3 1 0
After:  [2, 2, 2, 1]

Before: [0, 1, 2, 1]
5 1 0 1
After:  [0, 1, 2, 1]

Before: [1, 2, 1, 1]
15 0 1 0
After:  [2, 2, 1, 1]

Before: [1, 1, 0, 3]
13 1 0 2
After:  [1, 1, 1, 3]

Before: [2, 1, 2, 3]
14 0 2 0
After:  [4, 1, 2, 3]

Before: [1, 3, 2, 3]
8 2 3 3
After:  [1, 3, 2, 6]

Before: [0, 1, 1, 1]
9 0 1 1
After:  [0, 1, 1, 1]

Before: [2, 0, 1, 2]
8 3 2 3
After:  [2, 0, 1, 4]

Before: [1, 0, 1, 1]
0 1 0 3
After:  [1, 0, 1, 1]

Before: [2, 3, 1, 1]
6 3 3 3
After:  [2, 3, 1, 0]

Before: [3, 2, 3, 3]
7 1 0 3
After:  [3, 2, 3, 1]

Before: [1, 3, 3, 3]
3 1 2 1
After:  [1, 1, 3, 3]

Before: [0, 1, 1, 3]
5 1 0 2
After:  [0, 1, 1, 3]

Before: [3, 3, 1, 3]
2 1 3 1
After:  [3, 1, 1, 3]

Before: [0, 1, 2, 0]
5 1 0 0
After:  [1, 1, 2, 0]

Before: [2, 1, 2, 0]
9 1 2 0
After:  [3, 1, 2, 0]

Before: [0, 0, 3, 1]
10 0 0 1
After:  [0, 0, 3, 1]

Before: [0, 1, 0, 2]
1 3 1 1
After:  [0, 3, 0, 2]

Before: [3, 0, 1, 0]
11 1 0 2
After:  [3, 0, 3, 0]

Before: [1, 2, 3, 1]
6 1 0 3
After:  [1, 2, 3, 1]

Before: [1, 0, 3, 0]
0 1 0 0
After:  [1, 0, 3, 0]

Before: [0, 2, 1, 0]
11 0 1 3
After:  [0, 2, 1, 2]

Before: [1, 1, 1, 0]
13 1 0 3
After:  [1, 1, 1, 1]

Before: [1, 2, 3, 1]
1 3 2 3
After:  [1, 2, 3, 3]

Before: [0, 1, 3, 0]
5 1 0 2
After:  [0, 1, 1, 0]

Before: [3, 0, 2, 2]
6 3 3 0
After:  [0, 0, 2, 2]

Before: [1, 2, 3, 1]
6 3 3 3
After:  [1, 2, 3, 0]

Before: [3, 2, 0, 1]
7 1 0 2
After:  [3, 2, 1, 1]

Before: [1, 1, 3, 2]
13 1 0 1
After:  [1, 1, 3, 2]

Before: [0, 0, 1, 2]
10 0 0 2
After:  [0, 0, 0, 2]

Before: [3, 2, 2, 2]
6 3 3 0
After:  [0, 2, 2, 2]

Before: [1, 0, 2, 0]
0 1 0 0
After:  [1, 0, 2, 0]

Before: [1, 0, 2, 2]
9 0 2 3
After:  [1, 0, 2, 3]

Before: [2, 0, 3, 3]
12 0 3 1
After:  [2, 0, 3, 3]

Before: [2, 3, 0, 3]
2 1 3 1
After:  [2, 1, 0, 3]

Before: [1, 3, 2, 2]
6 3 3 3
After:  [1, 3, 2, 0]

Before: [3, 1, 1, 1]
4 3 3 0
After:  [4, 1, 1, 1]

Before: [2, 1, 1, 0]
8 0 2 2
After:  [2, 1, 4, 0]

Before: [1, 0, 2, 3]
0 1 0 3
After:  [1, 0, 2, 1]

Before: [1, 2, 2, 1]
4 3 3 1
After:  [1, 4, 2, 1]

Before: [0, 3, 2, 1]
10 0 0 3
After:  [0, 3, 2, 0]

Before: [0, 1, 0, 2]
5 1 0 0
After:  [1, 1, 0, 2]

Before: [0, 1, 1, 1]
5 1 0 0
After:  [1, 1, 1, 1]

Before: [1, 2, 3, 3]
12 0 3 2
After:  [1, 2, 0, 3]

Before: [1, 3, 3, 1]
3 1 2 1
After:  [1, 1, 3, 1]

Before: [1, 0, 3, 0]
0 1 0 2
After:  [1, 0, 1, 0]

Before: [3, 2, 3, 0]
7 1 0 1
After:  [3, 1, 3, 0]

Before: [1, 1, 1, 1]
13 1 0 0
After:  [1, 1, 1, 1]

Before: [0, 0, 0, 3]
9 1 3 1
After:  [0, 3, 0, 3]

Before: [3, 1, 0, 0]
11 2 0 2
After:  [3, 1, 3, 0]

Before: [0, 0, 1, 3]
11 0 2 0
After:  [1, 0, 1, 3]

Before: [3, 3, 3, 2]
4 2 1 1
After:  [3, 4, 3, 2]

Before: [0, 3, 2, 3]
2 1 3 0
After:  [1, 3, 2, 3]

Before: [1, 1, 1, 3]
9 0 3 2
After:  [1, 1, 3, 3]

Before: [2, 2, 1, 1]
15 3 1 3
After:  [2, 2, 1, 2]

Before: [3, 2, 1, 3]
7 1 0 2
After:  [3, 2, 1, 3]

Before: [1, 1, 0, 1]
13 1 0 2
After:  [1, 1, 1, 1]

Before: [3, 2, 2, 3]
3 1 1 0
After:  [1, 2, 2, 3]

Before: [0, 1, 2, 3]
5 1 0 0
After:  [1, 1, 2, 3]

Before: [0, 1, 3, 3]
5 1 0 3
After:  [0, 1, 3, 1]

Before: [0, 3, 1, 2]
6 3 3 0
After:  [0, 3, 1, 2]

Before: [0, 2, 3, 1]
8 1 2 2
After:  [0, 2, 4, 1]

Before: [1, 0, 2, 3]
0 1 0 2
After:  [1, 0, 1, 3]

Before: [2, 1, 1, 3]
9 1 3 1
After:  [2, 3, 1, 3]

Before: [2, 3, 3, 3]
12 0 3 1
After:  [2, 0, 3, 3]

Before: [1, 2, 3, 3]
3 1 1 3
After:  [1, 2, 3, 1]

Before: [1, 2, 0, 2]
15 0 1 3
After:  [1, 2, 0, 2]

Before: [1, 0, 0, 0]
0 1 0 0
After:  [1, 0, 0, 0]

Before: [0, 1, 1, 1]
6 2 3 0
After:  [0, 1, 1, 1]

Before: [3, 2, 2, 0]
7 1 0 1
After:  [3, 1, 2, 0]

Before: [2, 2, 0, 3]
8 0 3 1
After:  [2, 6, 0, 3]

Before: [1, 1, 0, 3]
12 0 3 2
After:  [1, 1, 0, 3]

Before: [2, 3, 2, 3]
12 0 3 0
After:  [0, 3, 2, 3]

Before: [2, 0, 1, 1]
6 3 3 1
After:  [2, 0, 1, 1]

Before: [1, 2, 0, 1]
4 1 3 0
After:  [5, 2, 0, 1]

Before: [0, 3, 2, 3]
10 0 0 3
After:  [0, 3, 2, 0]

Before: [2, 1, 2, 3]
9 1 2 2
After:  [2, 1, 3, 3]

Before: [2, 1, 2, 0]
11 1 0 2
After:  [2, 1, 3, 0]

Before: [0, 2, 2, 2]
2 1 2 0
After:  [1, 2, 2, 2]

Before: [1, 3, 2, 3]
8 0 2 2
After:  [1, 3, 2, 3]

Before: [1, 0, 0, 3]
9 2 3 1
After:  [1, 3, 0, 3]

Before: [1, 0, 2, 2]
0 1 0 2
After:  [1, 0, 1, 2]

Before: [0, 1, 1, 2]
1 3 1 0
After:  [3, 1, 1, 2]

Before: [3, 1, 3, 1]
1 3 2 3
After:  [3, 1, 3, 3]

Before: [0, 1, 0, 3]
4 3 2 2
After:  [0, 1, 5, 3]

Before: [1, 2, 2, 3]
8 3 3 0
After:  [9, 2, 2, 3]

Before: [1, 2, 2, 2]
14 3 2 2
After:  [1, 2, 4, 2]

Before: [0, 3, 0, 3]
2 1 3 2
After:  [0, 3, 1, 3]

Before: [2, 2, 0, 0]
3 1 0 2
After:  [2, 2, 1, 0]

Before: [1, 2, 3, 2]
9 0 2 1
After:  [1, 3, 3, 2]

Before: [0, 3, 2, 2]
8 1 2 0
After:  [6, 3, 2, 2]

Before: [1, 3, 2, 3]
2 1 3 0
After:  [1, 3, 2, 3]

Before: [1, 0, 2, 1]
1 3 2 1
After:  [1, 3, 2, 1]

Before: [0, 2, 2, 3]
10 0 0 1
After:  [0, 0, 2, 3]

Before: [0, 2, 2, 3]
2 1 2 1
After:  [0, 1, 2, 3]

Before: [3, 1, 2, 1]
14 2 2 1
After:  [3, 4, 2, 1]

Before: [1, 0, 0, 1]
4 0 3 3
After:  [1, 0, 0, 4]

Before: [1, 1, 0, 2]
8 3 2 2
After:  [1, 1, 4, 2]

Before: [0, 1, 1, 1]
4 1 3 1
After:  [0, 4, 1, 1]

Before: [1, 3, 1, 2]
15 1 3 3
After:  [1, 3, 1, 6]

Before: [2, 2, 2, 2]
6 3 3 0
After:  [0, 2, 2, 2]

Before: [1, 2, 3, 3]
8 1 3 1
After:  [1, 6, 3, 3]

Before: [3, 2, 2, 1]
14 2 2 1
After:  [3, 4, 2, 1]

Before: [3, 1, 2, 3]
8 1 2 3
After:  [3, 1, 2, 2]

Before: [2, 0, 3, 3]
4 2 1 0
After:  [4, 0, 3, 3]

Before: [0, 1, 1, 3]
5 1 0 0
After:  [1, 1, 1, 3]

Before: [1, 2, 2, 1]
15 3 1 2
After:  [1, 2, 2, 1]

Before: [2, 3, 3, 3]
4 3 1 1
After:  [2, 4, 3, 3]

Before: [1, 3, 2, 1]
14 2 2 1
After:  [1, 4, 2, 1]

Before: [0, 1, 2, 3]
5 1 0 3
After:  [0, 1, 2, 1]

Before: [1, 1, 1, 2]
13 1 0 2
After:  [1, 1, 1, 2]

Before: [3, 2, 2, 0]
7 1 0 0
After:  [1, 2, 2, 0]

Before: [0, 1, 0, 2]
5 1 0 2
After:  [0, 1, 1, 2]

Before: [1, 0, 1, 3]
0 1 0 3
After:  [1, 0, 1, 1]

Before: [0, 3, 0, 3]
2 1 3 3
After:  [0, 3, 0, 1]

Before: [0, 2, 2, 1]
10 0 0 1
After:  [0, 0, 2, 1]

Before: [2, 2, 2, 0]
14 1 2 3
After:  [2, 2, 2, 4]

Before: [3, 1, 3, 2]
15 2 3 3
After:  [3, 1, 3, 6]

Before: [1, 1, 2, 0]
13 1 0 2
After:  [1, 1, 1, 0]

Before: [1, 0, 1, 1]
0 1 0 2
After:  [1, 0, 1, 1]

Before: [3, 2, 1, 2]
7 1 0 2
After:  [3, 2, 1, 2]

Before: [2, 3, 3, 3]
12 0 3 2
After:  [2, 3, 0, 3]

Before: [0, 1, 3, 3]
10 0 0 3
After:  [0, 1, 3, 0]

Before: [3, 1, 2, 2]
1 3 1 2
After:  [3, 1, 3, 2]

Before: [1, 3, 3, 1]
6 3 3 2
After:  [1, 3, 0, 1]

Before: [0, 2, 3, 0]
10 0 0 2
After:  [0, 2, 0, 0]

Before: [0, 2, 1, 3]
10 0 0 3
After:  [0, 2, 1, 0]

Before: [2, 1, 3, 2]
4 1 3 2
After:  [2, 1, 4, 2]

Before: [1, 0, 2, 0]
0 1 0 1
After:  [1, 1, 2, 0]

Before: [1, 3, 0, 3]
4 1 2 3
After:  [1, 3, 0, 5]

Before: [0, 1, 0, 3]
5 1 0 1
After:  [0, 1, 0, 3]

Before: [2, 3, 2, 1]
1 3 2 3
After:  [2, 3, 2, 3]

Before: [3, 2, 1, 1]
7 1 0 0
After:  [1, 2, 1, 1]

Before: [2, 3, 2, 0]
14 0 2 0
After:  [4, 3, 2, 0]

Before: [1, 0, 0, 3]
9 0 3 1
After:  [1, 3, 0, 3]

Before: [0, 3, 2, 1]
14 2 2 3
After:  [0, 3, 2, 4]

Before: [0, 1, 0, 1]
5 1 0 0
After:  [1, 1, 0, 1]

Before: [1, 3, 3, 2]
15 2 3 0
After:  [6, 3, 3, 2]

Before: [1, 1, 3, 0]
4 2 3 1
After:  [1, 6, 3, 0]

Before: [1, 2, 0, 1]
15 0 1 2
After:  [1, 2, 2, 1]

Before: [0, 1, 2, 0]
5 1 0 1
After:  [0, 1, 2, 0]

Before: [1, 2, 3, 1]
15 0 1 3
After:  [1, 2, 3, 2]

Before: [1, 1, 2, 2]
13 1 0 2
After:  [1, 1, 1, 2]

Before: [3, 2, 2, 2]
7 1 0 0
After:  [1, 2, 2, 2]

Before: [1, 1, 3, 1]
1 3 2 3
After:  [1, 1, 3, 3]

Before: [1, 0, 2, 0]
0 1 0 2
After:  [1, 0, 1, 0]

Before: [2, 2, 3, 3]
12 0 3 2
After:  [2, 2, 0, 3]

Before: [1, 0, 2, 3]
0 1 0 0
After:  [1, 0, 2, 3]

Before: [0, 1, 0, 3]
5 1 0 3
After:  [0, 1, 0, 1]

Before: [1, 2, 3, 0]
15 0 1 1
After:  [1, 2, 3, 0]

Before: [1, 2, 2, 0]
14 1 2 2
After:  [1, 2, 4, 0]";

        string _input2 = @"1 0 2 3
1 2 1 0
1 2 3 2
9 0 3 3
8 3 2 3
14 3 1 1
5 1 2 0
1 0 1 3
1 2 2 1
12 3 2 3
8 3 1 3
14 3 0 0
5 0 2 2
8 0 0 0
4 0 2 0
1 0 3 3
1 1 0 1
9 0 3 3
8 3 1 3
14 2 3 2
1 2 1 3
1 0 3 1
3 0 3 0
8 0 1 0
14 2 0 2
1 2 0 1
1 2 0 0
1 3 2 3
13 3 0 3
8 3 2 3
14 3 2 2
5 2 0 1
1 1 0 3
1 3 2 2
1 0 3 0
14 3 3 0
8 0 1 0
14 1 0 1
1 0 0 0
1 1 1 2
14 3 3 0
8 0 1 0
14 0 1 1
5 1 3 0
1 2 3 1
1 3 2 3
13 3 1 1
8 1 2 1
8 1 2 1
14 1 0 0
5 0 0 3
1 2 1 1
1 3 0 0
11 1 0 0
8 0 3 0
14 0 3 3
5 3 3 2
1 1 3 3
1 3 2 0
13 0 1 0
8 0 3 0
14 0 2 2
1 0 1 3
1 3 1 1
8 1 0 0
4 0 1 0
14 0 0 3
8 3 1 3
14 3 2 2
5 2 3 1
1 0 0 0
1 0 3 3
1 2 3 2
12 3 2 0
8 0 2 0
8 0 3 0
14 0 1 1
5 1 2 2
1 0 1 1
1 1 3 0
1 1 2 3
4 3 1 3
8 3 2 3
8 3 1 3
14 2 3 2
5 2 3 3
1 0 1 2
1 1 0 1
8 0 2 1
8 1 2 1
14 3 1 3
5 3 3 0
1 1 1 3
1 1 2 1
8 0 0 2
4 2 3 2
8 3 2 3
8 3 1 3
14 0 3 0
5 0 2 1
1 1 2 0
1 1 2 3
14 0 0 3
8 3 3 3
14 3 1 1
5 1 1 0
8 1 0 2
4 2 0 2
1 2 2 3
1 1 3 1
15 1 3 1
8 1 1 1
8 1 3 1
14 1 0 0
5 0 1 1
1 2 3 2
8 0 0 0
4 0 1 0
5 0 2 3
8 3 2 3
8 3 2 3
14 1 3 1
5 1 1 2
1 1 1 3
1 2 3 0
1 3 3 1
13 1 0 3
8 3 2 3
8 3 3 3
14 3 2 2
5 2 1 3
1 0 1 1
1 1 0 0
1 1 3 2
4 0 1 1
8 1 2 1
8 1 2 1
14 1 3 3
1 3 2 2
1 1 3 1
1 2 2 0
11 0 2 2
8 2 2 2
8 2 1 2
14 3 2 3
5 3 3 0
1 3 2 2
1 1 1 3
14 1 3 1
8 1 1 1
14 1 0 0
5 0 2 2
1 2 3 1
8 0 0 0
4 0 2 0
6 0 3 1
8 1 2 1
14 2 1 2
5 2 3 1
1 1 1 0
1 3 1 3
1 1 1 2
10 3 2 2
8 2 3 2
14 2 1 1
1 2 3 3
8 2 0 2
4 2 0 2
1 0 2 0
1 3 2 0
8 0 1 0
14 1 0 1
5 1 1 0
1 2 2 2
1 0 0 3
8 0 0 1
4 1 0 1
12 3 2 3
8 3 1 3
14 3 0 0
5 0 1 1
1 1 1 3
1 3 2 2
1 2 1 0
15 3 0 2
8 2 2 2
14 1 2 1
8 1 0 2
4 2 0 2
1 3 3 0
8 2 0 3
4 3 3 3
2 2 0 3
8 3 1 3
14 1 3 1
5 1 3 0
8 3 0 1
4 1 3 1
1 0 3 3
1 3 2 1
8 1 2 1
8 1 2 1
14 0 1 0
5 0 0 3
8 3 0 0
4 0 3 0
1 3 2 2
1 2 0 1
11 1 2 1
8 1 3 1
14 3 1 3
5 3 0 1
1 2 0 0
1 1 1 3
1 0 1 2
6 0 3 3
8 3 2 3
14 1 3 1
5 1 0 3
1 2 3 1
1 3 3 0
1 3 0 2
11 1 0 0
8 0 3 0
14 3 0 3
5 3 3 0
1 3 1 1
1 0 3 3
0 3 2 1
8 1 2 1
14 0 1 0
5 0 2 1
8 3 0 0
4 0 1 0
8 1 0 3
4 3 2 3
1 2 0 3
8 3 1 3
14 1 3 1
1 3 0 3
1 0 2 2
1 2 1 0
1 2 3 0
8 0 3 0
14 0 1 1
5 1 3 2
1 1 2 0
1 1 3 3
1 3 3 1
4 0 1 3
8 3 1 3
14 2 3 2
8 0 0 0
4 0 2 0
8 3 0 1
4 1 2 1
1 1 3 3
15 3 0 1
8 1 2 1
8 1 2 1
14 2 1 2
5 2 0 1
1 2 2 3
1 3 2 2
11 0 2 0
8 0 1 0
8 0 1 0
14 0 1 1
5 1 0 3
1 2 1 1
1 2 3 0
2 0 2 1
8 1 2 1
14 3 1 3
5 3 1 2
1 0 1 3
1 3 1 1
9 0 3 3
8 3 2 3
8 3 1 3
14 3 2 2
5 2 1 0
1 2 1 2
1 2 0 3
1 1 1 1
15 1 3 3
8 3 2 3
14 0 3 0
5 0 2 3
1 0 2 2
1 3 1 0
1 2 0 1
8 1 1 1
8 1 1 1
14 1 3 3
5 3 2 1
1 0 2 3
1 3 0 2
1 1 1 0
0 3 2 3
8 3 3 3
14 1 3 1
5 1 0 3
1 3 0 0
1 0 1 1
10 0 2 2
8 2 3 2
14 3 2 3
5 3 1 2
1 2 3 0
8 0 0 3
4 3 1 3
1 3 1 1
4 3 1 1
8 1 3 1
14 2 1 2
5 2 2 0
1 3 0 2
1 0 1 3
1 2 0 1
0 3 2 3
8 3 2 3
14 0 3 0
5 0 0 1
1 2 2 0
1 0 1 3
0 3 2 2
8 2 1 2
14 2 1 1
1 1 1 3
1 3 1 0
1 0 0 2
2 2 0 2
8 2 1 2
14 1 2 1
1 0 0 3
1 2 2 2
7 2 0 0
8 0 1 0
14 0 1 1
1 2 2 3
1 3 1 0
1 0 3 2
0 2 3 2
8 2 1 2
14 1 2 1
1 1 0 0
1 0 1 2
0 2 3 0
8 0 2 0
14 1 0 1
5 1 3 2
8 0 0 0
4 0 2 0
1 1 2 1
1 1 0 3
15 1 0 3
8 3 3 3
8 3 2 3
14 3 2 2
5 2 2 3
1 0 2 1
1 1 2 2
1 1 0 0
4 0 1 0
8 0 2 0
14 0 3 3
5 3 1 1
1 1 1 0
1 1 2 3
14 3 3 2
8 2 2 2
14 2 1 1
1 2 3 2
14 3 0 3
8 3 1 3
14 3 1 1
5 1 2 3
8 2 0 2
4 2 1 2
1 3 1 1
1 0 0 0
10 1 2 2
8 2 3 2
8 2 2 2
14 3 2 3
5 3 0 2
8 3 0 3
4 3 1 3
1 2 1 0
1 0 0 1
14 3 3 0
8 0 1 0
14 2 0 2
5 2 2 1
1 2 0 2
1 1 2 0
1 2 2 3
15 0 3 2
8 2 2 2
8 2 3 2
14 1 2 1
1 0 3 2
1 3 2 0
2 2 0 3
8 3 3 3
14 3 1 1
5 1 2 2
1 1 2 1
8 2 0 0
4 0 1 0
1 1 3 3
1 3 1 1
8 1 3 1
14 1 2 2
5 2 1 1
8 2 0 3
4 3 0 3
8 1 0 0
4 0 0 0
8 0 0 2
4 2 3 2
1 2 3 2
8 2 3 2
14 2 1 1
1 2 2 0
1 2 3 2
12 3 2 2
8 2 1 2
14 2 1 1
1 1 1 0
1 0 3 2
8 2 0 3
4 3 2 3
8 0 2 3
8 3 2 3
14 1 3 1
5 1 2 3
1 3 1 2
1 2 3 1
11 1 2 0
8 0 2 0
8 0 2 0
14 0 3 3
5 3 3 0
1 1 2 3
1 3 0 1
1 1 1 2
14 3 3 2
8 2 3 2
14 2 0 0
5 0 1 3
1 3 0 0
1 0 0 2
1 0 3 1
2 2 0 2
8 2 2 2
14 3 2 3
5 3 1 0
1 1 1 3
1 1 0 2
8 0 0 1
4 1 2 1
14 3 3 3
8 3 3 3
14 0 3 0
5 0 1 2
1 3 0 0
8 1 0 1
4 1 0 1
8 1 0 3
4 3 2 3
1 1 3 0
8 0 1 0
8 0 2 0
14 0 2 2
1 2 3 0
1 1 1 3
6 0 3 1
8 1 3 1
14 1 2 2
5 2 3 1
8 0 0 2
4 2 2 2
6 0 3 3
8 3 1 3
14 1 3 1
1 1 0 0
1 2 0 3
5 0 2 0
8 0 3 0
8 0 2 0
14 1 0 1
5 1 0 3
1 2 1 1
1 3 0 0
11 2 0 2
8 2 1 2
14 2 3 3
5 3 2 1
1 2 3 0
1 1 0 3
1 0 0 2
15 3 0 0
8 0 2 0
8 0 1 0
14 1 0 1
5 1 0 3
1 3 2 2
1 2 3 1
8 1 0 0
4 0 3 0
10 0 2 2
8 2 3 2
8 2 2 2
14 3 2 3
5 3 2 0
1 0 2 3
1 0 2 1
8 1 0 2
4 2 3 2
0 3 2 3
8 3 1 3
14 3 0 0
8 0 0 1
4 1 3 1
1 2 0 2
1 0 0 3
12 3 2 1
8 1 2 1
14 0 1 0
1 3 1 2
8 2 0 1
4 1 2 1
11 1 2 2
8 2 2 2
14 0 2 0
5 0 3 2
1 2 2 3
1 2 1 0
3 0 3 1
8 1 1 1
14 1 2 2
5 2 1 3
1 0 3 2
1 1 2 0
8 2 0 1
4 1 3 1
4 0 1 2
8 2 2 2
8 2 1 2
14 2 3 3
5 3 0 2
1 2 2 0
1 2 0 3
1 0 1 1
1 1 3 0
8 0 3 0
14 2 0 2
5 2 2 3
1 2 3 1
1 3 3 2
1 2 0 0
11 0 2 1
8 1 3 1
8 1 1 1
14 3 1 3
5 3 1 0
1 2 3 1
1 1 2 3
1 2 3 1
8 1 2 1
14 1 0 0
8 1 0 1
4 1 1 1
1 2 0 3
15 1 3 1
8 1 3 1
14 0 1 0
5 0 3 1
8 1 0 3
4 3 1 3
1 0 0 0
8 3 2 2
8 2 2 2
14 1 2 1
8 3 0 2
4 2 0 2
1 2 0 3
1 2 2 0
3 0 3 3
8 3 2 3
8 3 1 3
14 3 1 1
1 1 1 0
1 2 1 2
8 3 0 3
4 3 3 3
5 0 2 3
8 3 3 3
14 1 3 1
5 1 1 0
1 0 3 3
1 3 1 1
12 3 2 2
8 2 1 2
14 2 0 0
5 0 3 2
1 2 1 0
1 2 2 3
8 3 0 1
4 1 1 1
3 0 3 0
8 0 2 0
14 2 0 2
5 2 3 1
1 1 2 2
8 3 0 3
4 3 3 3
8 0 0 0
4 0 1 0
10 3 2 0
8 0 1 0
8 0 3 0
14 0 1 1
5 1 0 2
1 2 1 1
1 1 1 0
8 0 0 3
4 3 2 3
15 0 3 3
8 3 3 3
14 3 2 2
5 2 0 3
1 1 1 1
1 0 1 2
8 3 0 0
4 0 3 0
2 2 0 2
8 2 2 2
8 2 3 2
14 3 2 3
1 2 2 2
11 2 0 2
8 2 2 2
14 3 2 3
5 3 1 1
1 2 3 0
1 3 0 2
1 1 2 3
2 0 2 3
8 3 2 3
14 1 3 1
5 1 1 3
1 1 1 1
1 0 2 0
8 1 2 2
8 2 1 2
14 3 2 3
5 3 1 2
1 2 2 0
1 2 2 3
3 0 3 1
8 1 2 1
14 1 2 2
5 2 1 0
1 0 2 2
8 3 0 1
4 1 1 1
0 2 3 3
8 3 1 3
14 0 3 0
5 0 1 2
1 1 1 3
1 2 3 0
8 2 0 1
4 1 3 1
7 0 1 0
8 0 1 0
14 0 2 2
1 3 2 0
8 2 0 1
4 1 2 1
11 1 0 0
8 0 3 0
14 2 0 2
5 2 2 3
1 0 0 2
8 3 0 0
4 0 3 0
13 0 1 2
8 2 2 2
14 3 2 3
5 3 2 2
1 0 1 3
8 3 0 0
4 0 1 0
1 0 3 1
4 0 1 0
8 0 3 0
14 2 0 2
5 2 1 3
1 2 0 2
1 3 0 0
8 3 0 1
4 1 2 1
7 2 0 2
8 2 3 2
14 2 3 3
5 3 0 1
8 1 0 0
4 0 1 0
1 2 2 2
8 2 0 3
4 3 0 3
12 3 2 2
8 2 2 2
8 2 3 2
14 1 2 1
5 1 0 2
8 1 0 1
4 1 3 1
4 0 1 3
8 3 2 3
14 2 3 2
5 2 2 3
1 2 2 1
8 2 0 0
4 0 0 0
1 3 0 2
1 2 0 2
8 2 2 2
14 2 3 3
1 1 2 0
8 3 0 2
4 2 2 2
5 0 2 1
8 1 1 1
8 1 2 1
14 1 3 3
5 3 1 1
1 3 3 0
1 0 0 3
12 3 2 0
8 0 1 0
14 0 1 1
8 0 0 3
4 3 2 3
1 2 1 0
1 3 0 2
3 0 3 0
8 0 3 0
14 1 0 1
5 1 1 0
1 0 1 2
1 3 0 3
1 1 3 1
8 1 2 3
8 3 2 3
8 3 1 3
14 0 3 0
5 0 2 1
1 3 3 2
1 1 2 3
1 3 3 0
10 0 2 0
8 0 2 0
14 1 0 1
5 1 3 2
1 1 0 1
1 0 0 3
8 3 0 0
4 0 0 0
1 3 1 3
8 3 1 3
14 2 3 2
8 2 0 3
4 3 0 3
1 2 0 0
15 1 0 1
8 1 2 1
14 2 1 2
5 2 3 1
1 1 1 3
1 2 0 2
1 3 1 0
7 2 0 3
8 3 2 3
8 3 3 3
14 1 3 1
5 1 3 0
1 1 0 1
1 2 2 3
1 0 1 2
8 1 2 2
8 2 2 2
14 2 0 0
1 3 3 1
1 3 0 2
1 1 2 3
4 3 1 1
8 1 3 1
8 1 1 1
14 1 0 0
5 0 0 3
1 2 0 1
1 1 0 0
8 0 2 0
8 0 1 0
14 0 3 3
5 3 2 2
1 3 0 1
1 3 0 3
1 1 1 0
4 0 1 0
8 0 1 0
14 2 0 2
1 1 0 1
1 0 0 3
1 2 2 0
15 1 0 0
8 0 2 0
14 2 0 2
5 2 1 3
1 3 0 2
1 2 2 1
1 2 1 0
11 0 2 0
8 0 1 0
14 3 0 3
5 3 1 0
1 2 1 2
1 2 2 3
1 3 2 1
7 2 1 2
8 2 3 2
8 2 1 2
14 2 0 0
5 0 3 3
8 1 0 1
4 1 0 1
1 1 1 0
1 2 1 2
5 0 2 2
8 2 2 2
8 2 1 2
14 2 3 3
5 3 1 0
1 1 2 3
1 0 1 2
1 3 0 1
8 3 2 3
8 3 3 3
14 3 0 0
5 0 2 2
1 1 3 0
1 1 0 3
1 2 2 1
14 3 0 0
8 0 3 0
14 2 0 2
1 3 3 1
1 1 2 0
1 0 0 3
4 0 1 0
8 0 3 0
14 0 2 2
5 2 0 0
1 0 1 2
8 0 0 3
4 3 1 3
8 3 2 1
8 1 1 1
14 1 0 0
1 0 0 1
1 2 0 3
1 2 0 2
9 2 3 2
8 2 3 2
14 2 0 0
5 0 0 1
1 3 0 0
1 3 0 2
1 0 3 3
0 3 2 0
8 0 2 0
8 0 2 0
14 0 1 1
5 1 3 2
1 1 0 0
8 1 0 1
4 1 1 1
1 3 3 3
14 1 0 0
8 0 3 0
14 2 0 2
5 2 0 1
1 2 1 2
1 3 0 0
1 2 1 3
7 2 0 0
8 0 2 0
14 0 1 1
5 1 0 2
1 1 0 3
1 3 3 1
1 2 1 0
15 3 0 0
8 0 3 0
8 0 3 0
14 0 2 2
5 2 1 3
1 3 2 2
1 1 3 1
1 0 0 0
8 1 2 1
8 1 2 1
14 1 3 3
8 3 0 0
4 0 2 0
1 2 1 2
8 3 0 1
4 1 3 1
7 0 1 2
8 2 3 2
14 3 2 3
5 3 2 0
8 0 0 3
4 3 0 3
1 2 0 2
8 3 0 1
4 1 1 1
12 3 2 2
8 2 2 2
14 2 0 0
1 0 1 1
1 1 0 3
1 2 0 2
4 3 1 2
8 2 1 2
8 2 2 2
14 2 0 0
5 0 0 3
8 3 0 2
4 2 3 2
1 2 1 0
2 0 2 0
8 0 3 0
14 0 3 3
1 2 2 0
2 0 2 2
8 2 2 2
8 2 1 2
14 3 2 3
5 3 0 1
1 2 0 3
1 2 2 2
9 0 3 3
8 3 1 3
14 3 1 1
5 1 3 3
1 3 3 1
1 1 3 2
10 1 2 2
8 2 2 2
8 2 1 2
14 3 2 3
5 3 0 0";
    }
}
