using HiveDemo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiveDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            BotClient Client = new BotClient("HiveDemo.Scripts.Mining.IRON_PM");
            Client.Start();
            Console.ReadKey();
        }
    }
}
