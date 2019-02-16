using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventCode.Day18;

namespace AdventCode.Day19
{
    public class Worker
    {
        private List<Operation> _operations = new List<Operation>();

        private List<Input> _inputs = new List<Input>();

        private int _ipIndex;

        private int _ip = 0;

        public Worker()
        {
            var splits = _input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            _ipIndex = int.Parse(splits[0].Split(' ')[1]);

            foreach (string split in splits.Skip(1))
            {
                _inputs.Add(new Input(split));
            }

            InitOps();
        }

        public int Work1()
        {
            int[] register = new int[6] { 0, 0, 0, 0, 0, 0 };

            do
            {
                register[_ipIndex] = _ip;

                var input = _inputs[_ip];
                input.Register = register;

                register = _operations.First(o => o.Name == input.OperationName).Compute(input);

                _ip = register[_ipIndex];
                _ip++;
            } while (_ip < _inputs.Count);

            return register[0];
        }

        public int Work2()
        {
            throw new NotImplementedException();
        }

        private int CalculateLoopingRegValue()
        {
            bool second = false;
            bool third = false;
            int step = 1;

            int sum = 0;

            for (int i = 1; i < 10551265; i += step)
            {
                sum += i;

                if (second && third)
                {
                    second = false;
                    third = false;
                    step *= 2;
                }
                else if (!second)
                    second = true;
                else if (!third)
                    third = true;
            }

            return sum;
        }

        private void InitOps()
        {
            _operations.Add(
                new Operation("addr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] + output[input.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("addi")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] + input.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("mulr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] * output[input.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("muli")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] * input.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("banr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] & output[input.RegB];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("bani")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] & input.RegB;
                        return output;
                    }
                });
            _operations.Add(
               new Operation("borr")
               {
                   Compute = (Input input) =>
                   {
                       int[] output = (int[])input.Register.Clone();
                       output[input.RegC] = output[input.RegA] | output[input.RegB];
                       return output;
                   }
               });
            _operations.Add(
                new Operation("bori")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] | input.RegB;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("setr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA];
                        return output;
                    }
                });
            _operations.Add(
                new Operation("seti")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = input.RegA;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtir")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = input.RegA > output[input.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtri")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] > input.RegB ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("gtrr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] > output[input.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqir")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = input.RegA == output[input.RegB] ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqri")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] == input.RegB ? 1 : 0;
                        return output;
                    }
                });
            _operations.Add(
                new Operation("eqrr")
                {
                    Compute = (Input input) =>
                    {
                        int[] output = (int[])input.Register.Clone();
                        output[input.RegC] = output[input.RegA] == output[input.RegB] ? 1 : 0;
                        return output;
                    }
                });
        }

        string _testInput = @"#ip 0
seti 5 0 1
seti 6 0 2
addi 0 1 0
addr 1 2 3
setr 1 0 0
seti 8 0 4
seti 9 0 5";

        string _input = @"#ip 4
addi 4 16 4
seti 1 1 1
seti 1 7 3
mulr 1 3 2
eqrr 2 5 2
addr 2 4 4
addi 4 1 4
addr 1 0 0
addi 3 1 3
gtrr 3 5 2
addr 4 2 4
seti 2 3 4
addi 1 1 1
gtrr 1 5 2
addr 2 4 4
seti 1 6 4
mulr 4 4 4
addi 5 2 5
mulr 5 5 5
mulr 4 5 5
muli 5 11 5
addi 2 1 2
mulr 2 4 2
addi 2 6 2
addr 5 2 5
addr 4 0 4
seti 0 0 4
setr 4 5 2
mulr 2 4 2
addr 4 2 2
mulr 4 2 2
muli 2 14 2
mulr 2 4 2
addr 5 2 5
seti 0 5 0
seti 0 2 4";
    }
}
