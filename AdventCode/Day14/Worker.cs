using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day14
{
    public class Worker
    {
        private LinkedList<int> _scores = new LinkedList<int>();

        private LinkedListNode<int> elf1, elf2;

        bool first, second, third, fourth, fifth, sixth;

        public Worker()
        {
            _scores.AddFirst(3);
            _scores.AddLast(7);

            elf1 = _scores.First;
            elf2 = _scores.Last;

            first = second = third = fourth = fifth = sixth = false;
        }

        public string Work1()
        {
            MakeRecipes();

            while (_scores.Count < _input + 10)
            {
                MakeRecipes();
                MoveElves();
            }

            string output = "";
            foreach (int digit in _scores.Skip(_input))
            {
                output += digit.ToString();
            }
            return output;
        }

        public int Work2()
        {
            MakeRecipes();

            while (!sixth)
            {
                MakeRecipes();
                MoveElves();
            }

            return _scores.Count - 6;
        }

        private void CheckScoreSequence(int added)
        {
            if (!first && added == _input / 100000 % 10)
                first = true;
            else if (first && !second && added == _input / 10000 % 10)
                second = true;
            else if (first && second && !third && added == _input / 1000 % 10)
                third = true;
            else if (first && second && third && !fourth && added == _input / 100 % 10)
                fourth = true;
            else if (first && second && third && fourth && !fifth && added == _input / 10 % 10)
                fifth = true;
            else if (first && second && third && fourth && fifth && !sixth && added == _input % 10)
                sixth = true;
            else
                first = second = third = fourth = fifth = sixth = false;
        }

        private void MakeRecipes()
        {
            int sum = elf1.Value + elf2.Value;

            if (sum >= 10)
            {
                _scores.AddLast(1);
                CheckScoreSequence(1);

                if (sixth)
                    return;

                _scores.AddLast(sum % 10);
                CheckScoreSequence(sum % 10);
            }
            else
            {
                _scores.AddLast(sum);
                CheckScoreSequence(sum);
            }
        }

        private void MoveElves()
        {
            int elf1Count = elf1.Value + 1;
            int elf2Count = elf2.Value + 1;

            for (int i = 0; i < elf1Count; i++)
            {
                if (elf1 == _scores.Last)
                {
                    elf1 = _scores.First;
                }
                else
                    elf1 = elf1.Next;
            }

            for (int i = 0; i < elf2Count; i++)
            {
                if (elf2 == _scores.Last)
                {
                    elf2 = _scores.First;
                }
                else
                    elf2 = elf2.Next;
            }
        }

        private int _input = 598701;

        private int _testInput = 9;

        private int _testInput2 = 515891;
    }
}
