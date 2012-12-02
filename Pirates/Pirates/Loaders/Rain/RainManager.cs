using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pirates.Loaders.Cloud;
using Pirates.Shaders;

namespace Pirates.Loaders.Rain
{
    internal class RainManager : IManager
    {
        private Instancer dropsList;

        public Instancer Instancer
        {
            get { return dropsList; }
        }

        private List<Instance> drops = new List<Instance>();

        public List<Instance> InstancesList
        {
            get { return drops; }
            set { drops = value; }
        }

        private Random rnd = new Random(DateTime.Now.Millisecond);

        public Random Rnd
        {
            get { return rnd; }
            set { rnd = value; }
        }

        public RainManager()
        {
            dropsList = new Instancer();
        }

        public void Update(float gameTime)
        {
            Instancer.Update(gameTime);
        }

        public void Draw(BaseShader shader)
        {
            Instancer.Draw(shader);
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