using AllInOneMono;
using FinalProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using HunterExtreme.Manager;
using System;

namespace HunterExtreme.Drawable
{
    class Trash : DrawableGameComponent
    {
        Texture2D trash;
        Vector2 trashPosition = Vector2.Zero;
        Vector2 Speed = new Vector2(0, 5);
        public Rectangle trashBoundry;
        bool playEffect = true;
        SoundEffect soundEffects;
        SoundEffect pickup;

        const int YCLAMP = 40;

        public Trash(Game game,Vector2 position) : base(game)
        {
            this.trashPosition = position;
        }
        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(trash, trashPosition, Color.White);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            trashPosition += Speed;
            trashPosition.Y = MathHelper.Clamp(trashPosition.Y, 0, GraphicsDevice.Viewport.Height -YCLAMP);

            if(trashPosition.Y== GraphicsDevice.Viewport.Height - 40 && playEffect==true)
            {
                soundEffects.Play();
                playEffect = false;
            }
            AnimalsDead animalsDead = Game.Services.GetService<AnimalsDead>();
            if (animalsDead.Dead >9)
            {
                TrashManager trashManger = Game.Services.GetService<TrashManager>();
                trashManger.SpawnTimer = 5;
                Game.Components.Remove(this);
                trashManger.TrashList.Remove(this);
                animalsDead.GameOver = false;
           }
            CheckForCollision();
            
            base.Update(gameTime);
        }

        public void RemoveAllTrash()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks For collision between the hunter and trash
        /// </summary>
        private void CheckForCollision()
        {
            trashBoundry = trash.Bounds;
            trashBoundry.Location = trashPosition.ToPoint();

            Hunter hunter = Game.Services.GetService<Hunter>();
            TrashManager trashManger = Game.Services.GetService<TrashManager>();
            if (trashBoundry.Intersects(hunter.HunterBounds))
            {
                Game.Services.GetService<Score>().AddScore(10);
                pickup.Play();
                Game.Components.Remove(this);
                trashManger.TrashList.Remove(this);
                this.Enabled = false;
            }
        }

        protected override void LoadContent()
        {
            soundEffects = Game.Content.Load<SoundEffect>("thud");
            pickup = Game.Content.Load<SoundEffect>("pickup");
            trash = Game.Content.Load<Texture2D>("trash");
            base.LoadContent();
        }
    }
}
