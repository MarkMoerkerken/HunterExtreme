using FinalProject;
using HunterExtreme.Drawable;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace HunterExtreme.Manager
{
    class TrashManager : GameComponent
    {
        double spawnFaster = 6;
        double secondsSinceSpawn = 5;
        public double SpawnTimer = 5;

        const double SPAWN_FASTER_TIMER = 6;
        const double MAX_SPAWN_RATE = 1.5;

        Random random = new Random();
        public List<Trash> TrashList = new List<Trash>();
        
        public TrashManager(Game game) : base(game)
        {

        }

        public override void Update(GameTime gameTime)
        {
            secondsSinceSpawn += gameTime.ElapsedGameTime.TotalSeconds;
            spawnFaster += gameTime.ElapsedGameTime.TotalSeconds;
            if (secondsSinceSpawn > SpawnTimer)
            {
                Trash trash = new Trash(Game, GetRandomPostion());
                Game.Components.Add(trash);
                TrashList.Add(trash);
                secondsSinceSpawn = 0.0;
            }
            if(spawnFaster > SPAWN_FASTER_TIMER && SpawnTimer > MAX_SPAWN_RATE)
            {
                SpawnTimer -= 0.3;
                spawnFaster = 0.0;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Gets a random postion where the trash where fall from
        /// </summary>
        /// <returns></returns>
        private Vector2 GetRandomPostion()
        {
            int X = random.Next(0, Game.GraphicsDevice.Viewport.Width);
            int Y = (0);
            return new Vector2(X, Y);
        }

    }
}
