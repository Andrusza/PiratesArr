using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;

namespace Pirates.Loaders
{
    public class BoundingBoxOOB
    {
        private Vector3[] corners;
        private Vector3[] transformedCorners;

        public Vector3[] Corners
        {
            get { return transformedCorners; }
            set { corners = value; }
        }

        public BoundingBoxOOB(BoundingBox box)
        {
            corners = box.GetCorners();
            transformedCorners = new Vector3[(corners.Length)];
        }

        public void Update(Matrix modelMatrix)
        {
            for (int j = 0; j < 8; j++)
            {
                transformedCorners[j] = Vector3.Transform(corners[j], modelMatrix);
            }
        }
    }

    abstract public class ObjectMesh : ObjectGeometry
    {
        protected Model fbx;
        private List<BoundingBoxOOB> boundingBoxes;

        public List<BoundingBoxOOB> BoundingBoxes
        {
            get { return boundingBoxes; }
            set { boundingBoxes = value; }
        }

        public Model Fbx
        {
            get { return fbx; }
            set { fbx = value; }
        }

        public ObjectMesh(string assetname, BaseShader effect)
            : base()
        {
            fbx = LoadModel(assetname, effect.Technique);
        }

        public ObjectMesh(string assetname)
            : base()
        {
            fbx = ContentLoader.Load<Model>(ContentType.FBX, assetname);
            CreateBoundingBoxes();
        }

        public void CreateBoundingBoxes()
        {
            boundingBoxes = new List<BoundingBoxOOB>();

            Matrix[] transforms = new Matrix[Fbx.Bones.Count];
            Fbx.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in Fbx.Meshes)
            {
                Matrix meshTransform = transforms[mesh.ParentBone.Index];
                BoundingBoxes.Add(BuildBoundingBox(mesh, meshTransform));
            }
        }

        private BoundingBoxOOB BuildBoundingBox(ModelMesh mesh, Matrix meshTransform)
        {
            // Create initial variables to hold min and max xyz values for the mesh
            Vector3 meshMax = new Vector3(float.MinValue);
            Vector3 meshMin = new Vector3(float.MaxValue);

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                // The stride is how big, in bytes, one vertex is in the vertex buffer
                // We have to use this as we do not know the make up of the vertex
                int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

                VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[part.NumVertices];
                part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, stride);

                // Find minimum and maximum xyz values for this mesh part
                Vector3 vertPosition = new Vector3();

                for (int i = 0; i < vertexData.Length; i++)
                {
                    vertPosition = vertexData[i].Position;

                    // update our values from this vertex
                    meshMin = Vector3.Min(meshMin, vertPosition);
                    meshMax = Vector3.Max(meshMax, vertPosition);
                }
            }

            // transform by mesh bone matrix
            meshMin = Vector3.Transform(meshMin, meshTransform);
            meshMax = Vector3.Transform(meshMax, meshTransform);

            // Create the bounding box
            BoundingBox box = new BoundingBox(meshMin, meshMax);
            return new BoundingBoxOOB(box);
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

        public void UpdateBoundingBox()
        {
            for (int i = 0; i < boundingBoxes.Count; i++)
            {
                boundingBoxes[i].Update(modelMatrix);
            }
        }
    }
}