using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectGeometry
    {
        Model fbx;
        public ObjectShip()
        {
            fbx = ContentLoader.Load<Model>(ContentType.FBX, "model");
        }

        public void Draw(BasicEffect fx)
        {
            Matrix[] modelTansforms = new Matrix[fbx.Bones.Count];
            fbx.CopyAbsoluteBoneTransformsTo(modelTansforms);

            foreach (ModelMesh mesh in fbx.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = modelTansforms[mesh.ParentBone.Index] * modelMatrix;
                    effect.View = fx.View;
                    effect.Projection = fx.Projection;
                }
                mesh.Draw();
            }
        }
    }
}