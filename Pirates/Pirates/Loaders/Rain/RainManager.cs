using System;
using Microsoft.Xna.Framework;
using Pirates.Loaders.Cloud;

namespace Pirates.Loaders.Rain
{
    internal class RainManager : Manager
    {
        public RainManager()
        {
            Instancer = new Instancer();
        }

        public void AddDrop(int dropsCount, Vector3 min, Vector3 max)
        {
            for (int w = 0; w < dropsCount; w++)
            {
                float x = MathHelper.Lerp(min.X, max.X, (float)Rnd.NextDouble());
                float y = MathHelper.Lerp(min.Y, max.Y, (float)Rnd.NextDouble());
                float z = MathHelper.Lerp(min.Z, max.Z, (float)Rnd.NextDouble());

                Vector3 information = new Vector3();

                float scale = 6.5f;
                Random rnd = new Random();

                information.Y = (float)rnd.NextDouble() + 0.01f;

                InstancesList.Add(new Instance(Instancer, scale, new Vector3(x, y, z), information));
            }
        }
    }
}