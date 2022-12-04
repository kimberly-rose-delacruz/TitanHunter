using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Scenes
{
    public class HighScoreScene : GameScene
    {
        private MainGame mainGame;
        
        public HighScoreScene(MainGame game) : base(game)
        {
            this.mainGame = game;
        }

        public void InitializeResources()
        {

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
