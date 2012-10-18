using PiratesArr.Game.GameMode.BaseMode;
using Microsoft.Xna.Framework.Graphics;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
    {
        public override void LoadContent()
        {
            basic = mainInstance.Content.Load<Effect>("Shaders//basic");
            basic.CurrentTechnique = basic.Techniques["ColoredNoShading"];

          
        }

        public override void UnloadContent()
        {
            mainInstance.Content.Unload();
        }
    }
}