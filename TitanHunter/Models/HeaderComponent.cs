/*HeaderComponent.cs
 *  This represents the header section within the action scene where scores and messages will be displayed based on the outcome of player's gameplay
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
using SharpDX.XInput;
using TitanHunter.Services;

namespace TitanHunter.Models
{
    public class HeaderComponent : DrawableGameComponent
    {
        //declaring necessary global variables to be access within this header components.
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

        //this header component is compose of player and gamelevelservice where I am controlling the outcome of the game to show it to the player.
        public HeaderComponent(MainGame game, Player player) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
            this.player = player;
            
            InitializeResources();
        }

        //initializing all resources all together including the fonts and image of the score board as a background.
        public void InitializeResources()
        {
            mediumFont = mainGame.Content.Load<SpriteFont>("fonts/mediumBold");
            smallFont = mainGame.Content.Load<SpriteFont>("fonts/small");
            scoreboardTexture = mainGame.Content.Load<Texture2D>("images/scoreboard");


        }

        //this is to update the headercomponent if the player won it will end the game.
        public override void Update(GameTime gameTime)
        {
            if(gameLevelService.IsGameWon() == true)
            {
                //this method will let the game end by knowing the total enemy killed within the field which is the enemies (Titan) and it will also play the game end sound effect
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

            //messages in the header component differs each time there is an outcome. 
            //this first condition is to let the header component know if the game is over.
            if (gameLevelService.IsGameOver() == true)
            {
                //if it's true it will draw this string in the headercomponent saying it is game over
                mainGame._spriteBatch.DrawString(mediumFont, gameOverText, new Vector2(Shared.stage.X / 2 - gameOverPosition.X / 2, 0), Color.White);

                if (gameLevelService.HasNewHighScore == true)
                {
                    //if the user gain a new highscore it will be showing that he gain the high score and be able to check if pressing enter will be redirected to the high score page.
                    mainGame._spriteBatch.DrawString(mediumFont, goToHighScorePageText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
                else
                {
                    //else he will just be instructed back to home page where startscene to show list of menus.
                    mainGame._spriteBatch.DrawString(mediumFont, returnToHomeText, new Vector2(Shared.stage.X / 2 - returnToHomePosition.X / 2, HEADER_HEIGHT / 2), Color.White);
                }
            }

            //this condition is the player won the game is true
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

            //in the left corner of the whole header component this is only showing the updated score of the player when killing the titans and destroying meteors.
            mainGame._spriteBatch.DrawString(smallFont, totalScoreText  + totalScore.ToString(), new Vector2(GAP_SPACE * 2, GAP_SPACE), Color.White);

            mainGame._spriteBatch.DrawString(smallFont, killedTitanScoreText + gameLevelService.TotalEnemyKilled.ToString() + "/" + GameManager.TOTAL_ENEMY_COUNT, new Vector2(GAP_SPACE * 2, totalScorePosition.Y/2 + totalScorePosition.Y), Color.White);

            mainGame._spriteBatch.DrawString(smallFont, destroyedMeteorScoreText + gameLevelService.TotalDestroyedMeteor.ToString(), new Vector2(GAP_SPACE * 2, killedTitanScorePosition.Y + totalScorePosition.Y + GAP_SPACE), Color.White);

            mainGame._spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
