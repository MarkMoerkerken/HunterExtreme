using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HunterExtreme.Drawable;
using FinalProject;
using HunterExtreme.Manager;
using FinalProjectShell;

namespace AllInOneMono.Drawable
{
    //Enum for which direction the animal is walking
    enum AnimalState
    {
        WalkLeft,
        WalkRight,
    }
    class Animal : DrawableGameComponent
    {
        Texture2D wolf;
        Texture2D deer;
        Texture2D bear;
        Texture2D boar;
        Texture2D fox;
        Texture2D rabbit;
        Texture2D activeTexture;

        Random random = new Random();
        AnimalState animalState = new AnimalState();

        Vector2 Position;
        Vector2 WalkSpeed;
        Rectangle sourceRect;

        double frameTimer = 0.0;
        double secondsSinceLastHit = 1.5;
        int currentFrame = 0;
        int health;
        int tileSize;
        bool hit = false;
        
        int tileCountWalk;
        const double FRAME_DURATION = 0.07;
        
        public Rectangle AnimalBounds
        {
            get
            {
                Rectangle bounds = sourceRect;
                bounds.Location = Position.ToPoint();
                return bounds;
            }
        }
        public Animal(Game game) : base(game)
        {
            
        }
        
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(activeTexture,
                    Position,
                    sourceRect,
                    hit == true ? Color.Green : Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    animalState == AnimalState.WalkRight ?
                                  SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    0f);
            sb.End();
            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        
        public override void Update(GameTime gameTime)
        {

            if (animalState == AnimalState.WalkLeft)
            {
                Position -= WalkSpeed;
            }
            else if (animalState == AnimalState.WalkRight)
            {
                Position += WalkSpeed;
            }

            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                frameTimer = 0;

                if (tileCountWalk <= ++currentFrame)
                {
                    currentFrame = 0;
                }

                sourceRect.X = tileSize * currentFrame;
            }

            if (Position.X == GraphicsDevice.Viewport.Width)
            {
                this.Enabled = false;
                Game.Components.Remove(this);
            }
            CheckForCollision(gameTime);
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            wolf = Game.Content.Load<Texture2D>("Wolf_Walk");
            deer = Game.Content.Load<Texture2D>("Deer_Walk");
            bear = Game.Content.Load<Texture2D>("Bear_Walk");
            boar = Game.Content.Load<Texture2D>("Boar_Walk");
            fox  = Game.Content.Load<Texture2D>("Fox_Walk");
            rabbit = Game.Content.Load<Texture2D>("Rabbit_Hop");

            GetRandomAnimal();
            GetAnimalPostion();
            base.LoadContent();
        }

        /// <summary>
        /// Checks for collision between trash and animal
        /// </summary>
        private void CheckForCollision(GameTime gameTime)
        {
            AnimalsDead animalsDead = Game.Services.GetService<AnimalsDead>();
            TrashManager trashManger = Game.Services.GetService<TrashManager>();
            foreach (Trash trash in trashManger.TrashList)
            {
                if (AnimalBounds.Intersects(trash.trashBoundry))
                {
                    hit = true;
                    secondsSinceLastHit += gameTime.ElapsedGameTime.TotalSeconds;
                    if (secondsSinceLastHit >= 2.5)
                    {
                        health--;
                        secondsSinceLastHit = 0.0;
                    }
                    if (health == 0)
                    {
                        Game.Components.Remove(this);
                        animalsDead.AddDeadAnimal();
                        if (animalsDead.Dead >= 10)
                        {
                            animalsDead.GameOver = true;
                            ((Game1)Game).HideAllScenes();
                            Game.Services.GetService<GameOver>().Show();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get a random location for either side of the screen
        /// </summary>
        private void GetAnimalPostion()
        {
            int rand = random.Next(0, 3);
            if (rand == 1)
            {
                int xPosition = Game.GraphicsDevice.Viewport.Width;
                int yPosition = Game.GraphicsDevice.Viewport.Height - 20 - activeTexture.Height;
                Position = new Vector2(xPosition, yPosition);
                animalState = AnimalState.WalkLeft;
            }
            else
            {
                int xPosition = Game.GraphicsDevice.Viewport.Width - Game.GraphicsDevice.Viewport.Width;
                int yPosition = Game.GraphicsDevice.Viewport.Height - 20 - activeTexture.Height;
                Position = new Vector2(xPosition, yPosition);
                animalState = AnimalState.WalkRight;
            }
            sourceRect = new Rectangle(activeTexture.Height * currentFrame, 0, tileSize, activeTexture.Height);
        }

        /// <summary>
        /// Get a random animal and sets its speed and health
        /// </summary>
        private void GetRandomAnimal()
        {
            int animalMax = Enum.GetNames(typeof(GetAnimalType)).Length;
            int randomAnimal = random.Next(0, animalMax);
            if (randomAnimal == 1)
            {
                tileCountWalk = 8;
                WalkSpeed = new Vector2(1.5f, 0);
                activeTexture = wolf;
                tileSize = 64;
                health = 1;
            }
            else if (randomAnimal == 2)
            {
                tileCountWalk = 8;
                WalkSpeed = new Vector2(1, 0);
                activeTexture = deer;
                tileSize = 72;
                health = 2;
            }
            else if (randomAnimal == 3)
            {
                tileCountWalk = 8;
                WalkSpeed = new Vector2(0.7f, 0);
                activeTexture = bear;
                tileSize = 64;
                health = 3;
            }
            else if (randomAnimal == 4)
            {
                tileCountWalk = 8;
                WalkSpeed = new Vector2(0.8f, 0);
                activeTexture = boar;
                tileSize = 64;
                health = 3;
            }
            else if (randomAnimal == 5)
            {
                tileCountWalk = 8;
                WalkSpeed = new Vector2(1.7f, 0);
                activeTexture = fox;
                tileSize = 64;
                health = 1;
            }
            else 
            {
                tileCountWalk = 10;
                WalkSpeed = new Vector2(2.5f, 0);
                activeTexture = rabbit;
                tileSize = 32;
                health = 0;
            }
        }
    }
}
