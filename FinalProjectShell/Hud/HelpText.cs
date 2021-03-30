using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class HelpText : Hud
    {
       public HelpText(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<HelpText>() == null)
            {
                Game.Services.AddService<HelpText>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = $"In this game you are supposed to collect the trash.\n" +
                            $"Collecting trash gives you points. \n" +
                            $"If the trash hits a animal they lose health or die and the game ends.\n" +
                            $"The arrow keys are you movement keys you can go left right or jump";
            base.Update(gameTime);
        }

        
    }
}