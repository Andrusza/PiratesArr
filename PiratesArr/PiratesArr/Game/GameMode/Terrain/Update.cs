﻿using Microsoft.Xna.Framework;
using PiratesArr.Game.GameMode.BaseMode;

namespace PiratesArr.Game.GameMode
{
    public partial class Tera : Mode
    {
        public override void Update(GameTime gameTime)
        {
            Input();

            VP = camera.Update();
        }
    }
}