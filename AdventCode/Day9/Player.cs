using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day9
{
    public class Player
    {
        public int Id { get; set; }

        public long Score { get; set; }

        public Player(int id)
        {
            Id = id;
        }
    }
}
