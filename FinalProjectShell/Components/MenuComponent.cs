using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public class MenuComponent : DrawableGameComponent
    {
        
        SpriteFont regularFont;
        SpriteFont highlightFont;

        private List<string> menuItems;
        private int SelectedIndex { get; set; }
        private Vector2 position;

        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Green;

        private KeyboardState oldState;

        public MenuComponent(Game game, List<string>menuNames) : base(game)
        {
            
            menuItems = menuNames; 
            

        }

        public override void Update(GameTime gameTime)
        {
            {               
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
                {
                    SelectedIndex++;
                    if (SelectedIndex == menuItems.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = menuItems.Count - 1;
                    }

                }
                oldState = ks;

                if (ks.IsKeyDown(Keys.Enter))
                {
                    SwitchScenesBasedOnSelection();
                }

            }
            base.Update(gameTime);
        }
        /// <summary>
        /// All the selction for the menu
        /// </summary>
        private void SwitchScenesBasedOnSelection()
        {
            ((Game1)Game).HideAllScenes();

            switch ((MenuSelection)SelectedIndex)
            {
                case MenuSelection.StartGame:
                    Game.Services.GetService<ActionScene>().Show();
                    break;
                case MenuSelection.Help:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case MenuSelection.Quit:
                    Game.Exit();
                    break;
                case MenuSelection.HighScore:
                    Game.Services.GetService<HighScore>().Show();
                    break;
                case MenuSelection.Credit:
                    Game.Services.GetService<CreditScene>().Show();
                    break;
                default:
                    // for now there is nothing handling the other options
                    // we will simply show this screen again
                    Game.Services.GetService<StartScene>().Show();
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            Vector2 tempPos = position;

            sb.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                SpriteFont activeFont = regularFont;
                Color activeColor = regularColor;

                // if the selection is the item we are drawing
                // made it a the special font and colour
                if (SelectedIndex == i)
                {
                    activeFont = highlightFont;
                    activeColor = hilightColor;                    
                }
                
                sb.DrawString(activeFont, menuItems[i], tempPos, activeColor);

                // update the position of next string
                tempPos.Y += regularFont.LineSpacing;                
            }

            sb.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            // starting position of the menu items
            position = new Vector2(GraphicsDevice.Viewport.Width / 3,
                                      GraphicsDevice.Viewport.Height /4);
               
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load the fonts we will be using for this menu
            regularFont = Game.Content.Load<SpriteFont>("Fonts/regularFont");
            highlightFont = Game.Content.Load<SpriteFont>("Fonts/hilightFont");
            base.LoadContent();
        }
    }
}
