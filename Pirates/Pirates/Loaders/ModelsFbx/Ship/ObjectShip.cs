using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        private BoundingBoxRenderer bbRenderer;

        public ObjectShip()
            : base("model")
        {
            UpdateBoundingBox();
            bbRenderer = new BoundingBoxRenderer();
        }

        public void Update()
        {
            base.Update();
            UpdateBoundingBox();
        }

        public void Draw(BasicEffect fx)
        {
            Matrix[] modelTansforms = new Matrix[Fbx.Bones.Count];
            Fbx.CopyAbsoluteBoneTransformsTo(modelTansforms);
            int i = 0;

            foreach (ModelMesh mesh in Fbx.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = modelTansforms[mesh.ParentBone.Index] * ModelMatrix;
                    effect.View = fx.View;
                    effect.Projection = fx.Projection;
                }
                mesh.Draw();
                //bbRenderer.Render(BoundingBoxes[i++], ModelMatrix, fx.View, fx.Projection);
            }
        }
    }
}