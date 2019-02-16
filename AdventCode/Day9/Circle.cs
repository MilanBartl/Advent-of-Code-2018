using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode.Day9
{
    public class Circle
    {
        public Marble First { get; set; }

        public Marble Last { get; set; }

        public void AddFirst(Marble first)
        {
            First = first;
            Last = first;
        }

        public void AddAfter(Marble current, Marble added)
        {
            if (current == Last)
                Last = added;

            added.Previous = current;
            added.Next = current.Next;
            current.Next = added;

            if (added.Next != null)
                added.Next.Previous = added;
        }

        public void Remove(Marble removed)
        {
            if (removed == Last)
            {
                Last = removed.Previous;
                removed.Previous.Next = removed.Next;
            }
            else if (removed == First)
            {
                First = Last;
                Last = First.Previous;
                First.Previous = null;
                Last.Next = null;
            }
            else
            {
                removed.Next.Previous = removed.Previous;
                removed.Previous.Next = removed.Next;
            }            
        }

        public void Clear()
        {
            var current = First;
            var next = First.Next;
            while (next != null)
            {
                current.Previous = null;
                current.Next = null;
                current = next;
                next = current.Next;
            }
        }
    }
}
