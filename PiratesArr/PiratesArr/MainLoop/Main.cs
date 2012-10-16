using System.Windows.Forms;
using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.GameMode.Intro;
using PiratesArr.GUI.LoadCursor;
using PiratesArr.MainLoop;

namespace PiratesArr
{
    public partial class Main : Microsoft.Xna.Framework.Game
    {
        private Mode renderMode;
        private FPScounter fps;

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
            renderMode = new Intro();
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

        public static GraphicsDeviceManager GetInstance(Main instance)
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