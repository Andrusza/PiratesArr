using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Objects.Namespace_Ship
{
    public class Ship
    {
        private Model model;
        private Matrix modelMatrix = Matrix.Identity;
        private static Main mainInstance;

        public Ship(string assetname, Effect effect)
        {
            mainInstance = Main.GetInstance();
            model = LoadModel("Models//" + assetname, effect);
        }

        private Model LoadModel(string assetName, Effect technique)
        {
            Model newModel = mainInstance.Content.Load<Model>(assetName);
            foreach (ModelMesh mesh in newModel.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = technique.Clone();
                }
            }
            return newModel;
        }

        public void Update()
        {
        }

        public void DrawModel(Matrix View, Matrix Projection, Vector4 eye)
        {
            Matrix[] modelTansforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTansforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    currentEffect.Parameters["vec4_Eye"].SetValue(eye);
                    currentEffect.Parameters["mat_View"].SetValue(View);
                    currentEffect.Parameters["mat_Projection"].SetValue(Projection);
                    currentEffect.Parameters["mat_World"].SetValue(modelTansforms[mesh.ParentBone.Index] * modelMatrix);
                }
                mesh.Draw();
            }
        }
    }
}