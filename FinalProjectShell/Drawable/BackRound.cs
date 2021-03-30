using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace AllInOneMono.Drawable
{
    class Background : DrawableGameComponent
    {
        Texture2D background;
        
        public Background(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.Draw(background, new Rectangle(0, 0, Game.GraphicsDevice.Viewport.Width, Game.GraphicsDevice.Viewport.Height), Color.White);
            sb.End();
            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("back2");

            base.LoadContent();
        }
    }
}
