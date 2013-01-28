using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pirates.Physics;
using Pirates.Shaders;
using Pirates.Utility;
using Pirates.Weather;

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

        private float sailHeight = 1;
        private float power = 0;

        public Vector2 sailDirection = new Vector2(0, 1);
        public float sialHeight = 0;

        public ObjectPhysics Physics
        {
            get { return physics; }
            set { physics = value; }
        }

        private Vector3 scale;
        private Quaternion q;
        private Vector3 translate;
        private Matrix ori = new Matrix();

        public ObjectShip()
            : base("model")
        {
            UpdateBoundingBox();

            bbRenderer = new BoundingBoxRenderer();
            sailMiddle = this.Fbx.Bones["zagiel2"];
            sailMiddlePlank = this.Fbx.Bones["zagiel2_belka"];
            sailMiddle.Transform.Decompose(out scale, out q, out translate);

            ori = sailMiddle.Transform;

            boneTransforms = new Matrix[this.Fbx.Bones.Count];

            shader = new JustMvp();
            shader.InitParameters();

            foreach (ModelMeshPart part in Fbx.Meshes[20].MeshParts)
            {
                part.Effect = shader.Technique.Clone();
            }
        }

        private float rotation = 0;
        private float rot = MathHelper.ToRadians(1);

        private void KeyPressed()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.S))
            {
                Wind.Force -= 40;
                if (sailHeight >= 0)
                {
                    sailHeight -= 0.05f;
                    power = sailHeight * 50.0f;
                    sailMiddle.Transform *= Matrix.CreateScale(1f, 1f - 0.05f, 1f) * Matrix.CreateTranslation(new Vector3(0f, 7.4f, 0f));
                }
            }
            else if (Wind.Force > 0) Wind.Force -= 15.2f;

            if (keyState.IsKeyDown(Keys.W))
            {
                Wind.Force += 150.1f;
                if (sailHeight <= 1.0)
                {
                    sailHeight += 0.05f;
                    power = sailHeight * 50.0f;
                    sailMiddle.Transform *= Matrix.CreateScale(1f, 1f + 0.05f, 1f) * Matrix.CreateTranslation(new Vector3(0f, -7.4f, 0f));
                }
                else sailMiddle.Transform = Matrix.CreateScale(1) * Matrix.CreateFromQuaternion(q) * Matrix.CreateTranslation(0, 0, 0);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                if (rotation > MathHelper.ToRadians(-90))
                {
                    Physics.ForceOnObject /= 2.0f;
                    rotation -= rot;
                    sailDirection = MathFunctions.AngleToVector(rotation);

                    this.Rotate(rotation, 0, 0);
                    this.Update();

                    sailMiddle.Transform *= Matrix.CreateRotationY(-rot);
                    sailMiddle.Transform.Decompose(out scale, out q, out translate);
                    sailMiddlePlank.Transform *= Matrix.CreateRotationY(-rot);
                }
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                if (rotation < MathHelper.ToRadians(90))
                {
                    Physics.ForceOnObject /= 2.0f;
                    rotation += rot;
                    sailDirection = MathFunctions.AngleToVector(rotation);

                    this.Rotate(rotation, 0, 0);
                    this.Update();

                    sailMiddle.Transform *= Matrix.CreateRotationY(rot);
                    sailMiddle.Transform.Decompose(out scale, out q, out translate);
                    sailMiddlePlank.Transform *= Matrix.CreateRotationY(rot);
                }
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                //this.Physics.Velocity = Vector3.Zero;
                this.Physics.ForceOnObject = -0.5f * this.Physics.ForceOnObject;
            }
        }

        public void Update(float time)
        {
            this.Physics.SailDirection = sailDirection;
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
                        currentEffect.Parameters["power"].SetValue(power);
                    }
                    mesh.Draw();
                }

                //bbRenderer.Render(BoundingBoxes[i++], ModelMatrix, fx.View, fx.Projection);
            }
        }
    }
}