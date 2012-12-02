using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectSkydome : ObjectMesh
    {
        private static float SKYDOMESIZE = 1200;

        public ObjectSkydome(Scattaring fx)
            : base("skydome4", fx)
        {
            fx.skydomeSize = SKYDOMESIZE;
        }

        public void Draw(Scattaring fx)
        {
            Matrix[] modelTansforms = new Matrix[fbx.Bones.Count];
            fbx.CopyAbsoluteBoneTransformsTo(modelTansforms);

            foreach (ModelMesh mesh in fbx.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix world = modelTansforms[mesh.ParentBone.Index] * fx.WorldMatrix;
                    currentEffect.Parameters["WorldViewProj"].SetValue(world * fx.viewProjection);
                    currentEffect.Parameters["WorldInverseTranspose"].SetValue(fx.invertTransposeWorld);
                    currentEffect.Parameters["World"].SetValue(world);
                    currentEffect.Parameters["ViewInv"].SetValue(fx.invertView);

                    currentEffect.Parameters["LightDirection"].SetValue(fx.lightDirection);
                    currentEffect.Parameters["LightColor"].SetValue(fx.lightColor);
                    currentEffect.Parameters["LightColorAmbient"].SetValue(fx.lightColorAmbient);
                    currentEffect.Parameters["FogColor"].SetValue(fx.fogColor);
                    currentEffect.Parameters["fDensity"].SetValue(fx.fogDensity);
                    currentEffect.Parameters["SunLightness"].SetValue(fx.sunLightness);
                    currentEffect.Parameters["sunRadiusAttenuation"].SetValue(fx.sunRadiusAttenuation);
                    currentEffect.Parameters["largeSunLightness"].SetValue(fx.largeSunLightness);
                    currentEffect.Parameters["largeSunRadiusAttenuation"].SetValue(fx.largeSunRadiusAttenuation);
                    currentEffect.Parameters["dayToSunsetSharpness"].SetValue(fx.dayToSunsetSharpness);
                    currentEffect.Parameters["hazeTopAltitude"].SetValue(fx.hazeTopAltitude);
                }
                mesh.Draw();
            }
        }
    }
}