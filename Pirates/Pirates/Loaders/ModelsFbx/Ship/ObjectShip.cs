using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Physics;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        private BoundingBoxRenderer bbRenderer;
        private ObjectPhysics physics;

        public ObjectPhysics Physics
        {
            get { return physics; }
            set { physics = value; }
        }
 

        public ObjectShip()
            : base("model")
        {
            UpdateBoundingBox();
            bbRenderer = new BoundingBoxRenderer();
            physics = new ObjectPhysics(1000);
        }

        public void Update(float time)
        {
            Vector3 newPos = Physics.Update(this, time);
            this.Translate(newPos);
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
                // bbRenderer.Render(BoundingBoxes[i++], ModelMatrix, fx.View, fx.Projection);
            }
        }
    }
}