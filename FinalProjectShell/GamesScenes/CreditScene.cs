using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllInOneMono.Drawable;
using FinalProjectShell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    public class CreditScene : GameScene
    {
        public CreditScene(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            // create and add any components that belong to 
            // this scene to the Scene components list
            this.AddComponent(new Background(Game));
            this.AddComponent(new Credit(Game, "fonts\\hudFont", HudLocation.CenterScreen));
            this.Hide();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }


    }
}
