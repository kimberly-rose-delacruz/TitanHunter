﻿using Microsoft.Xna.Framework;
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
        public const int TOTAL_ENEMY_COUNT = 5;
        public int currentTotalEnemyCount = TOTAL_ENEMY_COUNT;
        private bool isGameOver = false;
        private bool isGameReset = false;
        private bool isNewScoreAdded = false;
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

        public bool IsGameOver()
        {          
            return isGameOver;
        }

        public bool IsGameWon()
        {
            if(TotalEnemyKilled == TOTAL_ENEMY_COUNT)
            {
               if(isNewScoreAdded == false)
                {
                    AddScore();
                }

                return true;
            }

            return false;
        }

        
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


            if(newScore.PlayerTotalScore > currentHighScore)
            {
                //access this boolean in the gamescenes if there is a new high score.
                HasNewHighScore = true;
            }

            if(newScore.PlayerTotalScore != 0)
            {
                scores.Add(newScore);
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

        public bool SpawnEnemy(GameTime gameTime)
        {
            enemyTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (enemyTimer <= 0 && currentTotalEnemyCount > 0)
            {
                currentTotalEnemyCount--;
                 enemyTimer = enemyResetTimeValue;
                return true;
            }

            return false;
        }

        public void IncrementKilledEnemy()
        {
            TotalEnemyKilled++;
        }

        public void IncrementDestroyedMeteor()
        {
            TotalDestroyedMeteor++;
        }
    }
}