/*MenuComponent.cs
 *  This represents the the drawing of menu component in the startscene to show the available options for the user of the game where to go and show information regarding the game.
 *  
 *  Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitanHunter
{
    public class MenuComponent:DrawableGameComponent
    {
        //global variables
        private SpriteFont regularFont, highlightFont;
        private List<string> menuItems;
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color highlightColor = Color.Gray;
        private KeyboardState oldState;
        MainGame mainGame;

        public int selectedIndex { get; set; }

        public MenuComponent(MainGame game,
            SpriteFont regularFont,
            SpriteFont highlightFont,
            string[] menus) : base(game)
        {
            this.mainGame = game;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = menus.ToList<string>();
            //inserting the position of the menu list in the right lower corner of the stage.
            //i calculated using the stage width by getting the 75% of it and 60% of the height for the position.
            position = new Vector2(Shared.stage.X * 0.75f, Shared.stage.Y*0.60f);
        }

        //when user tries to navigate using Keys Up and down, it will navigate the user by choosing the menu from the list
        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if(selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
                
            }
            if(keyboardState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if(selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldState = keyboardState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 temporaryPosition = position;

            mainGame._spriteBatch.Begin();

            //this is to draw all the menu items in the background.
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    // mainGame._spriteBatch.DrawString(highlightFont, GetMenuString(i)
                    mainGame._spriteBatch.DrawString(highlightFont, GetMenuString(i),
                        temporaryPosition, highlightColor);
                    temporaryPosition.Y += highlightFont.LineSpacing;
                }
                else
                {
                    mainGame._spriteBatch.DrawString(regularFont, GetMenuString(i),
                        temporaryPosition, regularColor);
                    temporaryPosition.Y += regularFont.LineSpacing;
                }
            }
            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }

        //I modify the menu string where it will just change the begin text in the start scene menu to show whether if the player attempts to escape from an ongoing game.
        private string GetMenuString(int index)
        {
            if(index == 0)
            {
                //this will be executed and return the string continue to the draw method to give the Continue string to show in the menu list in start scene when game has start is true and gameOver is false and gameIsWon is false.
                if(mainGame.gameLevelService.HasGameStarted() == true && mainGame.gameLevelService.IsGameOver() == false && mainGame.gameLevelService.IsGameWon() == false)
                {
                    return "Continue";
                }
                else
                {
                    //else return the begin string as it was like before.
                    return menuItems[index];
                }
            }
            else
            {
                //for other indexes just retain it as it is.
                return menuItems[index];
            }
        }
    }
}
