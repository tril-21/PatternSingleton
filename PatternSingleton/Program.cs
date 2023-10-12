using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PatternSingleton
{
    class Program
    {
        public class Game
        {
            public DOTA2 dota2 { get; set; }
            public void Launch(string versionGame)
            {
                dota2 = DOTA2.getInstance(versionGame);
            }
        }
        public class DOTA2
        {
            private static DOTA2 instance;

            private static object sync = new Object();//для исправления многопоточности
            public string version { get; protected set; }
            protected DOTA2(string version)
            {
                this.version = version;
            }
            public static DOTA2 getInstance(string version)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new DOTA2(version);
                    }
                }
                return instance;
            }
        }
        //Класс для проверки работы потока
        public class Count
        {
            private static int count=0;

            private static object sync = new Object();//для синхронизации
            public static void increase()
            {
                lock (sync)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        count++;
                        Console.WriteLine(count);
                    }
                }
                
            }
        }
        static void Main(string[] args)
        {/*
            Game g = new Game();
            Thread t1 = new Thread(new ThreadStart(VersionLaunch));
            t1.Start();


            g.Launch("Dota2 v.1.7");
            Console.WriteLine(g.dota2.version);

            

            Console.ReadLine();


            void VersionLaunch()
            {
                g.dota2 = DOTA2.getInstance("Dota2 v.2.0");
                Console.WriteLine(g.dota2.version);
            }
            */
            Thread t1 = new Thread(new ThreadStart(Count.increase));
            t1.Start();

            Count.increase();

            Console.ReadKey();

        }

        
    }
}
