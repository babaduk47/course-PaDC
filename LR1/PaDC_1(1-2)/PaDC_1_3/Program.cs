using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDC_1_3
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<long> HashSet = new HashSet<long>();

            for (int i = 1; i <= 10000000; i++)
                HashSet.Add(i);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            long aElement = 0;
            for (int i = 0; i < 10000000; i++)
                Assert.IsTrue( HashSet.Contains(aElement));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);

            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 10000000; i++)
            {
                long aElement2 = 0;
                HashSet.Contains(aElement2);

            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);

            Console.ReadLine();
        }
    }
}
