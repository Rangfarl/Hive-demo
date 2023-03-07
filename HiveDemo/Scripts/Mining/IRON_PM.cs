using HiveDemo.Core;
using HiveDemo.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiveDemo.Scripts.Mining
{
    class IRON_PM
    {
        string Status = "START";
        public static Point foundRock;
        public static int failsafe = 0;
        public BotAPI Client = new BotAPI(RLClient.getHandlePID(12584), 12584);
        private bool first = true;
        public int[] invskip = { 0 };

        public void Execute()
        {
            if(first == true)
            {
                Console.WriteLine("Starting Script...");
                first = false;
            }
                  
        Thread.Sleep(1300);

            switch (Status)
            {
                case "START":
                    if (Client.levelup())
                    {
                        Status = "LEVEL_UP";    

                    }
                    else if (Client.lastinvslot())
                    {
                        Status = "DROP_INVENTORY";
                    }
                    else
                    {
                        Status = "FIND_IRON_ORE";
                    }
                    return;
                case "DROP_INVENTORY":
                    Console.WriteLine("Dropping inv..");
                    Client.dropInventory(invskip);
                    Status = "FIND_IRON_ORE";
                    return;
                case "LEVEL_UP":
                    Console.WriteLine("Leveled up!!");
                    Client.pressspace();
                    Status = "FIND_IRON_ORE";
                    return;
                case "FIND_IRON_ORE":
                    Console.WriteLine("Looking for ore...");
                    foundRock = Client.getClosestObject(84, 55, 42, 2);
                    if (foundRock.X != 0 & foundRock.Y != 0)
                    {
                        Client.mousemoveclick(foundRock.X, foundRock.Y);
                        Console.WriteLine("Found rock X: " + foundRock.X + " Y: " + foundRock.Y);
                        Status = "MINING_COMPLETE";
                    }
                    else
                    {
                        Status = "FIND_IRON_ORE";
                    }
                        return;
                case "MINING_COMPLETE":
                    while (!Client.getXPDrop())
                    {

                        Thread.Sleep(500);

                        if (failsafe == 50)
                        {
                            failsafe = 0;
                            Console.WriteLine("failsafe triggered");
                            Status = "FIND_IRON_ORE";
                            break;
                        }
                        if (Client.levelup())
                        {
                            Status = "LEVEL_UP";
                            break;
                        }
                        failsafe++;
                    }
                    Status = "START";
                    Console.WriteLine("Ore Mined!!");
                    return;

            }

        }
    }
}

