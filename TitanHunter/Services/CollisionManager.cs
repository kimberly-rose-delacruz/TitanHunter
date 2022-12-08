/*CollisionManager.cs
 *      this class is managing the logic when the player throws shuriken and it collides or hit the enemy and meteor. It will also handle the collision of the player towards the meteor and the fireball thrown by the enemies. The outcome of collision will also be accounted on having scores and letting the player be defeated if in case it will be collided by the objects (from enemies and meteors)
 *      
 *  REvision History
 *      created on December 3, 2022 by Kimberly Rose Dela Cruz
 */

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

            //this the logic for each projectile of the player's shuriken it will check if it collided with the meteors 
            foreach (var projectile in playerShurikens)
            {
                //get the list of meteors
                var meteors = Projectile.projectiles.Where(m => m is MeteorProjectile).ToList();

                //inside the forloop for all count of meteor
                for (int i = 0; i < meteors.Count; i++)
                {
                    var currentMeteor = meteors[i];

                    //it will check if the bounds of the projectile will intersect with the current meteor 
                    if (projectile.GetProjectileBounds().Intersects(currentMeteor.GetProjectileBounds()))
                    {
                        //then it will play the collision sound effect if there is a sound effect for meteor projectile when it collided with the shuriken
                        currentMeteor.Destroy();
                        //when it happen then remove the current meteor in the scene and also the shuriken that collided together.
                        Projectile.projectiles.Remove(currentMeteor);
                        Projectile.projectiles.Remove(projectile);
                    }
                   
                }


                for (int i = 0; i < enemies.Count; i++)
                {
                    var currentEnemy = enemies[i];
                    if(projectile.GetProjectileBounds().Intersects(currentEnemy.getEnemyBounds()))
                    {
                        //then it will play the collision sound effect if there is a sound effect for meteor projectile when it collided with the shuriken
                        currentEnemy.PlayCollisionSoundEffect();
                        currentEnemy.Kill();
                        Enemy.enemies.Remove(currentEnemy);
                        Projectile.projectiles.Remove(projectile);

                    }
                }

               
            }

            //killing player via collision with the meteor and enemy fireball
            //to get an enemyProjectile from the list I used the linq to getn an enemy projectile or a meteorprojectile from the projectiles list.
            var enemyProjectile = Projectile.projectiles.Where(p => p is EnemyProjectile || p is MeteorProjectile).ToList();
            
            //inside the loop of enemyProjectile.count then it will loop until it will check if there is already an intersection between the playerbounds and also the current enemybounds.
            for (int i = 0; i < enemyProjectile.Count; i++)
            {
                var currentEnemy = enemyProjectile[i];
                var playerBounds = player.GetPlayerBounds();
                var currentEnemyBounds = currentEnemy.GetProjectileBounds();


                if(playerBounds.Intersects(currentEnemyBounds))
                {
                    //if it intersects. we can provide the references of playerbounds and current enemy bounds and get the hitpoint
                    Rectangle.Intersect(ref playerBounds, ref currentEnemyBounds, out Rectangle hitPoint);
                    //by using the method Kill under player's class. it will get the killPoint and play as well the collisionsound effect and also show the pow texture inbetween the collision of the enemy and also the player.
                    player.Kill(hitPoint);
                }
            }

            base.Update(gameTime);
        }
    }
}
