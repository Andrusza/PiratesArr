using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Objects.Namespace_Ship;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        private Effect effect;

        private RasterizerState rs;

        private FirstPersonCamera camera;

        private Ship playerShip;

        public Scene()
            : base()
        {
            camera = new FirstPersonCamera(new Vector3(0, 0, 0), mainInstance.GraphicsDevice.Viewport.AspectRatio, 1f, 1000f);
            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }
    }
}