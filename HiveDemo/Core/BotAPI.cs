using HiveDemo.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HiveDemo.Core
{
    class BotAPI
    {
        private IntPtr _rlhwnd;
        public RemoteIO IO; 
        public BotAPI(IntPtr rlhwnd, UInt32 pid)
        {
            _rlhwnd = rlhwnd;
            IO = new RemoteIO(pid);
        }
        public void dropInventory(int[] slotToSkip)
        {
            IO.keyevent(401, 16, 16);
            for (int i = 0; i < Canvas.INVENTORY_SLOT_DROP.Length; i++)
            {
                if (!slotToSkip.Contains(i))
                {
                    Point center = new Point(Canvas.INVENTORY_SLOT[i].X + Canvas.INVENTORY_SLOT[i].Width / 2, Canvas.INVENTORY_SLOT[i].Y - 29 + Canvas.INVENTORY_SLOT[i].Height / 2);
                    IO.Click(center.X, center.Y);
                }
            }
            IO.keyevent(402, 16, 16);

        }
        public bool lastinvslot()
        {
            Rectangle Chatbox = Canvas.INVENTORY_SLOT[27];
            Color targetColor = Color.FromArgb(97, 64, 50);
            Point location = PixelFinder.Pixelsearch(_rlhwnd, Chatbox, targetColor, 0);
            if (location.X != 0 && location.Y != 0)
            {
                return true;
            }
            else { return false; }


        }
        public Point getClosestObject(int R, int G, int B, int shade)
        {

            Rectangle Gameview = Canvas.GAMEVIEW;
            Random rnd = new Random();
            Color targetColor = Color.FromArgb(R, G, B);

            // List<Rectangle> found = finder.FindColorRectangles(targetColor, Canvas.GAMEVIEW, 25, 25);
            List<Point> location = PixelFinder.PixelsearchALL(_rlhwnd, Gameview, targetColor, 2);
            if (location.Count > 1)
            {
                Point closestPoint = location.OrderBy(p => Math.Sqrt(Math.Pow(p.X - Canvas.PLAYER.X, 2) + Math.Pow(p.Y - Canvas.PLAYER.Y, 2))).Skip(rnd.Next(10)).First();
                return closestPoint;
            }
            else
            {
                Point closestPoint = new Point(0, 0);
                return closestPoint;
            }


        }

        public bool getXPDrop()
        {
            Rectangle Gameview = Canvas.XPDROP;
            Color targetColor = Color.FromArgb(0, 255, 255);
            Point location = PixelFinder.Pixelsearch(RLClient.getHandle("RuneLite"), Gameview, targetColor, 5);
            if (location.X != 0 && location.Y != 0)
            {
                return true;
            }
            else { return false; }
        }
        public bool levelup()
        {
            Rectangle Chatbox = Canvas.Chatbox;
            Color targetColor = Color.FromArgb(0, 0, 255);
            Point location = PixelFinder.Pixelsearch(_rlhwnd, Chatbox, targetColor, 5);
            if (location.X != 0 && location.Y != 0)
            {
                return true;
            }
            else { return false; }


        }

        public void mousemoveclick(int x, int y)
        {
            IO.Click(x, y - 29);
        }
        public void pressspace()
        {
            IO.keyevent(401, 32, 32);
            Thread.Sleep(300);
            IO.keyevent(402, 32, 32);
        }
        public void inv()
        {
            IO.keyevent(401, 27, 27);
            Thread.Sleep(300);
            IO.keyevent(402, 27, 27);
        }
    }
}
