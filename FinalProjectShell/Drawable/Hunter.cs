using FinalProject;
using HunterExtreme.Drawable;
using HunterExtreme.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AllInOneMono
{
    enum HunterState
    {
        WalkRight,
        WalkLeft,
        ShootRight,
        ShootLeft
    }
    class Hunter : DrawableGameComponent
    {
        Texture2D hunter;
        Vector2 hunterPosition;

        public Rectangle HunterBounds
        {
            get
            {
                Rectangle bounds = sourceRect;
                bounds.Location = hunterPosition.ToPoint();
                return bounds;
            }
        }

        bool flip = false;
        bool hasJumped = false;
        
        //Vector2 Jump = new Vector2(0, 10);
        Vector2 Speed = new Vector2(2, 0);
        public Rectangle sourceRect;
        
        HunterState hunterState = new HunterState();

        double frameTimer = 0.0;
        double jumpTimer = 0.0;
        int currentFrame = 0;

        const int TILE_SIZE = 55;
        const int TILE_COUNT_WALK = 3;
        const double FRAME_DURATION = 0.1;

        public Hunter(Game game) : base(game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(hunter,
                    hunterPosition,
                    sourceRect,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    flip ?
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
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                flip = false;
                
                if (frameTimer >= FRAME_DURATION)
                {
                    frameTimer = 0;
                    if (TILE_COUNT_WALK <= ++currentFrame)
                    {
                        currentFrame = 0;
                    }
                    sourceRect.X = TILE_SIZE * currentFrame;
                }
                hunterPosition += Speed;
                hunterState = HunterState.WalkRight;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                flip = true;
                
                if (frameTimer >= FRAME_DURATION)
                {
                    frameTimer = 0;
                    if (TILE_COUNT_WALK <= ++currentFrame)
                    {
                        currentFrame = 0;
                    }
                    sourceRect.X = TILE_SIZE * currentFrame;
                }
                hunterPosition -= Speed;
                hunterState = HunterState.WalkLeft;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up)&& hasJumped==false)
            {
                hunterPosition.Y -= 45f;
                hasJumped = true;
            }
            if (hasJumped == true)
            {
                jumpTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (jumpTimer > 0.2)
                {
                    hunterPosition.Y += 45f;
                    jumpTimer = 0.0;
                    hasJumped = false;
                }
            }
            hunterPosition.X = MathHelper.Clamp(hunterPosition.X, 0, GraphicsDevice.Viewport.Width- sourceRect.Width);
            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            hunter = Game.Content.Load<Texture2D>("geb1");
            currentFrame = 0;
            int xPosition = Game.GraphicsDevice.Viewport.Width / 2;
            int yPosition = Game.GraphicsDevice.Viewport.Height - 10 - hunter.Height;
            hunterPosition = new Vector2(xPosition, yPosition);

            sourceRect = new Rectangle(hunter.Height * currentFrame, 0, TILE_SIZE, hunter.Height);

            base.LoadContent();
        }
    }
}
