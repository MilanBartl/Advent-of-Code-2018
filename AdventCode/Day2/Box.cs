using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day2
{
    public class Box
    {
        public bool HasDouble { get; private set; }

        public bool HasTriple { get; private set; }

        public string Id { get; private set; }

        public Box(string id)
        {
            Id = id;

            var letters = new Dictionary<char, int>();
            foreach (char letter in Id)
            {
                if (letters.ContainsKey(letter))
                    letters[letter]++;
                else
                    letters.Add(letter, 1);
            }

            if (letters.Values.Contains(2))
                HasDouble = true;
            if (letters.Values.Contains(3))
                HasTriple = true;
        }

        public bool Compare(Box other)
        {
            int diffs = 0;
            for (int i = 0; i < Id.Length && diffs < 3; i++)
            {
                if (Id[i] != other.Id[i])
                    diffs++;
            }
            return !(diffs > 1);
        }
    }
}
