using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    
    class GameOverHud : Hud
    {
        public int[] HighScore = new int[10];
        bool over = false;
        public GameOverHud(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<GameOverHud>() == null)
            {
                Game.Services.AddService<GameOverHud>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            Score score = Game.Services.GetService<Score>();
            displayString = $"Game Over!\nYour Score: {score.score}";
            base.Update(gameTime);
        }

        /// <summary>
        /// Writes the highscore array to a file
        /// </summary>
        public void Write()
        {
            Score score = Game.Services.GetService<Score>();
            string file = @"Score.txt";
            if (score.score > HighScore[9])
            {
                HighScore[9] = score.score;
                Array.Sort(HighScore);
                Array.Reverse(HighScore);
            }
            if (!File.Exists(file))
            {
                File.Create(file);
            }

            using (StreamWriter writer = new StreamWriter(file))
            {
                for (int i = 0; i < HighScore.Length; i++)
                {
                    writer.WriteLine(HighScore[i]);
                }
                writer.Close();
            }
        }
    }
}