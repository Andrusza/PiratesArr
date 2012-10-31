using System.Runtime.Serialization;
using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Shaders;
using Pirates.Utility;
using System;
using Pirates.Loaders;

namespace Pirates.Screens.Scene
{
    [Serializable()]
    public partial class TerrainScreen : BaseMode
    {
        private FirstPersonCamera camera;
        private float aspectRatio = BaseClass.GetInstance().AspectRatio;

        private Matrix projectionMatrix;
        private RasterizerState rs;

        private MultiTextured effect;
        Terrain island;

        public TerrainScreen()
        {
            camera = new FirstPersonCamera(new Vector3(0, 10, 0));

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);

            effect = new MultiTextured();
            {
                effect.ProjectionMatrix = projectionMatrix;
                effect.ViewMatrix = camera.View;
                effect.InitParameters();
            }

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;

            island = new Terrain("island4", 2, 1);
           
        }

        public TerrainScreen(SerializationInfo info, StreamingContext ctxt)
        {
            this.camera = (FirstPersonCamera)info.GetValue("Camera", typeof(FirstPersonCamera));
            this.aspectRatio = (float)info.GetValue("AspectRatio", typeof(float));
            this.projectionMatrix = (Matrix)info.GetValue("ProjectionMatrix", typeof(Matrix));
            this.rs = new RasterizerState();
            effect = new MultiTextured();
            {
                effect.ProjectionMatrix = projectionMatrix;
                effect.ViewMatrix = camera.View;
                effect.InitParameters();
            }
            this.island = new Terrain("island4", 2, 1);
           
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("AspectRatio", this.aspectRatio);
            info.AddValue("ProjectionMatrix", this.projectionMatrix);
            info.AddValue("Camera", this.camera);
        }

        public override void ToFile()
        {
            Serializer serializer = new Serializer();
            serializer.SerializeObject<TerrainScreen>("save.txt", this);
        }

        public static TerrainScreen FromFile()
        {
            Serializer serializer = new Serializer();
            TerrainScreen obj= serializer.DeSerializeObject<TerrainScreen>("save.txt");
            return obj;
        }
    }
}