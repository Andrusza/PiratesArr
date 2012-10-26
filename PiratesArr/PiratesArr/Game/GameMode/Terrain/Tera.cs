using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Surface;
using PiratesArr.Game.Objects.Namespace_Ship;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        private RasterizerState rs;

        private Terrain tera;
        private Effect effect;
        private Effect effect2;

        Ship playerShip;

        private Random rng = new Random();

        private FirstPersonCamera camera;

        public Tera()
            : base()
        {
            camera = new FirstPersonCamera(new Vector3(0, -500, 0), mainInstance.GraphicsDevice.Viewport.AspectRatio, 1f, 100000f);
            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }
    }
}