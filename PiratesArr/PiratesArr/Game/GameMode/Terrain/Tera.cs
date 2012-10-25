using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;
using PiratesArr.Game.Surface;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        private Matrix VP;
        private RasterizerState rs;

        private Terrain tera;
        Effect effect;

        private FirstPersonCamera camera = new FirstPersonCamera(new Vector3(0, 100, 0));
     
        public Tera(): base()
        {
            rs = new RasterizerState();
            rs.CullMode = CullMode.None;
        }
    }
}