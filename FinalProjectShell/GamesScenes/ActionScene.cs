using AllInOneMono;
using AllInOneMono.Drawable;
using FinalProject;
using HunterExtreme.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class ActionScene : GameScene
    {
        
        public ActionScene (Game game): base(game)
        {
          
        }

        public override void Initialize()
        {
            // create and add any components that belong to this scene
            this.AddComponent(new Background(Game));

            this.AddComponent(new Score(Game, "fonts\\hudFont", HudLocation.TopRight));

            this.AddComponent(new AnimalsDead(Game, "fonts\\hudFont", HudLocation.TopLeft));
            

            AnimalManager animalManager = new AnimalManager(Game);
            this.AddComponent(animalManager);
            Game.Services.AddService<AnimalManager>(animalManager);

            TrashManager trashManager = new TrashManager(Game);
            this.AddComponent(trashManager);
            Game.Services.AddService<TrashManager>(trashManager);

            Hunter hunter = new Hunter(Game);
            this.AddComponent(hunter);
            Game.Services.AddService<Hunter>(hunter);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled )
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Score score = Game.Services.GetService<Score>();
                    AnimalsDead dead = Game.Services.GetService<AnimalsDead>();
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
