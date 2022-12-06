using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.XInput;
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
        private string gameWonNewHighScoreText = "NEW HIGH SCORE!";
        private string goToHighScorePageText = "Press [ENTER] to see high score.";

        private MainGame mainGame;
        private GameManager gameLevelService;
        private Player player;

        SpriteFont mediumFont;
        SpriteFont smallFont;

        Texture2D scoreboardTexture;
        Vector2 gameOverPosition;
        Vector2 gameWonPosition;
        Vector2 returnToHomePosition;
        Vector2 totalScorePosition;
        Vector2 killedTitanScorePosition;
        Vector2 gameWonNewHighScorePosition;

        public HeaderComponent(MainGame game, Player player) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
            this.player = player;
            
            InitializeResources();
        }

        public void InitializeResources()
        {
            mediumFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");
            smallFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            scoreboardTexture = mainGame.Content.Load<Texture2D>("images/scoreboard");


        }

        public override void Update(GameTime gameTime)
        {
            if(gameLevelService.IsGameWon() == true)
            {
                player.GameEnd();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //get the measurement of string
            gameOverPosition = mediumFont.MeasureString(gameOverText);
            gameWonPosition = mediumFont.MeasureString(gameWonText);
            gameWonNewHighScorePosition = mediumFont.MeasureString(gameWonNewHighScoreText);
            returnToHomePosition = mediumFont.MeasureString(returnToHomeText);
            totalScorePosition = smallFont.MeasureString(totalScoreText);
            killedTitanScorePosition = smallFont.MeasureString(killedTitanScoreText);
            int totalScore = gameLevelService.TotalEnemyKilled + gameLevelService.TotalDestroyedMeteor;


            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(scoreboardTexture, new Vector2(0, 0), Color.White);

            if (gameLevelService.IsGameOver() == true)
            {
                //draw gameover here.

                mainGame._spriteBatch.DrawString(mediumFont, gameOverText, new Vector2(Shared.stage.X / 2 - gameOverPosition.X / 2, 0), Color.White);

                if (gameLevelService.HasNewHighScore == true)
                {
                    mainGame._spriteBatch.DrawString(mediumFont, goToHighScorePageText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
                else
                {
                    mainGame._spriteBatch.DrawString(mediumFont, returnToHomeText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
            }


            if (gameLevelService.IsGameWon() == true)
            {
                //if the player wins it will displayed the congratulations status based if it's a new high score or not.
                if(gameLevelService.HasNewHighScore == true)
                {
                    mainGame._spriteBatch.DrawString(mediumFont, gameWonNewHighScoreText, new Vector2(Shared.stage.X / 2 - gameWonNewHighScorePosition.X / 2, 0), Color.White);

                    mainGame._spriteBatch.DrawString(mediumFont, goToHighScorePageText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
                else
                {
                    mainGame._spriteBatch.DrawString(mediumFont, gameWonText, new Vector2(Shared.stage.X / 2 - gameWonPosition.X / 2, 0), Color.White);
                    mainGame._spriteBatch.DrawString(mediumFont, returnToHomeText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
            }

            mainGame._spriteBatch.DrawString(smallFont, totalScoreText  + totalScore.ToString(), new Vector2(GAP_SPACE * 2, GAP_SPACE), Color.White);

            mainGame._spriteBatch.DrawString(smallFont, killedTitanScoreText + gameLevelService.TotalEnemyKilled.ToString() + "/" + GameManager.TOTAL_ENEMY_COUNT, new Vector2(GAP_SPACE * 2, totalScorePosition.Y/2 + totalScorePosition.Y), Color.White);

            mainGame._spriteBatch.DrawString(smallFont, destroyedMeteorScoreText + gameLevelService.TotalDestroyedMeteor.ToString(), new Vector2(GAP_SPACE * 2, killedTitanScorePosition.Y + totalScorePosition.Y + GAP_SPACE), Color.White);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
