using System;
using System.Runtime.Serialization;
using Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Loaders;
using Pirates.Loaders.Cloud;
using Pirates.Loaders.ModelsFbx;
using Pirates.Loaders.Rain;
using Pirates.Shaders;
using Pirates.Shaders.Rain;
using Pirates.Utility;
using Pirates.Weather;
using Pirates.Physics;

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
        private WaterShader waterShader;
        private JustMvp mvpshader;
        private BasicEffect shipShader;
        private Scattaring scatteringShader;
        private CloudShader cloudShader;
        private RainDropsShader rainDropsShader;
        private RainShader rainShader;
        private Pirates.Shaders.Fog fogShader;

        private Terrain island;
        private Terrain water;
        private ObjectSkydome skydome;
        private ObjectShip ship;

        private CloudManager cloudManager;
        private RainManager rainManager;

        private RenderTarget2D currentFrameRenderTarget;
        private Texture2D currentFrame;

        private RenderTarget2D reflectionRenderTarget;
        private Texture2D reflectionMap;

        private RenderTarget2D shadowRenderTarget;
        private Texture2D shadowMap;

        private Plane reflectionPlane;
        private Matrix reflectionMatrix;

        private const float waterHeight = 30.0f;

        public TerrainScreen()
        {
            camera = new FirstPersonCamera(new Vector3(800, 0, -515));
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

            shipShader = new BasicEffect(BaseClass.Device);
            {
                shipShader.Projection = projectionMatrix;
                shipShader.View = camera.View;
            }

            scatteringShader = new Scattaring();
            {
                scatteringShader.ProjectionMatrix = projectionMatrix;
                scatteringShader.ViewMatrix = camera.View;
                scatteringShader.InitParameters();
            }

            waterShader = new WaterShader();
            {
                waterShader.ProjectionMatrix = projectionMatrix;
                waterShader.ViewMatrix = camera.View;
                waterShader.InitParameters();
            }

            cloudShader = new CloudShader();
            {
                cloudShader.ProjectionMatrix = projectionMatrix;
                cloudShader.ViewMatrix = camera.View;
                cloudShader.InitParameters();
            }

            rainShader = new RainShader();
            {
                rainShader.ProjectionMatrix = projectionMatrix;
                rainShader.ViewMatrix = camera.View;
                rainShader.InitParameters();
            }

            rainDropsShader = new RainDropsShader();
            {
                rainDropsShader.InitParameters();
            }

            fogShader = new Pirates.Shaders.Fog();
            {
                fogShader.InitParameters();
            }

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            //rs.FillMode = FillMode.WireFrame;

            cloudManager = new CloudManager();
            rainManager = new RainManager();

            Vector3 minBox = new Vector3(-5000f, 300f, -5000f);
            Vector3 maxBox = new Vector3(5000f, 1500f, 5000f);

            int[] allCloudSprites = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            cloudManager.AddCloud(1000, minBox, maxBox, 0.75f, allCloudSprites);
            cloudManager.Instancer.Update();

            minBox = new Vector3(-800, 320f, -800f);
            maxBox = new Vector3(800f, 0f, 800f);
            rainManager.AddDrop(250000, minBox, maxBox);
            cloudManager.Instancer.Update();

            island = new Terrain("island4", 10, 1);
           

            water = new Terrain("map2", 30, 1);
            water.Translate(0, 40, 0);
            water.Update();
            reflectionPlane = CreatePlane(40, new Vector3(0, -1, 0), true);
            reflectionMatrix = Matrix.CreateReflection(reflectionPlane);

            skydome = new ObjectSkydome(scatteringShader);
            skydome.Scale(3200);
            skydome.Rotate(-90, new Vector3(1, 0, 0));
            skydome.Update();

            Wind.Direction = MathFunctions.AngleToVector(45);
            Wind.Force = new Vector2(0,0f);

            ship = new ObjectShip();
            ship.Physics.material = MaterialType.Island;
            ship.Translate(-500, 20, 100);
            ship.Update();

            if (island.IsOnHeightmap(ship.ModelMatrix.Translation))
            {
                ship.Physics.material = MaterialType.Island;
                island.ColisionWithTerrain(ship);
            }
            else
            {
                ship.Physics.material = MaterialType.Water;
                water.GetObjectPositionOnWater(ship, waterShader);
            }


            ship.Physics.StartPosition(ship.ModelMatrix.Translation);
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

            scatteringShader = new Scattaring();
            {
                scatteringShader.ProjectionMatrix = projectionMatrix;
                scatteringShader.ViewMatrix = camera.View;
                scatteringShader.InitParameters();
            }

            waterShader = new WaterShader();
            {
                waterShader.ProjectionMatrix = projectionMatrix;
                waterShader.ViewMatrix = camera.View;
                waterShader.InitParameters();
            }

            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = FillMode.WireFrame;

            island = new Terrain("island4", 2, 1);
            water = new Terrain("map2", 1, 1);
            water.Translate(0, 30, 0);
            water.Update();

            DepthStencilState depthStencilState = new DepthStencilState();
            depthStencilState.DepthBufferFunction = CompareFunction.LessEqual;
            BaseClass.Device.DepthStencilState = depthStencilState;
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