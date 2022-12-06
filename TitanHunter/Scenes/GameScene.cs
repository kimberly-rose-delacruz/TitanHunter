/*GameScene.cs 
 *  The purpose of this class is to represent the showing and hiding of the Scenes based on added components that will be used by other classs using this GameScene class.
 *  
 *  Revision History:
 *      Created on December 6, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitanHunter.Scenes
{
    public class GameScene : DrawableGameComponent
    {
        public List<GameComponent> components { get; set; }

        //method used to show and enable.
        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
        }

        //method used to hide and disable.
        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
        }


        //initialize the list of components based on Game Components
        protected GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            Hide();
        }

        public override void Update(GameTime gameTime)
        {
            //if one of the scene is enabled which is one of the components that has been added in the game. it will update that item to run the game.
            //i.e. the actionScene does have the player, collisionMAnager, projectile and header components. for each of these items it will update based on its features, functions and rules based on their class attributes.
            foreach (GameComponent item in components)
            {
                
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        
        public override void Draw(GameTime gameTime)
        {
            //after each update of the item, it will draw now accordingly and make it visible in the scene.
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }

                }
            }

            base.Draw(gameTime);
        }
    }
}
