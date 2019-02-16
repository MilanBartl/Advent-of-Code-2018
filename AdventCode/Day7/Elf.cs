using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day7
{
    public class Elf
    {
        public int Id { get; private set; }

        private int _timeToFinishTask = 0;

        private Step _currentTask;

        public bool IsIdle { get { return _timeToFinishTask == 0; } }

        public Elf(int id)
        {
            Id = id;
        }

        public void AssignTask(Step task)
        {
            _currentTask = task;
            _timeToFinishTask = task.RequiredTime;
        }

        public Step CompleteTask()
        {
            var completed = _currentTask;
            _currentTask = null;
            return completed;
        }

        public bool Work()
        {
            if (!IsIdle)
            {
                _timeToFinishTask--;
                return IsIdle;
            }
            else
                return false;
        }
    }
}
