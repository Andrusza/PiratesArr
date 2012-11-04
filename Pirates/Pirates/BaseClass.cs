using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirates.Screens;
using Pirates.Screens.Scene;
using Pirates.Utility;
using Pirates.Window;
using System;

namespace Pirates
{
    public class BaseClass : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
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
            graphics.PreferredBackBufferHeight = window.Height;
            graphics.PreferredBackBufferWidth = window.Width;

            this.IsMouseVisible = window.IsMouseVisible;
            this.Window.AllowUserResizing = window.IsResizing;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Device = this.GraphicsDevice;

            AspectRatio = this.GraphicsDevice.Viewport.AspectRatio;
            //renderMode = new SceneScreen();
            //renderMode = new SkyDomeScreen();
            try
            {
                renderMode = TerrainScreen.FromFile();
            }
            catch (Exception e)
            {
                renderMode = new TerrainScreen();
            }

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