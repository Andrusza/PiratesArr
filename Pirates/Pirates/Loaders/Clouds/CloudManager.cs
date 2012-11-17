using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Pirates.Loaders.Clouds
{
    class CloudManager
    {
        int[] allCloudSprites = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        CloudInstancer clouds;

        List<ObjectCloud> whisps = new List<ObjectCloud>();
        Random rnd = new Random(DateTime.Now.Millisecond);

        public CloudManager()
        {
            clouds = new CloudInstancer();
        }

        public void AddCloud(int whispCount, Vector3 min, Vector3 max, float colorMod, params int[] whispRange)
        {
            int si = 0;
            float scaleMod = Vector3.Distance(-min, max) / 4.5f;

            for (int w = 0; w < whispCount; w++)
            {
                float x = MathHelper.Lerp(-min.X, max.X, (float)rnd.NextDouble());
                float y = MathHelper.Lerp(-min.Y, max.Y, (float)rnd.NextDouble());
                float z = MathHelper.Lerp(-min.Z, max.Z, (float)rnd.NextDouble());

                if (si >= whispRange.Length)
                    si = 0;

                whisps.Add(new ObjectCloud(clouds));
            }
        }

    }
}
