using System;
using System.Runtime.Serialization;
using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;
using Pirates.Shaders;
using Pirates.Utility;
using Pirates.Loaders.ModelsFbx;

namespace Pirates.Screens.Scene
{
    [Serializable()]
    public partial class TerrainScreen : BaseMode
    {
        private FirstPersonCamera camera;
        private float aspectRatio = BaseClass.GetInstance().AspectRatio;
        private Matrix projectionMatrix;

        private RasterizerState rs;

        private MultiTextured islandShader;
        private waterShader waterShader;
        private JustMvp mvpshader;
        private Scattaring scattering;

        private Terrain island;
        private Terrain water;
        private ObjectSkydome skydome;
        private ObjectShip ship;

        private RenderTarget2D refractionRenderTarget;
        private Texture2D refractionMap;

        private RenderTarget2D reflectionRenderTarget;
        private Texture2D reflectionMap;
       

        private const float waterHeight = 30.0f;

        public TerrainScreen()
        {
            camera = new FirstPersonCamera(new Vector3(0, 10, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);

            islandShader = new MultiTextured();
            {
                islandShader.ProjectionMatrix = projectionMatrix;
                islandShader.ViewMatrix = camera.View;
                islandShader.InitParameters();
            }

            mvpshader = new JustMvp();
            {
                mvpshader.ProjectionMatrix = projectionMatrix;
                mvpshader.ViewMatrix = camera.View;
                mvpshader.InitParameters();
            }

            scattering = new Scattaring();
            {
                scattering.ProjectionMatrix = projectionMatrix;
                scattering.ViewMatrix = camera.View;
                scattering.InitParameters();
            }

            waterShader = new waterShader();
            {
                waterShader.ProjectionMatrix = projectionMatrix;
                waterShader.ViewMatrix = camera.View;
                waterShader.InitParameters();
            }

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;

            island = new Terrain("island4", 2, 1);
            island.Translate(0, 0, 0);
            island.Update();

            water = new Terrain("map2", 10, 1);
            water.Translate(0, 30, 0);
            water.Update();

            skydome = new ObjectSkydome(scattering);
            skydome.Scale(1200);
            skydome.Rotate(-90, new Vector3(1, 0, 0));
            skydome.Update();
            
            ship = new ObjectShip(mvpshader);
            ship.Scale(0.3f);
            ship.Translate(500, 30, 500);
            ship.Update();
        }

        public TerrainScreen(SerializationInfo info, StreamingContext ctxt)
        {
            this.camera = (FirstPersonCamera)info.GetValue("Camera", typeof(FirstPersonCamera));
            this.aspectRatio = (float)info.GetValue("AspectRatio", typeof(float));
            this.projectionMatrix = (Matrix)info.GetValue("ProjectionMatrix", typeof(Matrix));
            this.rs = new RasterizerState();

            islandShader = new MultiTextured();
            {
                islandShader.ProjectionMatrix = projectionMatrix;
                islandShader.ViewMatrix = camera.View;

                islandShader.InitParameters();
            }

            mvpshader = new JustMvp();
            {
                mvpshader.ProjectionMatrix = projectionMatrix;
                mvpshader.ViewMatrix = camera.View;
                mvpshader.InitParameters();
            }

            scattering = new Scattaring();
            {
                scattering.ProjectionMatrix = projectionMatrix;
                scattering.ViewMatrix = camera.View;
                scattering.InitParameters();
            }

            waterShader = new waterShader();
            {
                waterShader.ProjectionMatrix = projectionMatrix;
                waterShader.ViewMatrix = camera.View;
                waterShader.InitParameters();
            }

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;

            island = new Terrain("island4", 2, 1);
            water = new Terrain("map2", 1, 1);
            water.Translate(0, 30, 0);
            water.Update();

            //skydome = new ObjectMesh("skydome4", scattering);
            //skydome.Scale(1200);
            //skydome.Rotate(-90, new Vector3(1, 0, 0));
            //skydome.Translate(0, 30, 0);
            //skydome.Update();

            //ship = new ObjectMesh("ship2", mvpshader);
            //ship.Scale(0.3f);
            //ship.Rotate(-90, new Vector3(1, 0, 0));
            //ship.Translate(500, 39, 500);
            //ship.Update();
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
            TerrainScreen obj = serializer.DeSerializeObject<TerrainScreen>("save.txt");
            return obj;
        }
    }
}