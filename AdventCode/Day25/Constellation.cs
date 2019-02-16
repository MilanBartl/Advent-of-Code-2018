using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day25
{
    public class Constellation
    {
        public List<Point> Points { get; set; }

        public Constellation(IEnumerable<Point> points)
        {
            Points = new List<Point>();
            Points.AddRange(points);
            foreach (var point in points)
            {
                point.Constellation = this;
            }
        }

        public void AddPoints(IEnumerable<Point> points)
        {
            Points.AddRange(points);
            foreach (var point in points)
            {
                point.Constellation = this;
            }
        }
    }
}
