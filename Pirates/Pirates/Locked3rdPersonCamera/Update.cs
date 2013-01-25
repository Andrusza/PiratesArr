using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Pirates.Cameras
{
    public partial class ArcBallCamera
    {
        public Matrix Update()
        {
            MouseEvents();
            return this.View;
        }
    }
}