using Microsoft.Xna.Framework.Graphics;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Scene : Mode
    {
        public override void LoadContent()
        {
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}