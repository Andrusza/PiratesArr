using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Terrain
{
    public partial class Tera : Mode
    {
        public override void LoadContent()
        {
         
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}