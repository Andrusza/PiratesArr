using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.Camera.FirstPersonCamera;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Terrain
{
    public partial class Tera : Mode
    {
        private Matrix projectionMatrix;
        private Matrix viewMatrix;

        private Matrix VP;
        private RasterizerState rs;

        private PiratesArr.Game.Terrain.Terrain tera;

        private FirstPersonCamera camera = new FirstPersonCamera(new Vector3(0, 100, 0));

        public Tera()
            : base()
        {
            rs = new RasterizerState();
            tera = new PiratesArr.Game.Terrain.Terrain("map2");

            rs.CullMode = CullMode.None;
        }
    }
}