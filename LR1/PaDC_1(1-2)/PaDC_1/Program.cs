using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaDC_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //array dimensions
            //int[] n = {0, 50, 100, 150, 550, 600, 650, 12000};
            Dictionary<double, double> pairsLine = new Dictionary<double, double>();
            Dictionary<double, double> pairsColumns = new Dictionary<double, double>();

            //LinkedList < Double > linked_list = new LinkedList<Double>();

            for (int n_index = 0; n_index < 12000; n_index+=500)
            {
                Stopwatch stopwatch = new Stopwatch();

                //array initialization
                int[][] array = new int[n_index][];
                for(int i = 0; i < array.Length-1; i++)
                    array[i] = new int[n_index];

                //Array size
                double bytes = (n_index * sizeof(int))/1024d;

                //padding line by line
                stopwatch.Start();
                for (int i = 0; i < array.Length-1; i++)
                {
                    for (int j = 0; j < array[i].Length-1; j++)
                    {
                        array[i][j] = i * j;
                    }
                }
                stopwatch.Stop();

                pairsLine.Add(bytes, stopwatch.Elapsed.TotalMilliseconds);

                stopwatch.Reset();

                //padding by columns
                stopwatch.Start();
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array[i].Length - 1; j++)
                    {
                        array[j][i] = i * j;
                    }
                }
                stopwatch.Stop();

                pairsColumns.Add(bytes, stopwatch.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine("{0,15}| {1,15}| {2,15}|", "Размер","По строкам","По столбцам");
            foreach (var item in pairsLine.Keys)
            {
                Console.WriteLine("{0,12} kb| {1,15}| {2,15}|", item.ToString("F3"), pairsLine[item], pairsColumns[item]);
            }

            Console.WriteLine("\n\n Float test :\n");


            pairsLine.Clear();
            pairsColumns.Clear();
            for (int n_index = 0; n_index < 12000; n_index += 500)
            {
                Stopwatch stopwatch = new Stopwatch();

                //array initialization
                float[][] array = new float[n_index][];
                for (int i = 0; i < array.Length - 1; i++)
                    array[i] = new float[n_index];

                //Array size
                double bytes = (n_index * sizeof(float)) / 1024d;

                //padding line by line
                stopwatch.Start();
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array[i].Length - 1; j++)
                    {
                        array[i][j] = i * j;
                    }
                }
                stopwatch.Stop();

                pairsLine.Add(bytes, stopwatch.Elapsed.TotalMilliseconds);

                stopwatch.Reset();

                //padding by columns
                stopwatch.Start();
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array[i].Length - 1; j++)
                    {
                        array[j][i] = i * j;
                    }
                }
                stopwatch.Stop();

                pairsColumns.Add(bytes, stopwatch.Elapsed.TotalMilliseconds);
            }

            Console.WriteLine("{0,15}| {1,15}| {2,15}|", "Размер", "По строкам", "По столбцам");
            foreach (var item in pairsLine.Keys)
            {
                Console.WriteLine("{0,12} kb| {1,15}| {2,15}|", item.ToString("F3"), pairsLine[item], pairsColumns[item]);
            }

            Console.ReadLine();
        }
    }
}
