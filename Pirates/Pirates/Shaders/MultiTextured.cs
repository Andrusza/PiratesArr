using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class MultiTextured : BaseShader
    {
        public MultiTextured(): base("MultiTexturing")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["MultiTexturing"];
        }

        public void InitParameters()
        {
            Texture2D rock = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "Rock");
            Texture2D grass = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "Grass");
            Texture2D sand = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "Sand");
            Texture2D weight = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "colorMap");

            Technique.Parameters["d0_Sand"].SetValue(sand);
            Technique.Parameters["d1_Grass"].SetValue(grass);
            Technique.Parameters["d2_Rock"].SetValue(rock);
            Technique.Parameters["WeightMap"].SetValue(weight);

            Technique.Parameters["Projection"].SetValue(projectionMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }

        public override void Update(float time)
        {
            Technique.Parameters["View"].SetValue(viewMatrix);
        }
    }
}
