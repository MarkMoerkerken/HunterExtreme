using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class Credit : Hud
    {
        public Credit(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Credit>() == null)
            {
                Game.Services.AddService<Credit>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = $"Made by: Mark Moerkerken\n" +
                            $"All art and asset were retrieved from open game art\n " +
                            $"This game purpose is to encourge people to stop litering";
            base.Update(gameTime);
        }


    }
}