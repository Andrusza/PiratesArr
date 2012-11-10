using System;
using Microsoft.Xna.Framework;

namespace Pirates.Shaders
{
    public struct WaveParameters
    {
        public float wavelength;
        public float steepness;
        public float speed;
        public float kAmpOverLen;
        public Vector2 wave_dir;
    };

    public partial class waterShader : BaseShader
    {
        private static Random random = new Random();

        public WaveParameters GenerateWaves(WaveParameters p)
        {
            float wl = p.wavelength;
            wl = (float)random.NextDouble() * (2.0f * wl - 0.7f * wl) + 0.7f * wl;
           
            p.wavelength = wl;
            p.speed = (float)Math.Sqrt(9.81f * 2.0f * Math.PI / wl) * wl * p.speed;
            float theta = (float)random.NextDouble() * 2.0f * (float)Math.PI;

            p.wave_dir = new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta));
            return p;
        }
    }
}