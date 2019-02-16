using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day7
{
    public class Step
    {
        public char Id { get; set; }

        public List<Step> Before { get; set; }

        public List<Step> After { get; private set; }

        public int RequiredTime {  get { return Id - 4; } }

        public bool IsWorking { get; set; }

        public bool IsProcessed { get; set; }

        public Step(char id)
        {
            Id = id;
            Before = new List<Step>();
            After = new List<Step>();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Step;
            if (other == null)
                return false;
            else
                return other.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
