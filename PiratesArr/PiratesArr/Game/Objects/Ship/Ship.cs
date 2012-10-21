using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Objects.Namespace_Ship
{
    public class Ship
    {
        private Model model;
        private Matrix modelMatrix = Matrix.Identity;
        private static Main mainInstance;
        private Effect basic;

        public Ship(string assetname)
        {
            mainInstance = Main.GetInstance();
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
            
            model = LoadModel("Models//" + assetname, basic);
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

        public void DrawModel(Matrix VP)
        {
            Matrix[] modelTansforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(modelTansforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect currentEffect in mesh.Effects)
                {
                    currentEffect.Parameters["mat_MVP"].SetValue(modelTansforms[mesh.ParentBone.Index] * modelMatrix*VP);
                }
                mesh.Draw();
            }
        }
    }
}