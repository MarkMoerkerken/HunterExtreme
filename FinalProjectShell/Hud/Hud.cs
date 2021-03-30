using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    public enum HudLocation
    {
        TopLeft,
        TopCenter,
        TopRight,
        CenterScreen,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    class Hud : DrawableGameComponent
    {
        protected string displayString;
        protected HudLocation screenLocation;

        SpriteFont font;
        string fontName;


        public Hud(Game game, string fontName, HudLocation screenLocation) : base(game)
        {
            this.fontName = fontName;
            this.screenLocation = screenLocation;
            this.DrawOrder = int.MaxValue;
            displayString = "HUD string not set";
        }

        /// <summary>
        /// Draws the score string to the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(font, displayString, GetPosition(), Color.Green);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// Loads any object assets for the object
        /// </summary>"
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("regularFont");
            base.LoadContent();
        }

        /// <summary>
        /// Gets the Vector2 position of where to place the 
        /// object on screen based on HudLocation and display
        /// string in the given object
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            Vector2 location = Vector2.Zero;

            float stringWidth = font.MeasureString(displayString).X;
            float stringHeight = font.MeasureString(displayString).Y;
            int displayWidth = Game.GraphicsDevice.Viewport.Width;
            int displayHeight = Game.GraphicsDevice.Viewport.Height;


            switch (screenLocation)
            {
                case HudLocation.TopLeft:
                    // display at 0,0
                    break;
                case HudLocation.TopCenter:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = 0;
                    break;
                case HudLocation.TopRight:
                    location.X = displayWidth - stringWidth;
                    location.Y = 0;
                    break;
                case HudLocation.CenterScreen:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = displayHeight / 2 - stringHeight / 2;
                    break;
                case HudLocation.BottomLeft:
                    location.Y = displayHeight - stringHeight;
                    break;
                case HudLocation.BottomCenter:
                    location.X = displayWidth / 2 - stringWidth / 2;
                    location.Y = displayHeight - stringHeight;
                    break;
                case HudLocation.BottomRight:
                    location.X = displayWidth - stringWidth;
                    location.Y = displayHeight - stringHeight;
                    break;
                default:
                    break;
            }

            return location;
        }
    }
}