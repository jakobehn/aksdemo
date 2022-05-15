using System;
using System.Threading;

namespace QBoxCore.HighscoreNotification
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Waiting for highscore notifications");
            Console.WriteLine("------");
            while(true)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}: No new notifications");
                Thread.Sleep(3000);
            }
        }
    }
}
