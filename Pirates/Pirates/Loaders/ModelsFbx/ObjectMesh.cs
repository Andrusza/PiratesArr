using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    abstract public class ObjectMesh : ObjectGeometry
    {
        protected Model fbx;

        public ObjectMesh(string assetname, BaseShader effect)
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
    }
}