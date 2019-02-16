using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day8
{
    public class Node
    {
        public int Id { get; set; }

        public List<Node> Children { get; set; }

        public List<int> Metadata { get; set; }

        public int References { get; set; }

        public Node(int id)
        {
            Id = id;
            References = 0;
            Children = new List<Node>();
            Metadata = new List<int>();
        }
    }
}
