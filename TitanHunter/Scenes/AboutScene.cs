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
        MainGame mainGame;

        private Texture2D aboutBackgroundTexture;
        private SpriteFont textFont;
        private SpriteFont titleFont;
        private Vector2 backgroundPosition;
        private const string name = "Created By \nKimberly Rose Dela Cruz";
        private string aboutTitle = "About";
        private Color textColor = Color.White;
        public AboutScene(MainGame game) : base(game)
        {
            this.mainGame = game;
            backgroundPosition = new Vector2(0, 0);
            InitializeResources();
        }

        public void InitializeResources()
        {
            aboutBackgroundTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
            titleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            textFont = mainGame.Content.Load<SpriteFont>("fonts/medium");
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(aboutBackgroundTexture, backgroundPosition, textColor);
            mainGame._spriteBatch.DrawString(titleFont, aboutTitle, backgroundPosition, textColor);
            mainGame._spriteBatch.DrawString(textFont, name, new Vector2(Shared.stage.X / 3, Shared.stage.Y / 2), textColor);
            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
