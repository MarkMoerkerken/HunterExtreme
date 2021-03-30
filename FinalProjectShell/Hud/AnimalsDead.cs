using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProject
{
    class AnimalsDead : Hud
    {
        public int Dead;

        public bool GameOver = false;

        public AnimalsDead(Game game, string fontName, HudLocation screenLocation)
            : base(game, fontName, screenLocation)
        {
            if (Game.Services.GetService<AnimalsDead>() == null)
            {
                Game.Services.AddService<AnimalsDead>(this);
            }
        }

        public override void Update(GameTime gameTime)
        {
            displayString = $"DeadAnimals: {Dead}";
            base.Update(gameTime);
        }
        /// <summary>
        /// Adds dead animals to the count
        /// </summary>
        public void AddDeadAnimal()
        {
            Dead += 1;
        }
    }
}

