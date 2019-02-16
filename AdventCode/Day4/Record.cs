using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day4
{
    public class Record
    {
        public DateTime Time { get; private set; }

        public bool IsSleeping { get; private set; }

        public int GuardBeginShift { get; private set; } 

        public Record(string input)
        {
            var splits = input.Split(']');

            string dt = splits[0].Substring(1);
            string action = splits[1];

            splits = dt.Split(' ');
            string date = splits[0];
            string time = splits[1];

            splits = date.Split('-');
            string year = splits[0];
            string month = splits[1];
            string day = splits[2];

            splits = time.Split(':');
            string hour = splits[0];
            string minute = splits[1];

            Time = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minute), 0);

            if (action.Contains('#'))
            {
                GuardBeginShift = int.Parse(action.Split('#')[1].Split(' ')[0]);
                IsSleeping = false;
            }
            else if (action.Contains('w'))
                IsSleeping = false;
            else
                IsSleeping = true;
        }

    }
}
