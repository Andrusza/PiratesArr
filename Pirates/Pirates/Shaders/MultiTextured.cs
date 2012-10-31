﻿using System;
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
            Texture2D snow= ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "snow");
            Texture2D grass = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "grass");
            Texture2D sand = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "sand");
            Texture2D weight = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "island4");

            Technique.Parameters["d0_Sand"].SetValue(sand);
            Technique.Parameters["d1_Grass"].SetValue(grass);
            Technique.Parameters["d2_Snow"].SetValue(snow);
            Technique.Parameters["WeightMap"].SetValue(weight);

            Technique.Parameters["Projection"].SetValue(projectionMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }

        public override void Update(float time)
        {
            Technique.Parameters["World"].SetValue(worldMatrix);
            Technique.Parameters["View"].SetValue(viewMatrix);
        }
    }
}
