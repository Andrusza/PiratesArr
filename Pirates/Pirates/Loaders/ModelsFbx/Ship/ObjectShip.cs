using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Physics;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        private BoundingBoxRenderer bbRenderer;
        private ObjectPhysics physics;
        private Matrix[] boneTransforms;
        private ModelBone sailMiddle;

        public Vector2 sailDirection = new Vector2(0, 1);
        public float sialHeight = 0;

        public ObjectPhysics Physics
        {
            get { return physics; }
            set { physics = value; }
        }

        public ObjectShip()
            : base("model")
        {
            UpdateBoundingBox();
            //meshWorldMatrices[20] = meshWorldMatrices[17] *= Matrix.CreateScale(0f);
            bbRenderer = new BoundingBoxRenderer();
            sailMiddle = this.Fbx.Bones["zagiel2"];
            sailMiddle.Transform *= Matrix.CreateScale(0f);

            boneTransforms = new Matrix[this.Fbx.Bones.Count];
        }

        public void Update(float time)
        {
            Vector3 newPos = Physics.Update(this.ModelMatrix, time);
            this.Translate(newPos.X, this.ModelMatrix.Translation.Y, newPos.Z);
            base.Update();
            UpdateBoundingBox();
        }

        public void Draw(BasicEffect fx)
        {
            Fbx.CopyAbsoluteBoneTransformsTo(boneTransforms);
            
            int i = 0;

            foreach (ModelMesh mesh in Fbx.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = boneTransforms[mesh.ParentBone.Index] * ModelMatrix;
                    effect.View = fx.View;
                    effect.Projection = fx.Projection;
                }
                mesh.Draw();
                //bbRenderer.Render(BoundingBoxes[i++], ModelMatrix, fx.View, fx.Projection);
            }
        }
    }
}