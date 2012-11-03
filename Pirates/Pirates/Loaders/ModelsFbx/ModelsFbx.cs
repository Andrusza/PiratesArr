using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    public class GameObject : BaseObject
    {
        private Model fbx;

        public GameObject(string assetname, BaseShader effect)
            : base()
        {
            fbx = LoadModel(assetname, effect.Technique);
        }

        private Model LoadModel(string assetName, Effect technique)
        {
            Model newModel = ContentLoader.Load<Model>(ContentType.FBX, assetName);
            foreach (ModelMesh mesh in newModel.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = technique.Clone();
                }
            }
            return newModel;
        }

        public void Update(Effect effect)
        {
            foreach (ModelMesh mesh in fbx.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    meshPart.Effect = effect.Clone();
                }
            }
        }

        public void DrawModel()
        {
            Matrix[] modelTansforms = new Matrix[fbx.Bones.Count];
            fbx.CopyAbsoluteBoneTransformsTo(modelTansforms);

            foreach (ModelMesh mesh in fbx.Meshes)
            {
                
                mesh.Draw();
            }
        }
    }
}