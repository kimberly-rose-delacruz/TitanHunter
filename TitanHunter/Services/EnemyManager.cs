/*EnemyManager.cs
 *  this class is only for check if the game shall be able to throw meteors and spawning enemy within the field. this is also being validation if the game is already over or player won the game then it will not execute the latter continous spawning of enemies (x number set in the game Manager) while the meteors are being throwned indefinitely.
 *  
 *  Revision History:
 *      Created on December 3, 2022 By Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Models;
using TitanHunter.Services;
using SharpDX.Direct3D9;

namespace TitanHunter.Controllers
{
    public class EnemyManager : GameComponent
    {
        static Random rand = new Random();
        private MainGame mainGame;
        private GameManager gameLevelService;

        public EnemyManager(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
        }

        public override void Update(GameTime gameTime)
        {
            //all update will stop when player is dead or won.
            if (mainGame.gameLevelService.IsGameOver() || mainGame.gameLevelService.IsGameWon() == true) { return; }

            //if the Is throwing Meteor is true then it will create object of meteor projectile within the field
            if (gameLevelService.ThrowMeteors(gameTime) == true)
            {
                Projectile.projectiles.Add(new MeteorProjectile(mainGame, new Vector2(Shared.stage.X, rand.Next(Shared.SHARED_HEIGHT, (int)Shared.stage.Y))));

            }

            //if spawning enemy is true then spawn it in the field accordingly.
            if(gameLevelService.SpawnEnemy(gameTime) == true)
            {
                Enemy.enemies.Add(new Enemy(mainGame, new Vector2(rand.Next((int)(Shared.stage.X * 0.6f), (int)(Shared.stage.X * 0.9f)), rand.Next(Shared.SHARED_HEIGHT, (int)Shared.stage.Y))));
            }

        }
    }
}
