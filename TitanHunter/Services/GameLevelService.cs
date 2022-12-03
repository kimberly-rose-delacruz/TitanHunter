using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Services
{
    public class GameLevelService
    {

        private double meteorTimer = 1.5D;
        private double meteorResetTimeValue = 1.5D;
        private double enemyTimer = 2D;
        private double enemyResetTimeValue = 2D;
        private int totalEnemyCount = 5;
        private bool isGameOver = false;
        private bool isGameReset = false;

        //game resetting
        public void Reset()
        {
            //resetting the game when the game is over.
            isGameReset = true;
            isGameOver = false;
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

        public void PlayerIsDead()
        {
            isGameOver = true;
        }

        public int GetCurrentGameLevel()
        {

            return 1;
        }

        public bool ThrowMeteors(GameTime gameTime)
        {
            meteorTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if(meteorTimer <= 0)
            {
                meteorTimer = meteorResetTimeValue;
                return true;
            }

            return false;
        }

        public bool SpawnEnemy(GameTime gameTime)
        {
            enemyTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (enemyTimer <= 0 && totalEnemyCount >0 )
            {
                totalEnemyCount--;
                 enemyTimer = enemyResetTimeValue;
                return true;
            }

            return false;
        }


    }
}
