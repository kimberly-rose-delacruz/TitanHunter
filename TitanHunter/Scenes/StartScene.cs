using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitanHunter.Scenes
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }

        private SpriteBatch spriteBatch;

        MainGame mainGame;

        string[] menuItems = { "Begin", "Help", "High Score", "About", "Exit" };
        Texture2D backgroundTex;
        Vector2 position;

        public StartScene(Game game) : base(game)
        {
            mainGame = (MainGame)game;
            spriteBatch = mainGame._spriteBatch;
            SpriteFont regular = mainGame.Content.Load<SpriteFont>("fonts/regular");
            SpriteFont highlight = mainGame.Content.Load<SpriteFont>("fonts/highlight");
            Menu = new MenuComponent(game, spriteBatch, regular, highlight, menuItems);
            components.Add(Menu);

            backgroundTex = mainGame.Content.Load<Texture2D>("images/HomeBackground");
            position = new Vector2(0, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, position, Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
