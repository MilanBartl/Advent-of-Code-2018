using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day9
{
    public class Worker
    {
        private int _lastNumber;

        private Circle _circle = new Circle();

        private LinkedList<Player> _players = new LinkedList<Player>();

        public Worker()
        {
            // test data
            //_lastNumber = 25;
            //int playerCount = 9;

            // real data
            _lastNumber = 71436;
            int playerCount = 466;

            for (int i = 0; i < playerCount; i++)
            {
                _players.AddLast(new Player(i + 1));
            }

            _circle.AddFirst(new Marble(0));
        }

        public long Work1()
        {
            PlayTheGame();
            return _players.Select(p => p.Score).Max();
        }

        public long Work2()
        {
            _lastNumber = _lastNumber * 100;
            PlayTheGame();
            return _players.Select(pl => pl.Score).Max();
        }

        private void PlayTheGame()
        {
            var current = _circle.First;
            var player = _players.First;
            for (int i = 1; i <= _lastNumber; i++)
            {
                if (i % 23 == 0)
                {
                    player.Value.Score += i;
                    for (int j = 0; j < 7; j++)
                    {
                        current = MoveCounterClockwise(current);
                    }
                    player.Value.Score += current.Value;
                    current = MoveClockwise(current);

                    if (current.Previous != null)
                        _circle.Remove(current.Previous);
                    else
                        _circle.Remove(_circle.Last);
                }
                else
                {
                    current = MoveClockwise(current);
                    _circle.AddAfter(current, new Marble(i));
                    current = MoveClockwise(current);
                }

                player = player.Next == null ? _players.First : player.Next;
            }
        }

        private Marble MoveClockwise(Marble current)
        {
            return current.Next == null ? _circle.First : current.Next;
        }

        private Marble MoveCounterClockwise(Marble current)
        {
            return current.Previous == null ? _circle.Last : current.Previous;
        }
    }
}
