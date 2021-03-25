using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDC_1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            Dictionary<int, double> task1 = new Dictionary<int, double>();
            Dictionary<int, double> task2 = new Dictionary<int, double>();
            Dictionary<int, double> task3 = new Dictionary<int, double>();


            for (int n_index = 0; n_index < 12000; n_index += 500) {

                stopwatch.Reset();
                stopwatch.Start();

                double[] array = new double[n_index];
                for (int i = 0; i < n_index; i++)
                    array[i] = i;
                double sum = 0;
                for (int i = 0; i < n_index; i++)
                    sum += array[i];

                stopwatch.Stop();

                task1.Add(n_index, stopwatch.Elapsed.TotalMilliseconds);

                stopwatch.Reset();
                stopwatch.Start();

                LinkedList<double> linked_list = new LinkedList<double>();
                for (int i = 0; i < n_index; i++)
                    linked_list.AddFirst((double)i);
                double sum2 = 0;
                for (int i = 0; i < n_index; i++)
                    sum2 += linked_list.ElementAt(i);

                stopwatch.Stop();

                task2.Add(n_index, stopwatch.Elapsed.TotalMilliseconds);

                stopwatch.Reset();
                stopwatch.Start();

                ArrayList array_list = new ArrayList(n_index);
                for (int i = 0; i < n_index; i++)
                    array_list.Add((double)(i));
                double sum3 = 0;
                for (int i = 0; i < n_index; i++)
                    sum3 += (double)array_list[i];

                stopwatch.Stop();

                task3.Add(n_index, stopwatch.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine("{0,15}| {1,15}| {2,15}| {3,15}|", "Размер", "Task1", "Task2", "Task3");
            foreach (var item in task1.Keys)
            {
                Console.WriteLine("{0,15}| {1,15}| {2,15}| {3,15}|", item.ToString(), task1[item], task2[item], task3[item]);
            }

            Console.ReadLine();
        }
    }
}
