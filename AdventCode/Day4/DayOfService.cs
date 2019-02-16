using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day4
{
    public class DayOfService
    {
        public DateTime Day { get; private set; }

        public int[] Asleep { get; private set; }

        public DayOfService(List<Record> records)
        {
            if (records[0].Time.Hour == 23)
                records[0].Time.AddMinutes(60 - records[0].Time.Minute);
            else
                records[0].Time.AddMinutes(-records[0].Time.Minute);

            Day = records[0].Time.Date;
            Asleep = new int[60];

            Record current = records[0];
            Record next = null;
            if (records.Count > 2)
                next = records[1];

            int r = 0;
            int endi = 0;
            for (int i = 0; i < Asleep.Length; i++)
            {
                if (i!= 0 && next != null && i == next.Time.Minute)
                {
                    if (r + 2 > records.Count - 1)
                    {
                        endi = i;
                        break;
                    }
                    else
                    {
                        r++;
                        current = next;
                        next = records[r + 1];
                    }
                }

                Asleep[i] = current.IsSleeping ? 1 : 0;
            }

            if (next != null)
            {
                current = next;
                for (int i = endi; i < Asleep.Length; i++)
                {
                    Asleep[i] = current.IsSleeping ? 1 : 0;
                }
            }
        }
    }
}
