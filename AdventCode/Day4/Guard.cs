using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day4
{
    public class Guard
    {
        public int Number { get; private set; }

        public List<DayOfService> ServiceDays { get; private set; }

        private int _minutesAsleep = -1;

        public int MinutesAsleep
        {
            get
            {
                if (_minutesAsleep == -1)
                {
                    _minutesAsleep = 0;
                    foreach (var day in ServiceDays)
                    {
                        _minutesAsleep += day.Asleep.Sum();
                    }
                }
                return _minutesAsleep;
            }
        }

        private int _mostSleepingMinute = -1;

        public int MostSleepingMinute
        {
            get
            {
                if (_mostSleepingMinute == -1)
                {
                    var minutes = new Dictionary<int, int>();
                    for (int i = 0; i < 60; i++)
                    {
                        minutes[i] = 1;
                    }

                    foreach (var day in ServiceDays)
                    {
                        for (int i = 0; i < day.Asleep.Length; i++)
                        {
                            minutes[i] += day.Asleep[i];
                        }
                    }
                    _mostSleepingMinuteCount = minutes.Values.Max();
                    _mostSleepingMinute = minutes.Where(m => m.Value == _mostSleepingMinuteCount).Select(m => m.Key).First();
                }
                return _mostSleepingMinute;
            }
        }

        private int _mostSleepingMinuteCount = -1;

        public int MostSleepingMinuteCount
        {
            get
            {
                if (_mostSleepingMinuteCount == -1)
                {
                    int a = MostSleepingMinute;
                }
                return _mostSleepingMinuteCount;
            }
        }

        public Guard(int number)
        {
            Number = number;
            ServiceDays = new List<DayOfService>();
        }
    }
}
