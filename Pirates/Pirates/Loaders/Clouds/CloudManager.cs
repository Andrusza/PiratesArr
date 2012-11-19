using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Pirates.Shaders;

namespace Pirates.Loaders.Cloud
{
    internal class CloudManager
    {
       
        public CloudInstancer cloudsList;

        private List<ObjectCloud> whisps = new List<ObjectCloud>();
        private Random rnd = new Random(DateTime.Now.Millisecond);

        public CloudManager()
        {
            cloudsList = new CloudInstancer();
        }

        public void Update(float gameTime)
        {
            cloudsList.Update(gameTime);
        }

        public void Draw(CloudShader shader)
        {
            cloudsList.Draw(shader);
        }

        public void AddCloud(int whispCount, Vector3 min, Vector3 max, float colorMod, params int[] whispRange)
        {
            int si = 0;
            float scaleMod = Vector3.Distance(min, max) / 4.5f;

            for (int w = 0; w < whispCount; w++)
            {
                float x = MathHelper.Lerp(min.X, max.X, (float)rnd.NextDouble());
                float y = MathHelper.Lerp(min.Y, max.Y, (float)rnd.NextDouble());
                float z = MathHelper.Lerp(min.Z, max.Z, (float)rnd.NextDouble());

                if (si >= whispRange.Length)
                    si = 0;
                Vector3 information = new Vector3();

                float whipTexture=whispRange[si++] /100.0f;
                information.X = whipTexture;

                float color=rnd.Next(7, 10) / 10f * colorMod;
                information.Y = 1;
                information.Z = color;
                float scale = 500;

                whisps.Add(new ObjectCloud(cloudsList, scale, new Vector3(x, y, z),information));
            }
        }
    }
}