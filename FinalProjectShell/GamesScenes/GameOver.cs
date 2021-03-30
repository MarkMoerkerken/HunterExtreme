using AllInOneMono.Drawable;
using FinalProjectShell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    public class GameOver : GameScene
    {
        public GameOver(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            // create and add any components that belong to 
            // this scene to the Scene components list
            this.AddComponent(new Background(Game));
            this.AddComponent(new GameOverHud(Game, "fonts\\hudFont", HudLocation.CenterScreen));
            this.Hide();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Score score = Game.Services.GetService<Score>();
                    GameOverHud gameOverHud = Game.Services.GetService<GameOverHud>();
                    AnimalsDead dead = Game.Services.GetService<AnimalsDead>();
                    gameOverHud.Write();
                    score.score = 0;
                    dead.Dead = 0;
                    ((Game1)Game).HideAllScenes();
                    Game.Services.GetService<StartScene>().Show();
                }
            }
            base.Update(gameTime);
        }
        
    }
}
