using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;

namespace Pirates.Shaders
{
    public class CloudShader : BaseShader
    {
        public Vector4 lightColor;

        public Vector3 eyePosition;
        public Matrix VP;
        public Vector4 lightVector;
        private Texture2D cloudsParts;
        public float hour;

        public CloudShader()
            : base("Cloud")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Clouds"];
            lightColor = Color.White.ToVector4();
            cloudsParts = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "clouds");
        }

        public override void InitParameters()
        {
            Technique.Parameters["lightColor"].SetValue(lightColor);
            Technique.Parameters["partTexture"].SetValue(cloudsParts);
            Technique.Parameters["hour"].SetValue(0);
        }

        public override void Update(float time)
        {
            eyePosition *= -1;

            Technique.Parameters["hour"].SetValue(hour);
            Technique.Parameters["EyePosition"].SetValue(eyePosition);
            Technique.Parameters["vp"].SetValue(viewMatrix * projectionMatrix);
            Technique.Parameters["World"].SetValue(WorldMatrix);
        }
    }
}