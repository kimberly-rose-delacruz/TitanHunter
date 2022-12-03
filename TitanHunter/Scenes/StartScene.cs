using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Color regularColor = Color.Red;

        public StartScene(MainGame game) : base(game)
        {
            this.mainGame = game;
            SpriteFont regular = mainGame.Content.Load<SpriteFont>("fonts/regular");
            SpriteFont highlight = mainGame.Content.Load<SpriteFont>("fonts/highlight");
            titleFont= mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            backgroundTex = mainGame.Content.Load<Texture2D>("images/HomeBackground");
            Menu = new MenuComponent(game, spriteBatch, regular, highlight, menuItems);
            components.Add(Menu);
            backgroundPosition = new Vector2(0, 0);
            gameTitlePosition = new Vector2(Shared.stage.X/3, Shared.stage.Y/3);
        }


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
