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

        public double meteorTimer = 1.5D;
        public double meteorResetTimeValue = 1.5D;
        public double enemyTimer = 2D;
        public double enemyResetTimeValue = 2D;
        public int totalEnemyCount = 5;
        public bool isGameOver = false;

        public void Reset()
        {

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
