using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode.Day7
{
    public class Worker
    {
        private List<Step> _steps = new List<Step>();

        public Worker()
        {
            var splits = _input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string split in splits)
            {
                string pattern = "step [A-Z]";
                var match = Regex.Match(split, pattern, RegexOptions.IgnoreCase);

                char stepId = match.Value.Split(' ')[1][0];
                match = match.NextMatch();
                char afterStepId = match.Value.Split(' ')[1][0];

                var step = _steps.FirstOrDefault(s => s.Id == stepId);
                var afterStep = _steps.FirstOrDefault(s => s.Id == afterStepId);

                if (step == null)
                {
                    step = new Step(stepId);
                    _steps.Add(step);
                }
                if (afterStep == null)
                {
                    afterStep = new Step(afterStepId);
                    _steps.Add(afterStep);
                }

                step.After.Add(afterStep);
                afterStep.Before.Add(step);
            }
        }

        public string Work1()
        {
            string output = "";

            var current = _steps.First(s => s.Before.Count == 0);
            List<Step> available = new List<Step>();
            available.Add(current);

            while (current != null)
            {
                output += current.Id;
                current.IsProcessed = true;
                available.Remove(current);

                available = _steps.Where(s => !s.IsProcessed && s.Before.All(parent => parent.IsProcessed)).OrderBy(s => s.Id).ToList();
                current = available.FirstOrDefault();
            }

            return output;
        }

        public int Work2()
        {
            string output = "";

            var workers = new List<Elf>();
            for (int i = 0; i < 5; i++)
            {
                workers.Add(new Elf(i));
            }

            Step current = null;
            List<Step> available = new List<Step>();
            available.Add(_steps.First(s => s.Before.Count == 0));

            int o = 0;
            for (int i = 0; _steps.Any(s => !s.IsProcessed); i++)
            {
                if (current == null)
                    current = available.FirstOrDefault();

                while (current != null)
                {
                    var worker = workers.FirstOrDefault(w => w.IsIdle);
                    if (worker != null)
                    {
                        worker.AssignTask(current);
                        current.IsWorking = true;
                        available.Remove(current);
                        current = available.FirstOrDefault();
                    }
                    else
                        break;
                }

                workers.ForEach(w => 
                {
                    if (w.Work())
                    {
                        var completed = w.CompleteTask();
                        output += completed.Id;
                        _steps.First(s => s.Id == completed.Id).IsProcessed = true;
                    }
                });
                available = _steps.Where(s => !s.IsProcessed && ! s.IsWorking && s.Before.All(parent => parent.IsProcessed)).OrderBy(s => s.Id).ToList();
                o = i;
            }

            return o;
        }

        string _testInput = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";

        string _input = @"Step J must be finished before step K can begin.
Step N must be finished before step X can begin.
Step S must be finished before step G can begin.
Step T must be finished before step R can begin.
Step H must be finished before step L can begin.
Step V must be finished before step W can begin.
Step G must be finished before step U can begin.
Step K must be finished before step A can begin.
Step D must be finished before step Z can begin.
Step C must be finished before step E can begin.
Step X must be finished before step P can begin.
Step Y must be finished before step U can begin.
Step R must be finished before step O can begin.
Step W must be finished before step U can begin.
Step O must be finished before step Q can begin.
Step A must be finished before step P can begin.
Step B must be finished before step E can begin.
Step F must be finished before step E can begin.
Step Q must be finished before step U can begin.
Step M must be finished before step E can begin.
Step P must be finished before step U can begin.
Step L must be finished before step Z can begin.
Step Z must be finished before step U can begin.
Step U must be finished before step E can begin.
Step I must be finished before step E can begin.
Step H must be finished before step G can begin.
Step X must be finished before step I can begin.
Step K must be finished before step X can begin.
Step Z must be finished before step I can begin.
Step S must be finished before step M can begin.
Step L must be finished before step U can begin.
Step A must be finished before step M can begin.
Step W must be finished before step A can begin.
Step N must be finished before step A can begin.
Step S must be finished before step E can begin.
Step W must be finished before step Q can begin.
Step J must be finished before step L can begin.
Step Q must be finished before step L can begin.
Step M must be finished before step U can begin.
Step H must be finished before step E can begin.
Step D must be finished before step E can begin.
Step V must be finished before step P can begin.
Step Q must be finished before step M can begin.
Step X must be finished before step W can begin.
Step K must be finished before step I can begin.
Step T must be finished before step H can begin.
Step Y must be finished before step L can begin.
Step G must be finished before step O can begin.
Step M must be finished before step Z can begin.
Step F must be finished before step Z can begin.
Step Q must be finished before step E can begin.
Step H must be finished before step C can begin.
Step Q must be finished before step P can begin.
Step D must be finished before step U can begin.
Step Z must be finished before step E can begin.
Step O must be finished before step M can begin.
Step L must be finished before step I can begin.
Step J must be finished before step A can begin.
Step Q must be finished before step Z can begin.
Step P must be finished before step I can begin.
Step K must be finished before step O can begin.
Step R must be finished before step E can begin.
Step W must be finished before step F can begin.
Step D must be finished before step Q can begin.
Step R must be finished before step U can begin.
Step W must be finished before step P can begin.
Step S must be finished before step Z can begin.
Step T must be finished before step P can begin.
Step B must be finished before step Q can begin.
Step S must be finished before step T can begin.
Step R must be finished before step A can begin.
Step K must be finished before step R can begin.
Step N must be finished before step G can begin.
Step C must be finished before step W can begin.
Step T must be finished before step A can begin.
Step B must be finished before step Z can begin.
Step C must be finished before step P can begin.
Step D must be finished before step P can begin.
Step B must be finished before step P can begin.
Step F must be finished before step U can begin.
Step V must be finished before step X can begin.
Step K must be finished before step W can begin.
Step Y must be finished before step I can begin.
Step C must be finished before step B can begin.
Step X must be finished before step L can begin.
Step X must be finished before step M can begin.
Step H must be finished before step P can begin.
Step S must be finished before step F can begin.
Step J must be finished before step Y can begin.
Step Y must be finished before step Z can begin.
Step B must be finished before step I can begin.
Step S must be finished before step C can begin.
Step K must be finished before step E can begin.
Step N must be finished before step Q can begin.
Step A must be finished before step Z can begin.
Step J must be finished before step I can begin.
Step Y must be finished before step O can begin.
Step Y must be finished before step F can begin.
Step S must be finished before step U can begin.
Step D must be finished before step W can begin.
Step V must be finished before step D can begin.";
    }
}
