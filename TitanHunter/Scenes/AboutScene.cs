/*AboutScene.cs
 *      This class will represent the creator's name.
 *      
 * Revision History:
 *      Created on December 6, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanHunter.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter;

namespace MobsHunterGame.Scenes
{
    public class AboutScene : GameScene
    {
        //declaring global variables.
        private MainGame mainGame;
        private const string NAME = "Created By \nKimberly Rose Dela Cruz";
        private Texture2D aboutBackgroundTexture;
        private SpriteFont textFont;
        private SpriteFont titleFont;
        private Vector2 backgroundPosition;
        private string aboutTitle = "About";
        private Color textColor = Color.White;

        //initialize the position, and use MainGame to manage all the components within this scene.
        public AboutScene(MainGame game) : base(game)
        {
            this.mainGame = game;
            backgroundPosition = new Vector2(0, 0);
            //initialize resources from contents uploaded in the content.mgcb.
            InitializeResources();
        }

        //this method will load all uploaded images fonts in the resources for about scene. composition of backgroun texture, titleFont, and textFont.
        public void InitializeResources()
        {
            aboutBackgroundTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
            titleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            textFont = mainGame.Content.Load<SpriteFont>("fonts/medium");
        }

        //this will now actually draw the backgrountexture, with corresponding position and textColor.
        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(aboutBackgroundTexture, backgroundPosition, textColor);
            //this includes the title of the scene writen after the background
            mainGame._spriteBatch.DrawString(titleFont, aboutTitle, backgroundPosition, textColor);
            //then draw the string of full name as the creator.
            mainGame._spriteBatch.DrawString(textFont, NAME, new Vector2(Shared.stage.X / 3, Shared.stage.Y / 2), textColor);
            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
