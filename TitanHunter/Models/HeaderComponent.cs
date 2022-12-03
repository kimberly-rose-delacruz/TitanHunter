using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Services;

namespace TitanHunter.Models
{
    public class HeaderComponent : DrawableGameComponent
    {
        public const int HEADER_HEIGHT = 85;
        //private const int GAMEOVER_X_POSITION = 390;
        //private const int GAMEOVER_Y_POSITION = 35;
        Vector2 gameOverPosition;
        Vector2 returnToHomePosition;


        private const string gameOverText = "GAME OVER";
        private const string returnToHomeText = "Press [ENTER] to go back to menu.";
        private const string totalScoreText = "Total Score: ";


        private MainGame mainGame;
        private GameLevelService gameLevelService;

        SpriteFont gameOverFont;
        SpriteFont returnToHomeFont;

        public HeaderComponent(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
            
            InitializeResource();
        }

        public void InitializeResource()
        {
            gameOverFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");
            returnToHomeFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");

        }

        public override void Draw(GameTime gameTime)
        {
            if(gameLevelService.IsGameOver() == true)
            {
                //draw gameover here.
                mainGame._spriteBatch.Begin();
                gameOverPosition =  gameOverFont.MeasureString(gameOverText);
                returnToHomePosition = returnToHomeFont.MeasureString(returnToHomeText);

                mainGame._spriteBatch.DrawString(gameOverFont, gameOverText, new Vector2(Shared.stage.X/2 - gameOverPosition.X/2, 0), Color.White);

                mainGame._spriteBatch.DrawString(returnToHomeFont, returnToHomeText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT/2), Color.White);
                mainGame._spriteBatch.End();

            }

            base.Draw(gameTime);
        }
    }
}
