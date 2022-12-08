/*StartScene.cs
 *      This class will represent the page of the start page where it will show the Game's title, list of menu for the player to use.
 *      
 *  Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Color = Microsoft.Xna.Framework.Color;

namespace TitanHunter.Scenes
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch spriteBatch;

        MainGame mainGame;

        private string[] menuItems = { "Begin", "Help", "High Score", "About", "Exit" };
        private string gameTitle = "Titan Hunter";

        Texture2D backgroundTex;
        Vector2 backgroundPosition;
        Vector2 gameTitlePosition;
        SpriteFont titleFont;
        private Color regularColor = Color.White;
        SpriteFont regular;
        SpriteFont highlight;
        Song backgroundMusic;

        public StartScene(MainGame game) : base(game)
        {
            this.mainGame = game;
            InitializeResources();

            Menu = new MenuComponent(game, regular, highlight, menuItems);
            components.Add(Menu);
            backgroundPosition = new Vector2(0, 0);
            Vector2 titlePosition = titleFont.MeasureString(gameTitle);

            gameTitlePosition = new Vector2(Shared.stage.X/2 - titlePosition.X/2, 0);
            
        }

        //using the show method from the startscene and play the song of the game by using Media Player.
        public override void Show()
        {
            MediaPlayer.IsRepeating = true;
            //playing the background Music.
            MediaPlayer.Play(backgroundMusic);
            base.Show();
        }

        //when the startscene is hidden stop the mediaplayer from playing the backgroundMusic
        public override void Hide()
        {
            MediaPlayer.Stop();
            base.Hide();
        }

        //created this method to load all resources of the font texture and music for the game that will be used in the startscene.
        public void InitializeResources()
        {
            regular = mainGame.Content.Load<SpriteFont>("fonts/regular");
            highlight = mainGame.Content.Load<SpriteFont>("fonts/highlight");
            titleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            backgroundTex = mainGame.Content.Load<Texture2D>("images/HomeBackground");
            backgroundMusic = mainGame.Content.Load<Song>("sounds/TitanHunterMusic");

        }

        //draw the background texture and the title of the game.
        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(backgroundTex, backgroundPosition, Color.White);
            mainGame._spriteBatch.DrawString(titleFont, gameTitle, gameTitlePosition, regularColor);
            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
