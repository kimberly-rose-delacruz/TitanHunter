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
        private const int GAP_SPACE = 10;
        private string gameOverText = "GAME OVER";
        private string returnToHomeText = "Press [ENTER] to go back to menu.";
        private string totalScoreText = "Total Score: ";
        private string killedTitanScoreText = "Titan Score: ";
        private string destroyedMeteorScoreText = "Meteor Score: ";
        private string gameWonText = "CONGRATULATIONS! YOU WON";
        private string goToHighScorePageText = "Press [ENTER] to see high score.";

        private MainGame mainGame;
        private GameLevelService gameLevelService;

        SpriteFont gameOverFont;
        SpriteFont gameWonFont;
        SpriteFont returnToHomeFont;
        SpriteFont totalScoreFont;
        SpriteFont killedTitanScoreFont;
        SpriteFont destroyedMeteorScoreFont;

        Texture2D scoreboardTexture;
        Vector2 gameOverPosition;
        Vector2 gameWonPosition;
        Vector2 returnToHomePosition;
        Vector2 totalScorePosition;
        Vector2 killedTitanScorePosition;
        Vector2 destroyedMeteorScorePosition;
        

        public HeaderComponent(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
            
            InitializeResources();
        }

        public void InitializeResources()
        {
            gameOverFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");
            returnToHomeFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");
            totalScoreFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            killedTitanScoreFont= mainGame.Content.Load<SpriteFont>("fonts/small");
            destroyedMeteorScoreFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            scoreboardTexture = mainGame.Content.Load<Texture2D>("images/scoreboard");
            gameWonFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");

        }



        public override void Draw(GameTime gameTime)
        {
            //get the measurement of string
            gameOverPosition = gameOverFont.MeasureString(gameOverText);
            returnToHomePosition = returnToHomeFont.MeasureString(returnToHomeText);
            totalScorePosition = totalScoreFont.MeasureString(totalScoreText);
            killedTitanScorePosition = killedTitanScoreFont.MeasureString(killedTitanScoreText);
            destroyedMeteorScorePosition = destroyedMeteorScoreFont.MeasureString(destroyedMeteorScoreText);
            int totalScore = gameLevelService.TotalEnemyKilled + gameLevelService.TotalDestroyedMeteor;
            gameWonPosition = gameWonFont.MeasureString(gameWonText);


            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(scoreboardTexture, new Vector2(0, 0), Color.White);

            if (gameLevelService.IsGameOver() == true)
            {
                //draw gameover here.

                mainGame._spriteBatch.DrawString(gameOverFont, gameOverText, new Vector2(Shared.stage.X / 2 - gameOverPosition.X / 2, 0), Color.White);

                mainGame._spriteBatch.DrawString(returnToHomeFont, returnToHomeText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
            }
            
            if(gameLevelService.IsGameWon() == true)
            {
                //draw gameWon here.
                mainGame._spriteBatch.DrawString(gameWonFont, gameWonText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);

            }

            mainGame._spriteBatch.DrawString(totalScoreFont, totalScoreText  + totalScore.ToString(), new Vector2(GAP_SPACE * 2, GAP_SPACE), Color.White);

            mainGame._spriteBatch.DrawString(killedTitanScoreFont, killedTitanScoreText + gameLevelService.TotalEnemyKilled.ToString() + "/" + GameLevelService.TOTAL_ENEMY_COUNT, new Vector2(GAP_SPACE * 2, totalScorePosition.Y/2 + totalScorePosition.Y), Color.White);

            mainGame._spriteBatch.DrawString(destroyedMeteorScoreFont, destroyedMeteorScoreText + gameLevelService.TotalDestroyedMeteor.ToString(), new Vector2(GAP_SPACE * 2, killedTitanScorePosition.Y + totalScorePosition.Y + GAP_SPACE), Color.White);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
