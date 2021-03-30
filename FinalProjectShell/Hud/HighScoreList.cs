using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace FinalProject
{
    class HighScoreList : Hud
    {
       public HighScoreList(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<HighScoreList>() == null)
            {
                Game.Services.AddService<HighScoreList>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = GetHighScore();
            base.Update(gameTime);
        }

        /// <summary>
        /// gets the highscore array
        /// </summary>
        public string GetHighScore()
        {
            GameOverHud gameOverHud = Game.Services.GetService<GameOverHud>();
            string file = "Score.txt";
            StreamReader reader = new StreamReader(file);
            displayString = "";
            if (!File.Exists(file))
            {
                File.Create(file);
            }
            else
            {
                int i = 0;
                while (reader.EndOfStream == false)
                {
                    string highScores = reader.ReadLine();
                    try
                    {
                        gameOverHud.HighScore[i] = Convert.ToInt32(highScores);
                    }
                    catch
                    {
                        gameOverHud.HighScore[i] = 0;
                    }
                    displayString += gameOverHud.HighScore[i] + "\n";
                    i++;
                }
                reader.Dispose();
            }
            return displayString;
        }
    }
}