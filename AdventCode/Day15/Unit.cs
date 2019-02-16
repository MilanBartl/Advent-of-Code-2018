using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day15
{
    public class Unit
    {
        private static int _idGen = 1;

        public int Id { get; set; }

        public Race Race { get; private set; }

        public Coordinate Position { get; set; }

        public int HitPoints { get; set; }

        public int Attack { get; private set; }

        public Unit(int x, int y, Race race)
        {
            Id = _idGen++;
            HitPoints = 200;
            Attack = 3;

            Position = new Coordinate(x, y);
            Race = race;
        }
    }

    public enum Race
    {
        Elf,
        Goblin
    };
}
