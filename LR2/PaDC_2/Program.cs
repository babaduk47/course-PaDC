using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace PaDC_2
{
    class Program
    {

        private static ConcurrentQueue<string> taskQueue;

        static string[] imagesLink = { 
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye_kollazh_na_temu_prirody_1920x1200.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/242624_565001.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye-kartinki-Priroda-2278640.jpeg",
            "https://bipbap.ru/wp-content/uploads/2017/04/wallpapers-nature-16.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/voshod_solnca_priroda_pole_mostik_4499x2231.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/v2.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/plitvickiye-ozera-1.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-10.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-1-4.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye_kollazh_na_temu_prirody_1920x1200.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/242624_565001.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye-kartinki-Priroda-2278640.jpeg",
            "https://bipbap.ru/wp-content/uploads/2017/04/wallpapers-nature-16.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/voshod_solnca_priroda_pole_mostik_4499x2231.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/v2.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/plitvickiye-ozera-1.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-10.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-1-4.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye_kollazh_na_temu_prirody_1920x1200.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/242624_565001.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/krasivye-kartinki-Priroda-2278640.jpeg",
            "https://bipbap.ru/wp-content/uploads/2017/04/wallpapers-nature-16.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/voshod_solnca_priroda_pole_mostik_4499x2231.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/v2.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_kartinki_foto_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_05.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/priroda_01.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/plitvickiye-ozera-1.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-10.jpg",
            "https://bipbap.ru/wp-content/uploads/2017/04/maxresdefault-1-4.jpg"
        };

        static string[] filesName = new string[imagesLink.Length];
        static void Main(string[] args)
        {
            for(int i = 0; i < imagesLink.Length; i++)
            {
                filesName[i] = i.ToString() + ".jpg";
            }

            //проверить сущаетвует ли папка в которую будут сохранятся файлы загруженные с диска
            //FDTD  from disk to disk
            if (!Directory.Exists("FDTD")) Directory.CreateDirectory("FDTD");

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("{0,15}| {1,15}| {2,15}|", "Кол-во потоков", "FromURL", "FromDisk");

            for (int count = 2; count <= 10; count += 2)
            {
                //create queue of lines
                taskQueue = new ConcurrentQueue<string>(imagesLink);
                index = 0;
                //create and start threads
                List<Thread> th = new List<Thread>();
                for (int i = 0; i < count; i++)
                    th.Add(new Thread(LoadFromURL) { IsBackground = true });

                stopwatch.Start();
                foreach (var t in th)
                    t.Start();
                foreach (var t in th)
                    t.Join();
                stopwatch.Stop();
                double endFromURL = stopwatch.Elapsed.TotalMilliseconds;
                //Console.WriteLine("FromURL : " + stopwatch.Elapsed.TotalMilliseconds);


                taskQueue = new ConcurrentQueue<string>(filesName);
                //create and start threads
                th.Clear();
                for (int i = 0; i < count; i++)
                    th.Add(new Thread(LoadFromFile) { IsBackground = true });
                stopwatch.Reset();
                stopwatch.Start();
                foreach (var t in th)
                    t.Start();
                foreach (var t in th)
                    t.Join();
                stopwatch.Stop();
                double endFromDisk = stopwatch.Elapsed.TotalMilliseconds;
                //Console.WriteLine("FromDisk : " + stopwatch.Elapsed.TotalMilliseconds);
                Console.WriteLine("{0,15}| {1,15}| {2,15}|", count, endFromURL, endFromDisk);
                stopwatch.Reset();
            }

            stopwatch.Start();

            //получение изображений по URL
            for (int i = 0; i < imagesLink.Length; i++)
            {
                WebClient client = new WebClient();
                byte[] bArr = client.DownloadData(imagesLink[i]);
                Image temp = new Bitmap(new MemoryStream(bArr));
                temp.Save(filesName[i]);
                client.Dispose();
                temp.Dispose();
            }
            stopwatch.Stop();
            Console.WriteLine("Время затраченное на скачивание картинки из интренета и сохранения ее на диск : " + stopwatch.Elapsed.TotalMilliseconds);

            stopwatch.Reset();

            

            stopwatch.Start();

            //получение изображений с диска
            for (int i = 0; i < imagesLink.Length; i++)
            {
                Image temp = Bitmap.FromFile(filesName[i]);
                temp.Save("FDTD/" + filesName[i]);
                temp.Dispose();
            }
            stopwatch.Stop();
            Console.WriteLine("Время затраченное на загрузку картинки с диска и сохранения ее в другую папку : " + stopwatch.Elapsed.TotalMilliseconds);

            Console.ReadKey();
        }

        static int index = 0;
        private static void LoadFromFile()
        {
            string line = "";
            while (taskQueue.TryDequeue(out line))//get line from queue
            {
                Image temp = Bitmap.FromFile(line);
                temp.Save("FDTD/" + line);
                temp.Dispose();
            }
        }

        private static void LoadFromURL()
        {
            string line = "";
            while (taskQueue.TryDequeue(out line))//get line from queue
            {
                WebClient client = new WebClient();
                byte[] bArr = client.DownloadData(line);
                Image temp = new Bitmap(new MemoryStream(bArr));
                temp.Save((index++) + ".jpg");
                client.Dispose();
                temp.Dispose();
            }
        }
    }
}
