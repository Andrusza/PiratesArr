﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.Terrain
{
    public class Terrain
    {
        private Effect basic;

        private Texture2D heightMapTexture;
        private Texture2D sand;

        private static Main mainInstance;

        private uint vertexCountX;
        private uint vertexCountZ;

        private uint blockScale;
        private uint heightScale;

        private uint numTriangles;
        private uint numVertices;

        private VertexBuffer vbo;

        public VertexBuffer Vbo
        {
            get { return vbo; }
            set { vbo = value; }
        }
        private IndexBuffer ibo;

        public IndexBuffer Ibo
        {
            get { return ibo; }
            set { ibo = value; }
        }

        private Color[] heightMap;

        public void Draw()
        {
           
        }

        public Terrain(string assetName)
        {
            mainInstance = Main.GetInstance();
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
            heightMapTexture = mainInstance.Content.Load<Texture2D>("Heightmap//"+assetName);

            int heightMapSize = heightMapTexture.Width * heightMapTexture.Height;

            heightMap = new Color[heightMapSize];
            heightMapTexture.GetData<Color>(heightMap);

            vertexCountX = (uint)heightMapTexture.Width;
            vertexCountZ = (uint)heightMapTexture.Height;
            blockScale = 30;
            heightScale = 3;

            GenerateTerrainMesh();
        }

        private void GenerateTerrainMesh()
        {
            numVertices = vertexCountX * vertexCountZ;
            numTriangles = (vertexCountX - 1) * (vertexCountZ - 1) * 2;

            uint[] indices = GenerateTerrainIndices();

            VertexPositionNormalTangentBinormalTexture[] vertices = GenerateTerrainVertices();
            GenerateTerrainNormals(vertices, indices);
            GenerateTerrainTangentBinormal(vertices, indices);

          
            Vbo = new VertexBuffer(mainInstance.GraphicsDevice, VertexPositionNormalTangentBinormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            Vbo.SetData(vertices);
            Ibo = new IndexBuffer(mainInstance.GraphicsDevice, typeof(uint), indices.Length, BufferUsage.WriteOnly);
            Ibo.SetData(indices);
        }

        private VertexPositionNormalTangentBinormalTexture[] GenerateTerrainVertices()
        {
            float terrainWidth = (vertexCountX - 1) * blockScale;
            float terrainDepth = (vertexCountZ - 1) * blockScale;
            float halfTerrainWidth = terrainWidth * 0.5f;
            float halfTerrainDepth = terrainDepth * 0.5f;

            float tu = 0;
            float tv = 0;
            float tuDerivative = 1.0f / (vertexCountX - 1);
            float tvDerivative = 1.0f / (vertexCountZ - 1);

            VertexPositionNormalTangentBinormalTexture[] vertices = new VertexPositionNormalTangentBinormalTexture[numVertices];

            int count = 0;
            for (float i = -halfTerrainDepth; i <= halfTerrainDepth; i += blockScale)
            {
                tu = 0.0f;
                for (float j = -halfTerrainWidth; j <= halfTerrainWidth; j += blockScale)
                {
                    vertices[count].Position = new Vector3(j, heightMap[count].R * heightScale, i);
                    vertices[count].TextureCoordinate = new Vector2(tu, tv);

                    tu += tuDerivative;
                    count++;
                }
                tv += tvDerivative;
            }
            return vertices;
        }

        private void GenerateTerrainNormals(VertexPositionNormalTangentBinormalTexture[] vertices, uint[] indices)
        {
            for (uint i = 0; i < indices.Length; i += 3)
            {
                // Get the vertex position (v1, v2, and v3)
                Vector3 v1 = vertices[indices[i]].Position;
                Vector3 v2 = vertices[indices[i + 1]].Position;
                uint lol=indices[i + 2];
                Vector3 v3 = vertices[lol].Position;

                // Calculate vectors v1->v3 and v1->v2 and the normal as a cross product
                Vector3 vu = v3 - v1;
                Vector3 vt = v2 - v1;
                Vector3 normal = Vector3.Cross(vu, vt);
                normal.Normalize();

                vertices[indices[i]].Normal += normal;
                vertices[indices[i + 1]].Normal += normal;
                vertices[indices[i + 2]].Normal += normal;
            }

            for (int i = 0; i < vertices.Length; i++)
                vertices[i].Normal.Normalize();
        }

        private uint[] GenerateTerrainIndices()
        {
            uint numIndices = numTriangles * 3;
            uint[] indices = new uint[numIndices];
            uint indicesCount = 0;
            for (uint i = 0; i < (vertexCountZ - 1); i++)
            {
                for (uint j = 0; j < (vertexCountX - 1); j++)
                {
                    uint index = j + i * vertexCountZ;
                    // First triangle
                    indices[indicesCount++] = index;
                    indices[indicesCount++] = index + 1;
                    indices[indicesCount++] = index + vertexCountX + 1;
                    // Second triangle
                    indices[indicesCount++] = index + vertexCountX + 1;
                    indices[indicesCount++] = index + vertexCountX;
                    indices[indicesCount++] = index;
                }
            }
            return indices;
        }

        public void GenerateTerrainTangentBinormal(VertexPositionNormalTangentBinormalTexture[] vertices, uint[] indices)
        {
            for (uint i = 0; i < vertexCountZ; i++)
            {
                for (uint j = 0; j < vertexCountX; j++)
                {
                    uint vertexIndex = j + i * vertexCountX;
                    Vector3 v1 = vertices[vertexIndex].Position;
                    // Calculate the tangent vector
                    if (j < vertexCountX - 1)
                    {
                        Vector3 v2 = vertices[vertexIndex + 1].Position;
                        vertices[vertexIndex].Tangent = (v2 - v1);
                    }
                    // Special case: last vertex of the plane in the X axis
                    else
                    {
                        Vector3 v2 = vertices[vertexIndex - 1].Position;
                        vertices[vertexIndex].Tangent = (v1 - v2);
                    }
                    // Calculate binormal as a cross product (Tangent x Normal)
                    vertices[vertexIndex].Tangent.Normalize();
                    vertices[vertexIndex].Binormal = Vector3.Cross(
                    vertices[vertexIndex].Tangent, vertices[vertexIndex].Normal);
                }
            }
        }

        public struct VertexPositionNormalTangentBinormalTexture
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 TextureCoordinate;
            public Vector3 Tangent;
            public Vector3 Binormal;

            public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
            (
                new VertexElement(0,VertexElementFormat.Vector3,VertexElementUsage.Position,0),
                new VertexElement(12,VertexElementFormat.Vector3,VertexElementUsage.Normal,0),
                new VertexElement(24,VertexElementFormat.Vector2,VertexElementUsage.TextureCoordinate,0),
                new VertexElement(32,VertexElementFormat.Vector3,VertexElementUsage.Tangent,0),
                new VertexElement(44,VertexElementFormat.Vector3,VertexElementUsage.Binormal,0)
            );
        }
    }
}