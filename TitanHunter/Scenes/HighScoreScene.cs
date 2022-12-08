/*HighScoreScene.cs
 *      This is a class that will draw all the score updates based from player's gameplay.
 *      
 *  Revision History
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanHunter.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TitanHunter.Scenes
{
    public class HighScoreScene : GameScene
    {
        //declaring global variables.
        private MainGame mainGame;
        private List<Score> highScores;
        private const string TABLE_HEADER_TITLE = "Rank         Score          Game Time";
        private const int GAP_SPACE = 40;
        private const int X_POSITION = 0;
        private const int Y_POSITION = 0;
        private const int ADJUSTEDTEXT_X_POSITION = 20;
        private string highScoreTitle = "High Score";
        private const int NUMBER_OF_RANK = 5;
        private const int PADDING_WIDTH = 5;

        Texture2D highScoreBackgroundTexture;
        Texture2D rankingThropyTexture;
        Vector2 fontPosition;
        Vector2 highScoreTitlePosition;

        SpriteFont regularFont;
        SpriteFont highScoreTitleFont;
        private Color textColor = Color.White;


        public HighScoreScene(MainGame game) : base(game)
        {
            this.mainGame = game;
            InitializeResources();
            fontPosition = new Vector2(GAP_SPACE, Shared.stage.Y/3);
            highScoreTitlePosition = new Vector2(ADJUSTEDTEXT_X_POSITION, Y_POSITION);
            highScoreTitlePosition = new Vector2(ADJUSTEDTEXT_X_POSITION, Y_POSITION);

        }

        public void InitializeResources()
        {
            highScoreBackgroundTexture = mainGame.Content.Load<Texture2D>("images/HelpBackground");
            regularFont = mainGame.Content.Load<SpriteFont>("fonts/medium");
            highScoreTitleFont = mainGame.Content.Load<SpriteFont>("fonts/GameTitleFont");
            rankingThropyTexture = mainGame.Content.Load<Texture2D>("images/throphy");
        }

        //I use the show method from the Gamescene to set the list of highscores based from the service where there is the method to add score based on player's game outcome.
        public override void Show()
        {
            //in every time the user views the highscore scene, it will retrieve highest total player score.
            //in this game play I only get the max of 5 highest total score from the list.
            highScores = mainGame.gameLevelService.scores.OrderByDescending(s => s.PlayerTotalScore).Take(NUMBER_OF_RANK).ToList();
            base.Show();
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 temporaryPosition = fontPosition;


            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(highScoreBackgroundTexture, new Vector2(X_POSITION, Y_POSITION), textColor);

            mainGame._spriteBatch.Draw(rankingThropyTexture, new Vector2(Shared.stage.X*0.60f, Shared.stage.Y*0.40f), textColor);
            mainGame._spriteBatch.DrawString(highScoreTitleFont, highScoreTitle, highScoreTitlePosition, textColor);
            mainGame._spriteBatch.DrawString(regularFont, TABLE_HEADER_TITLE, new Vector2(GAP_SPACE, Shared.stage.Y/3), Color.Gold);

            //this is to draw each of the string in the highscore scene page according to the list of scores updated from highScore list of values.
            for (int i = 0; i < highScores.Count; i++)
            {
                var score = highScores[i];
                //I use the PadLeft to create an equal padding to show the score with equal sizes vertically.
                var totalScore = score.PlayerTotalScore.ToString().PadLeft(PADDING_WIDTH, '0');

                mainGame._spriteBatch.DrawString(regularFont, $"     {i + 1}           {totalScore}           {score.PlayTime.ToLongTimeString()}",new Vector2(GAP_SPACE, temporaryPosition.Y + regularFont.LineSpacing), textColor);
                temporaryPosition.Y+= regularFont.LineSpacing;

            }
            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
