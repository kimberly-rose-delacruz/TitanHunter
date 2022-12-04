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
        private SpriteFont regularFont, highlightFont;
        private List<string> menuItems;
        private Vector2 position;
        private Color regularColor = Color.White;
        private Color highlightColor = Color.Gray;
        private KeyboardState oldState;
        MainGame mainGame;

        public int selectedIndex { get; set; }

        public MenuComponent(MainGame game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont highlightFont,
            string[] menus) : base(game)
        {
            this.mainGame = game;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = menus.ToList<string>();
            position = new Vector2(Shared.stage.X / 2 -40, Shared.stage.Y/2);
        }

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
                    mainGame._spriteBatch.DrawString(highlightFont, menuItems[i],
                        temporaryPosition, highlightColor);
                    temporaryPosition.Y += highlightFont.LineSpacing;
                }
                else
                {
                    mainGame._spriteBatch.DrawString(regularFont, menuItems[i],
                        temporaryPosition, regularColor);
                    temporaryPosition.Y += regularFont.LineSpacing;
                }
            }
            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
