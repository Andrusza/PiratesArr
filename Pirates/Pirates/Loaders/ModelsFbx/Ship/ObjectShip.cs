using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pirates.Physics;
using Pirates.Shaders;
using Pirates.Utility;

namespace Pirates.Loaders.ModelsFbx
{
    public class ObjectShip : ObjectMesh
    {
        private BoundingBoxRenderer bbRenderer;
        private ObjectPhysics physics;
        private Matrix[] boneTransforms;
        private ModelBone sailMiddle;
        private ModelBone sailMiddlePlank;
        private JustMvp shader;

        private float sailHeight = 2;

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

            bbRenderer = new BoundingBoxRenderer();
            sailMiddle = this.Fbx.Bones["zagiel2"];
            sailMiddlePlank = this.Fbx.Bones["zagiel2_belka"];

            boneTransforms = new Matrix[this.Fbx.Bones.Count];

            shader = new JustMvp();
            shader.InitParameters();

            sailMiddle.Transform *= Matrix.CreateScale(1f, 1f - 0.05f * sailHeight, 1f) * Matrix.CreateTranslation(new Vector3(0f, 7.4f * sailHeight, 0f));

            foreach (ModelMeshPart part in Fbx.Meshes[20].MeshParts)
            {
                part.Effect = shader.Technique.Clone();
            }
        }

        float rotation = 0;
        float rot = MathHelper.ToRadians(1);

        private void KeyPressed()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.A))
            {
                
                if (rotation > MathHelper.ToRadians(-45/2))
                {
                    rotation -= rot;
                    sailDirection = MathFunctions.AngleToVector(rotation);
                    sailMiddle.Transform *= Matrix.CreateRotationY(-rot);
                    sailMiddlePlank.Transform *= Matrix.CreateRotationY(-rot);
                }
            }

            if (keyState.IsKeyDown(Keys.D))
            {

                if (rotation < MathHelper.ToRadians(45/2))
                {
                    rotation += rot;
                    sailDirection = MathFunctions.AngleToVector(rotation);
                    sailMiddle.Transform *= Matrix.CreateRotationY(rot);
                    sailMiddlePlank.Transform *= Matrix.CreateRotationY(rot);
                }
            }
        }

        public void Update(float time)
        {
            Vector3 newPos = Physics.Update(this.ModelMatrix, time);
            this.Translate(newPos.X, this.ModelMatrix.Translation.Y, newPos.Z);

            shader.Update(time);
            base.Update();
            KeyPressed();

            UpdateBoundingBox();
        }

        public void Draw(BasicEffect fx)
        {
            Fbx.CopyAbsoluteBoneTransformsTo(boneTransforms);
            shader.MVP = boneTransforms[3] * ModelMatrix * fx.View * fx.Projection;

            int i = 0;

            foreach (ModelMesh mesh in Fbx.Meshes)
            {
                if (mesh.Name != "zagiel2")
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = boneTransforms[mesh.ParentBone.Index] * ModelMatrix;
                        effect.View = fx.View;
                        effect.Projection = fx.Projection;
                    }

                    mesh.Draw();
                }
                else
                {
                    foreach (Effect currentEffect in mesh.Effects)
                    {
                        Matrix world = boneTransforms[mesh.ParentBone.Index] * ModelMatrix * fx.View * fx.Projection;
                        currentEffect.Parameters["MVP"].SetValue(world);
                        currentEffect.Parameters["time"].SetValue(shader.time);
                    }
                    mesh.Draw();
                }

                //bbRenderer.Render(BoundingBoxes[i++], ModelMatrix, fx.View, fx.Projection);
            }
        }
    }
}