using System.Windows.Forms;
using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.GUI.LoadCursor;
using PiratesArr.MainLoop;

namespace PiratesArr
{
    public partial class Main : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDeviceManager graphicMenager;

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
            graphicMenager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphicMenager.PreferredBackBufferHeight = 600;
            graphicMenager.PreferredBackBufferWidth = 800;

            this.IsMouseVisible = true;
            Cursor myCursor = CustomCursor.LoadCustomCursor(@"Content\Cursors\PIRATE.ani");

            Form winForm = (Form)Form.FromHandle(Window.Handle);
            winForm.Cursor = myCursor;

            this.Window.AllowUserResizing = true;
        }

        protected override void Initialize()
        {
            this.IsFixedTimeStep = false;
            base.Initialize();
            mainInstance = this;

            fps = new FPScounter();
            //renderMode = new Intro();
            //renderMode = new Scene();
            renderMode = new Tera();
        }

        #region Singleton

        private static Main mainInstance;
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

        #endregion Singleton
    }
}