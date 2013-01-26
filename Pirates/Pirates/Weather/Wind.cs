using System.Timers;
using Microsoft.Xna.Framework;
using System;

namespace Pirates.Weather
{
    public static class Wind
    {
        private static System.Timers.Timer timer = new Timer();
        private static Random rnd=new Random();
        private static double interval = 1000*10;

        public static void Setup()
        {
            timer.Interval = interval;
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(NewWind);
            timer.Start();
        }

        private static void NewWind(object sender, ElapsedEventArgs e)
        {
            //Force=rnd.Next(100, 400);
            //direction.X = (float)rnd.NextDouble() * 2 - 1;
            //direction.Y = (float)rnd.NextDouble() * 2 - 1;
            //direction.Normalize();
            Force = 1000;
            Direction = new Vector2(0, 1);
            
        }

        private static Vector2 direction;
        private static float force;

        public static float Force
        {
            get { return Wind.force; }
            set { Wind.force = value; }
        }

        public static Vector2 Direction
        {
            get { return Wind.direction; }
            set { Wind.direction = value; }
        }
    }
}