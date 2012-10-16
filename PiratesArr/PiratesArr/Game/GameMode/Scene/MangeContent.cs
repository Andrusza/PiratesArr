using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode.Scene
{
    public partial class Scene : Mode
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