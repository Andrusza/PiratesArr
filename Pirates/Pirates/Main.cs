using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Screens;
using Pirates.Screens.Scene;
using Pirates.Utility;
using Pirates.Window;

namespace Pirates
{
    public class BaseClass : Microsoft.Xna.Framework.Game
    {
        private static GraphicsDeviceManager graphics;

        public static GraphicsDeviceManager Graphics
        {
            get { return graphics; }
            set { graphics = value; }
        }

        private static GraphicsDevice device;

        public static GraphicsDevice Device
        {
            get { return device; }
            set { device = value; }
        }

        private BaseMode renderMode;

        private XnaWindow window;
        private FPScounter fps;
        private float aspectRatio;

        public float AspectRatio
        {
            get { return aspectRatio; }
            set { aspectRatio = value; }
        }

        public BaseClass()
        {
            window = new XnaWindow();
            this.IsFixedTimeStep = false;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = XnaWindow.Height;
            graphics.PreferredBackBufferWidth = XnaWindow.Width;

            this.IsMouseVisible = window.IsMouseVisible;
            this.Window.AllowUserResizing = window.IsResizing;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Device = this.GraphicsDevice;
            fps = new FPScounter(Device);
            AspectRatio = this.GraphicsDevice.Viewport.AspectRatio;

            //if (File.Exists("save.txt"))
            //{
            //    renderMode = TerrainScreen.FromFile();
            //}
            //else
            //{
            //    renderMode = new TerrainScreen();
            //}
            renderMode = new TerrainScreen();
            this.renderMode.LoadContent();
        }

        protected override void LoadContent()
        {
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            renderMode.Update(gameTime);
            fps.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.CornflowerBlue);
            renderMode.Draw(gameTime);
            fps.Draw();
            base.Draw(gameTime);
        }

        protected override void OnExiting(Object sender, EventArgs args)
        {
            renderMode.ToFile();
            base.OnExiting(sender, args);
        }

        private static BaseClass BaseClassInstance;
        private static object syncLock = new object();

        public static BaseClass GetInstance()
        {
            if (BaseClassInstance == null)
            {
                lock (syncLock)
                {
                    if (BaseClassInstance == null)
                    {
                        BaseClassInstance = new BaseClass();
                    }
                }
            }
            return BaseClassInstance;
        }
    }
}