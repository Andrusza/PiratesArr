using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pirates.Loaders.Clouds;
using Pirates.Shaders;

namespace Pirates.Loaders.Cloud
{
    internal class CloudManager : IManager
    {
        private CloudInstancer cloudsList;

        public Instancer Instancer
        {
            get { return cloudsList; }
        }

        private List<Instance> whisps = new List<Instance>();

        public List<Instance> InstancesList
        {
            get { return whisps; }
            set { whisps = value; }
        }

        private Random rnd = new Random(DateTime.Now.Millisecond);

        public Random Rnd
        {
            get { return rnd; }
            set { rnd = value; }
        }

        public CloudManager()
        {
            cloudsList = new CloudInstancer();
        }

        public void Update(float gameTime)
        {
            Instancer.Update(gameTime);
        }

        public void Draw(BaseShader shader)
        {
            Instancer.Draw(shader);
        }

        public void AddCloud(int whispCount, Vector3 min, Vector3 max, float colorMod, params int[] whispRange)
        {
            int si = 0;
            float scaleMod = Vector3.Distance(min, max) / 4.5f;

            for (int w = 0; w < whispCount; w++)
            {
                float x = MathHelper.Lerp(min.X, max.X, (float)Rnd.NextDouble());
                float y = MathHelper.Lerp(min.Y, max.Y, (float)Rnd.NextDouble());
                float z = MathHelper.Lerp(min.Z, max.Z, (float)Rnd.NextDouble());

                if (si >= whispRange.Length)
                    si = 0;
                Vector3 information = new Vector3();

                float whipTexture = whispRange[si++] / 100.0f;
                information.X = whipTexture;

                float color = Rnd.Next(7, 10) / 10f * colorMod;
                information.Y = 1;
                information.Z = color;
                float scale = 500;

                InstancesList.Add(new Instance(Instancer, scale, new Vector3(x, y, z), information));
            }
        }
    }
}