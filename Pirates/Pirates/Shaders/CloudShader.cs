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

        private Texture2D cloudsParts;

        public CloudShader()
            : base("Cloud")
        {
            this.Technique.CurrentTechnique = this.Technique.Techniques["Clouds"];
            lightColor = Color.White.ToVector4();
            cloudsParts = ContentLoader.Load<Texture2D>(ContentType.TEXTURE, "clouds");
        }

        public void InitParameters()
        {
            Technique.Parameters["lightColor"].SetValue(lightColor);
            Technique.Parameters["partTexture"].SetValue(cloudsParts);
        }

        float x = 0;

        public override void Update(float time)
        {
            x += 0.5f;
            Technique.Parameters["EyePosition"].SetValue(new Vector3(0,x,0));
            Technique.Parameters["vp"].SetValue(viewMatrix * projectionMatrix);
            Technique.Parameters["World"].SetValue(WorldMatrix);
        }
    }
}