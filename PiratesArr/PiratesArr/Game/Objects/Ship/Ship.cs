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
            Matrix.CreateScale(1, out modelMatrix);
            modelMatrix = Matrix.CreateRotationZ(MathHelper.ToRadians(-90));
            modelMatrix.Translation += new Vector3(0, 110, 0);
           
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

        public void DrawModel(Matrix View, Matrix Projection)
        {
            Matrix[] modelTansforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTansforms);
            //Matrix.CreateScale(100, out modelMatrix);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    currentEffect.Parameters["View"].SetValue(View);
                    currentEffect.Parameters["Projection"].SetValue(Projection);
                    currentEffect.Parameters["World"].SetValue(modelTansforms[mesh.ParentBone.Index] * (modelMatrix));
                }
                mesh.Draw();
            }
        }
    }
}