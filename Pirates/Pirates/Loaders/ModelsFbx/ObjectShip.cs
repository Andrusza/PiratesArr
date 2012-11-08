using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        public ObjectShip(JustMvp fx)
            : base("ship2", fx)
        {
        }

        public void Draw(JustMvp fx)
        {
            Matrix[] modelTansforms = new Matrix[fbx.Bones.Count];
            fbx.CopyAbsoluteBoneTransformsTo(modelTansforms);

            foreach (ModelMesh mesh in fbx.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    Matrix world = modelTansforms[mesh.ParentBone.Index] * fx.WorldMatrix;
                    currentEffect.Parameters["MVP"].SetValue(fx.MVP);
                }
                mesh.Draw();
            }
        }
    }
}