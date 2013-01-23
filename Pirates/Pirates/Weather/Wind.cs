using Microsoft.Xna.Framework;

namespace Pirates.Weather
{
    public static class Wind
    {
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