using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        private BoundingBoxRenderer bbRenderer;
        private ObjectPhysics physics;
        private Vector2 direction = new Vector2(0, 1);

        public ObjectShip()
            : base("model")
        {
            UpdateBoundingBox();
            bbRenderer = new BoundingBoxRenderer();
            physics = new ObjectPhysics(12200);
        }

        public void Update(float time)
        {
            direction = physics.Update(Position2D(), new Vector2(0, 1), direction, time);
            this.Rotate(ObjectPhysics.VectorToAngle(direction), 0f, 0f);
            //this.Translate(newPos);
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