using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Scenes
{
    public class HelpScene : GameScene
    {
        private MainGame mainGame;
        private Texture2D helpSceneTexture;
        private Vector2 position;
        private const int X_POSITION = 0;
        private const int Y_POSITION = 0;

        public HelpScene(MainGame game) : base(game)
        {
            mainGame = game;
            position = new Vector2(X_POSITION, Y_POSITION);
            InitializeTexture();
        }

        public void InitializeTexture()
        {
            helpSceneTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
           
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(helpSceneTexture, position, Color.White);
            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
