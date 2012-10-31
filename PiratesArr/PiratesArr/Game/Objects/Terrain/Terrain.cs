using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Surface
{
    public partial class Terrain
    {
        private Texture2D heightMapTexture;
        private List<Texture2D> texturesAtlas;

        private Matrix worldMatrix = Matrix.Identity;

        private static Main mainInstance;

        private uint vertexCountX;
        private uint vertexCountZ;

        private uint blockScale;
        private uint heightScale;

        private uint numTriangles;
        private uint numVertices;

        private int numIndices;

        private VertexBuffer vbo;
        private IndexBuffer ibo;

        private Color[] heightMap;

        public Terrain(string assetName)
        {
            mainInstance = Main.GetInstance();
            texturesAtlas = new List<Texture2D>();

            heightMapTexture = mainInstance.Content.Load<Texture2D>("Heightmap//" + assetName);
            int heightMapSize = heightMapTexture.Width * heightMapTexture.Height;

            heightMap = new Color[heightMapSize];
            heightMapTexture.GetData<Color>(heightMap);

            vertexCountX = (uint)heightMapTexture.Width;
            vertexCountZ = (uint)heightMapTexture.Height;
            blockScale = 1;
            heightScale = 1;

            GenerateTerrainMesh();
        }

        private void GenerateTerrainMesh()
        {
            numVertices = vertexCountX * vertexCountZ;
            numTriangles = (vertexCountX - 1) * (vertexCountZ - 1) * 2;

            uint[] indices = GenerateTerrainIndices();
            numIndices = indices.Length;

            VertexPositionNormalTangentBinormalTexture[] vertices = GenerateTerrainVertices();
            GenerateTerrainNormals(vertices, indices);
            GenerateTerrainTangentBinormal(vertices, indices);

            vbo = new VertexBuffer(mainInstance.GraphicsDevice, VertexPositionNormalTangentBinormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vbo.SetData(vertices);

            ibo = new IndexBuffer(mainInstance.GraphicsDevice, typeof(uint), indices.Length, BufferUsage.WriteOnly);
            ibo.SetData(indices);
        }

        public void AddTexture(string path, string name)
        {
            Texture2D temp = mainInstance.Content.Load<Texture2D>("Textures//" + path);
            temp.Name = name;
            texturesAtlas.Add(temp);
        }

        public void BindTextures(Effect effect)
        {
            foreach (Texture2D tex in texturesAtlas)
            {
                effect.Parameters[tex.Name].SetValue(tex);
            }
        }
    }
}