using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirates.Loaders.Clouds
{
    class CloudInstancer:Instancer
    {
        private float x = 0;

        public override void Update(float time)
        {
            x += 0.001f;

            this.Rotate(x, x, 0);
            this.Update();
        }
    }
}
