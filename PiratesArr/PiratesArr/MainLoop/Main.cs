using System.Windows.Forms;
using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.GameMode.Scene;
using PiratesArr.GUI.LoadCursor;
using PiratesArr.MainLoop;
using PiratesArr.Game.GameMode.Terrain;

namespace PiratesArr
{
    public partial class Main : Microsoft.Xna.Framework.Game
    {
        private Mode renderMode;
        private FPScounter fps;

        private Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get { return viewMatrix; }
            set { viewMatrix = value; }
        }

        public Mode RenderMode
        {
            get { return renderMode; }
            set { renderMode = value; }
        }

        public Main()
        {
            graphicsInstance = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphicsInstance.PreferredBackBufferHeight = 600;
            graphicsInstance.PreferredBackBufferWidth = 800;

            this.IsMouseVisible = true;
            Cursor myCursor = CustomCursor.LoadCustomCursor(@"Content\Cursors\PIRATE.ani");

            Form winForm = (Form)Form.FromHandle(Window.Handle);
            winForm.Cursor = myCursor;
        }

        protected override void Initialize()
        {
            base.Initialize();
            mainInstance = this;

            fps = new FPScounter();
            // renderMode = new Intro();
            //renderMode = new Scene();
             renderMode = new Tera();
        }

        #region Singleton

        private static Main mainInstance;
        private static GraphicsDeviceManager graphicsInstance;
        private static object syncLock = new object();

        public static Main GetInstance()
        {
            if (mainInstance == null)
            {
                lock (syncLock)
                {
                    if (mainInstance == null)
                    {
                        mainInstance = new Main();
                    }
                }
            }
            return mainInstance;
        }

        public static GraphicsDeviceManager GetInstance(Microsoft.Xna.Framework.Game instance)
        {
            if (graphicsInstance == null)
            {
                lock (syncLock)
                {
                    if (graphicsInstance == null)
                    {
                        graphicsInstance = new GraphicsDeviceManager(instance);
                    }
                }
            }
            return graphicsInstance;
        }

        #endregion Singleton
    }
}