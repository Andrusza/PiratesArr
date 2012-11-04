using System;
using System.Runtime.Serialization;
using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;
using Pirates.Shaders;
using Pirates.Utility;

namespace Pirates.Screens.Scene
{
    [Serializable()]
    public partial class TerrainScreen : BaseMode
    {
        private FirstPersonCamera camera;
        private float aspectRatio = BaseClass.GetInstance().AspectRatio;

        private Matrix projectionMatrix;
        private Matrix reflectionViewMatrix;

        private RasterizerState rs;

        private MultiTextured effect;
        private waterShader waterShader;
        private JustMvp mvpshader;
        private Scattaring scattering;

        private Terrain island;
        private Terrain water;
        private GameObject skydome;

        private RenderTarget2D refractionRenderTarget;
        private Texture2D refractionMap;

        private RenderTarget2D reflectionRenderTarget;
        private Texture2D reflectionMap;

        private const float waterHeight = 5.0f;

        public TerrainScreen()
        {
            camera = new FirstPersonCamera(new Vector3(0, 10, 0));
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 1, 10000);

            effect = new MultiTextured();
            {
                effect.ProjectionMatrix = projectionMatrix;
                effect.ViewMatrix = camera.View;
                effect.Fx_LightPosition.SetValue(new Vector3(1000, 350, 0));
                effect.InitParameters();
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
            water = new Terrain("map2", 10, 1);
            water.Translate(0, 30, 0);
            water.Update();

            skydome = new GameObject("skydome4", scattering);
            skydome.Scale(1200);
            skydome.Rotate(-90, new Vector3(1, 0, 0));
            skydome.Translate(0, 30, 0);
            skydome.Update();
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
                effect.Fx_LightPosition.SetValue(new Vector3(1000, 350, 0));
                effect.InitParameters();
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

            island = new Terrain("island4", 2, 1);
            water = new Terrain("map2", 10, 1);
            water.Translate(0, 30, 0);
            water.Update();

            this.skydome = (GameObject)info.GetValue("Skydome", typeof(GameObject));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("AspectRatio", this.aspectRatio);
            info.AddValue("ProjectionMatrix", this.projectionMatrix);
            info.AddValue("Camera", this.camera);
            info.AddValue("Skydome", this.skydome);
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