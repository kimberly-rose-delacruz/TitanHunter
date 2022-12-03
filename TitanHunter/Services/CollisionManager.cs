using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Models;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Services
{
    public class CollisionManager : GameComponent
    {

        private Player player;
        public CollisionManager(Game game, Player player) : base(game)
        {
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            var playerShurikens = Projectile.projectiles.Where(p => p is PlayerProjectile).ToList();
            var enemies = Enemy.enemies.ToList();

            foreach (var projectile in playerShurikens)
            {
                var meteors = Projectile.projectiles.Where(m => m is MeteorProjectile).ToList();

                for (int i = 0; i < meteors.Count; i++)
                {
                    var currentMeteor = meteors[i];

                    if (projectile.getBounds().Intersects(currentMeteor.getBounds()))
                    {
                        currentMeteor.Destroy();
                        Projectile.projectiles.Remove(currentMeteor);
                        Projectile.projectiles.Remove(projectile);
                    }
                   
                }


                for (int i = 0; i < enemies.Count; i++)
                {
                    var currentEnemy = enemies[i];
                    if(projectile.getBounds().Intersects(currentEnemy.getEnemyBounds()))
                    {
                        currentEnemy.PlayCollisionSoundEffect();
                        currentEnemy.Kill();
                        Enemy.enemies.Remove(currentEnemy);
                        Projectile.projectiles.Remove(projectile);

                    }
                }

               
            }

            //killing player via collision
            var enemyProjectile = Projectile.projectiles.Where(p => p is EnemyProjectile || p is MeteorProjectile).ToList();
            for (int i = 0; i < enemyProjectile.Count; i++)
            {
                var currentEnemy = enemyProjectile[i];
                var playerBounds = player.getBounds();
                var currentEnemyBounds = currentEnemy.getBounds();


                if(playerBounds.Intersects(currentEnemyBounds))
                {
                    Rectangle.Intersect(ref playerBounds, ref currentEnemyBounds, out Rectangle hitPoint);
                    player.Kill(hitPoint);
                    
                    //stop all the objects within the field. 
                    //gameOverScene show
                    //clicking escape or enter to restart the game.
                    //add the score in the highscore list.
                }
            }

            base.Update(gameTime);
        }
    }
}
