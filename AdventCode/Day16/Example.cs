using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day16
{
    public class Example
    {
        public int[] Before { get; private set; }

        public int[] After { get; private set; }

        public int OperationId { get; set; }

        public int RegA { get; set; }

        public int RegB { get; set; }

        public int RegC { get; set; }

        public List<Operation> PossibleOperations { get; set; }

        public int PossibleOperationsCount { get { return PossibleOperations.Count; } }

        public Example(string input)
        {
            var splits = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            int i = 0;
            string before = splits[0].Split('[')[1].TrimEnd(']');
            Before = new int[4];
            foreach (string split in before.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
            {
                Before[i++] = int.Parse(split);
            }

            i = 0;
            string after = splits[2].Split('[')[1].TrimEnd(']');
            After = new int[4];
            foreach (string split in after.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
            {
                After[i++] = int.Parse(split);
            }

            var operation = splits[1].Split(' ');
            OperationId = int.Parse(operation[0]);
            RegA = int.Parse(operation[1]);
            RegB = int.Parse(operation[2]);
            RegC = int.Parse(operation[3]);

            PossibleOperations = new List<Operation>();
        }

        public Example(int opId, int regA, int regB, int regC, int[] register)
        {
            OperationId = opId;
            RegA = regA;
            RegB = regB;
            RegC = regC;
            Before = (int[])register.Clone();
        }
    }
}
