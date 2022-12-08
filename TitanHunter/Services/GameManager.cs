/*GameManager.cs
 *      This is the class that serves all the logic behind the Game, it reports the status of each of game objective and resetting the game whether it has ended or player wins. Game service also handles the adding of score when player hits the meteor and more of services that may reports to other scenes that can manipulat what to show and what to update.
 *      
 *  Revision History:
 *      Updated on December 6, 2022 by Kimberly Rose Dela Cruz
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitanHunter.Models;

namespace TitanHunter.Services
{
    public class GameManager
    {

        private double meteorTimer = 1.5D;
        private double meteorResetTimeValue = 1.5D;
        private double enemyTimer = 2D;
        private double enemyResetTimeValue = 2D;
        //only 5 titans will be spawned in the field to finish the game
        public const int TOTAL_ENEMY_COUNT = 5;
        public int currentTotalEnemyCount = TOTAL_ENEMY_COUNT;
        private bool isGameOver = false;
        private bool isGameReset = false;
        private bool isNewScoreAdded = false;
        private bool isGameStarted = false;
        public int TotalEnemyKilled { get; private set; }
        public int TotalDestroyedMeteor { get; private set; }

        public bool HasNewHighScore { get; private set; }

        public List<Score> scores = new List<Score>();
        
        //game resetting
        public void Reset()
        {
            //resetting the game when the game is over.
            isGameReset = true;
            isGameOver = false;
            currentTotalEnemyCount = TOTAL_ENEMY_COUNT;
            TotalEnemyKilled = 0;
            TotalDestroyedMeteor = 0;
            isNewScoreAdded = false;
            meteorResetTimeValue = 1.5D;
            HasNewHighScore = false;

        }

        //function to know if the game has been reset.
        public bool IsGameReset()
        {
            if(isGameReset == true)
            {
                //set again to false to restart the game again.
                isGameReset = false;
                return true;
                
            }

            return isGameReset;
        }

        //function to know if the game is over.
        public bool IsGameOver()
        {          
            return isGameOver;
        }

        //functio to know if the player has won based on the totalEnemyKilled equals to the total enemy count of the titans which is only total of 5 
        public bool IsGameWon()
        {
            if(TotalEnemyKilled == TOTAL_ENEMY_COUNT)
            {
               if(isNewScoreAdded == false)
                {
                    //if new score is added to false then add a new score.
                    AddScore();
                }

                return true;
            }

            return false;
        }

        //this method will add a score based on players's hit count
        public void AddScore()
        {

            int playerNoScore = 0;
            DateTime gamePlayTime = DateTime.Now;
            var newScore = new Score();
            newScore.PlayTime = gamePlayTime;
            newScore.PlayerTotalScore = TotalEnemyKilled + TotalDestroyedMeteor;

            //getting current player high score from the list of scores by using Max function.
             var currentHighScore = scores.Count== 0 ? playerNoScore : 
                scores.Max(s => s.PlayerTotalScore);

            //if the player's total score is greater than the current high score.
            if(newScore.PlayerTotalScore > currentHighScore)
            {
                //access this boolean in the gamescenes if there is a new high score.
                HasNewHighScore = true;
            }

            //if player total score is not equal to 0 (such as it hits enemy or the meteor it will add a score.
            if(newScore.PlayerTotalScore != 0)
            {
                scores.Add(newScore);
                //report the status that a new score is added.
                isNewScoreAdded = true;
            }

        }

        //If player Is Dead then add score and set isGameOver to true.
        public void PlayerIsDead()
        {
             isGameOver = true;

            if (isNewScoreAdded == false)
            {
                AddScore();
            }
        }
         
        //this is a function to know when to throw meteors dynamically based on the timer.
        public bool ThrowMeteors(GameTime gameTime)
        {
            meteorTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if(meteorTimer <= 0)
            {
                meteorTimer = meteorResetTimeValue;
                
                //this serves that not killing all titans within the field will make the meteor throw even more fast by resetting
                if(meteorResetTimeValue > 0.5D)
                {
                    meteorResetTimeValue -= 0.1D;
                }       

                return true;
            }

            return false;
        }

        //spawning of enemy continously until the maximum number has nee decrement to 0 from 5 count.
        public bool SpawnEnemy(GameTime gameTime)
        {
            enemyTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (enemyTimer <= 0 && currentTotalEnemyCount > 0)
            {
                //spawning maximum of 5 enemies until it will finish the count.
                currentTotalEnemyCount--;
                 enemyTimer = enemyResetTimeValue;
                return true;
            }

            return false;
        }

        //method to know the total enemy killed for scoring
        public void IncrementKilledEnemy()
        {
            TotalEnemyKilled++;
        }

        //method to know the total destroyed meteor for scoring.
        public void IncrementDestroyedMeteor()
        {
            TotalDestroyedMeteor++;
        }

        //method to let the service that the game has started.
        public void StartGame()
        {
            isGameStarted = true;
        }

        //function to know if the game has started to be used by menu component and change the begin to continue .
        public bool HasGameStarted()
        {
            return isGameStarted;
        }
    }
}
