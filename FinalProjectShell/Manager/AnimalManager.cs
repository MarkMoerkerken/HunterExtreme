using FinalProject;
using HunterExtreme.Drawable;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOneMono.Drawable
{
    class AnimalManager : GameComponent
    {
        Random random = new Random();
        Vector2 speed;
        double SecondsSinceSpawn = 4.5;
        const double SPAWN_TIMER = 4.5;

        
        public AnimalManager(Game game) : base(game)
        {
            
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            SecondsSinceSpawn += gameTime.ElapsedGameTime.TotalSeconds;
            if (SecondsSinceSpawn > SPAWN_TIMER)
            {
                Game.Components.Add(new Animal(Game));

                SecondsSinceSpawn = 0.0;
            }
            base.Update(gameTime);
        }
    }
}
