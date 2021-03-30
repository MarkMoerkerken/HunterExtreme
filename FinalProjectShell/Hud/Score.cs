using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class Score : Hud
    {
        public int[] HighScore = new int[10];
        public int score=0;
        
        public Score(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {score}";
            base.Update(gameTime);
        }
       

        /// <summary>
        /// Adds scores
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(int value)
        {
            score += 10;
        }
    }
}
