using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllInOneMono.Drawable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalProjectShell
{
    public abstract class GameScene : GameComponent
    {


        const int CLEANUP_INTERVAL = 1;
        double cleanupTimer = 0.0;

        /// <summary>
        /// Used to hold a reference to the components that belong to 
        /// this GameScene instance.  Used to quickly iterate through components 
        /// that belong to the scence to enable and make visible where applicable
        /// </summary>
        List<GameComponent> SceneComponents { get; set; }

        public GameScene(Game game) : base(game)
        {

            SceneComponents = new List<GameComponent>();

            Hide();
        }

        public override void Update(GameTime gameTime)
        {
            cleanupTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (cleanupTimer >= CLEANUP_INTERVAL)
            {
                cleanupTimer = 0.0;
                for (int i = 0; i < SceneComponents.Count; i++)
                {
                    if (Game.Components.Contains(SceneComponents[i]) == false)
                    {
                        SceneComponents.Remove(SceneComponents[i]);
                    }
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Shows this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to true
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Show()
        {

            this.Enabled = true;
            // iterate though all components held by this scene 
            // and set Enabled to true and if it's also a DrawableGameComponent
            // set Visible to true
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        /// <summary>
        /// Hides this instance of game scene and all of its
        /// components.  Sets Enabled and Visible to false
        /// for all of the components that belong to this 
        /// scene
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            // iterate though all components held by this scene 
            // and set Enabled to false and if it's also a DrawableGameComponent
            // set Visible to false
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }

        /// <summary>
        /// As a final step of initialization of the GameScene
        /// we will iterate through all the items that we added to the scene 
        /// and add them to the game components collection
        /// </summary>
        public override void Initialize()
        {
            // iterate through the list of scene components and
            // add them to the game component for the framework to manage
            foreach (GameComponent component in SceneComponents)
            {
                if (Game.Components.Contains(component) == false)
                {
                    Game.Components.Add(component);
                }

            }

            base.Initialize();
        }

        /// <summary>
        /// Add item to Scene components and 
        /// to Game Components
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(GameComponent component)
        {
            this.SceneComponents.Add(component);
            //Game.Components.Add(component);
        }


        /// <summary>
        /// Counts how many components of a given type
        /// we are holding in this scene
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetComponentCount(Type type)
        {
            int count = 0;
            foreach (GameComponent component in SceneComponents)
            {

                if (component.GetType() == type)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
