using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Objects.Namespace_Ship;
using PiratesArr.Game.Surface;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        private float[] amplitude;
        private float[] wavelenght;
        private float[] speed;
        private Vector2[] waveDirection;

        private double randomRange()
        {
            double range = MathHelper.ToRadians(-60) + rng.NextDouble() * MathHelper.ToRadians(60);
            return range;
        }

        public override void LoadContent()
        {
            effect2 = mainInstance.Content.Load<Effect>("Shaders//MVP");
            effect2.CurrentTechnique = effect2.Techniques["Technique1"];
            

            playerShip = new Ship("ship", effect2);

            tera = new Terrain("map2");
            SetShader("basic", "Textured");
            tera.AddTexture("water2", "diffuseMap0");
            tera.AddTexture("waterNormal", "normalMap0");
            tera.BindTextures(effect);
            Texture2D waterr = mainInstance.Content.Load<Texture2D>("textures//water2");
            effect2.Parameters["diffuseMap0"].SetValue(waterr);

            amplitude = new float[8];
            wavelenght = new float[8];
            speed = new float[8];
            waveDirection = new Vector2[8];
            Random rng = new Random();

            for (int i = 0; i < 8; i++)
            {
                amplitude[i] = 10f / ((float)i + 1f);
                wavelenght[i] = 200f * MathHelper.Pi / ((float)i + 1f);
                speed[i] = 1000f + 6f * (float)i;
                waveDirection[i] = new Vector2((float)Math.Sin(randomRange()), (float)Math.Cos(randomRange()));
            }

            effect.Parameters["numWaves"].SetValue(8);
            effect.Parameters["amplitude"].SetValue(amplitude);
            effect.Parameters["wavelength"].SetValue(wavelenght);
            effect.Parameters["speed"].SetValue(speed);
            effect.Parameters["direction"].SetValue(waveDirection);

            effect.Parameters["AmbientColor"].SetValue(Color.White.ToVector4());
            effect.Parameters["AmbientIntensity"].SetValue(0.1f);

            effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            effect.Parameters["DiffuseIntensity"].SetValue(0.10f);
            Vector3 direction = Vector3.Normalize(new Vector3(1, 0, -1));
            effect.Parameters["LightDirection"].SetValue(direction);

            effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());
        }

        public void SetShader(string shadername, string shaderTechniqueName)
        {
            effect = mainInstance.Content.Load<Effect>("Shaders//" + shadername);
            effect.CurrentTechnique = effect.Techniques[shaderTechniqueName];
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}