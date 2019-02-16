using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace AdventCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker = new Day22.Worker();
            var result = worker.Work2();

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}
