/*Enemy.cs
 *  This class represents the enemy within the game as the Titan that needs to be defeated by the player. In this code I did all code pertaining to enemy, such as creating more enemies by using a list.
 *  
 *  Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Services;

namespace TitanHunter.Models
{
    public class Enemy : DrawableGameComponent
    {
        //default enemy attack timer and its reset time value.
        public double enemyAttackTimer = 0D;
        public double enemyAttackResetTimeValue = 3.5D;

        //creating a list of enemies to be spawned in the field of action scene
        public static List<Enemy> enemies = new List<Enemy>();

        //attributes of the enemy
        public Vector2 position { get; set; }
        private Texture2D enemyTexture { get; set; }
        //sound effect.
        protected SoundEffect collisionSoundEffect;
        private MainGame mainGame;

        public Enemy(MainGame game, Vector2 position) : base(game)
        {

            this.mainGame = game;
            //initialize the resource for this enemy to be displayed in the field.
            InitializeResources();

            //handle spawning of enemy if enemy height is greater than the stage height
            if (position.Y > Shared.stage.Y - enemyTexture.Height - Shared.WALL_HEIGHT)
            {
                position.Y = Shared.stage.Y - enemyTexture.Height - Shared.WALL_HEIGHT;
            }

            //handle spawning of enemy if enemy will be spawned beyond the header height re-position the enemy inside the dungeon field.
            if(position.Y < HeaderComponent.HEADER_HEIGHT)
            {
                position.Y = HeaderComponent.HEADER_HEIGHT;
            }    
    
            this.position = position;

        }

        //created this method to initialize the resources for enemy class.
        protected virtual void InitializeResources()
        {
            //loading the texture of the enemy from the loaded content.
            this.enemyTexture = mainGame.Content.Load<Texture2D>("images/enemy");
        }

        //every time the enemy is called from the actioneScene this update will show the enemy in a  different position within the field or the dungeon background.
        public override void Update(GameTime gameTime)
        {
            //gameTime elapsedGameTime in seconds value is a changing value given by the game when updating attributes I used this value based on the reference to get a time where I can randomly throw ball to the player as a counter attack.
            enemyAttackTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            
            if(enemyAttackTimer <= 0)
            {
                //when this is executed i will reset the attacktimer again into 3.5D seconds
                enemyAttackTimer = enemyAttackResetTimeValue;
                //then throw a ball from an enemy.
                Projectile.projectiles.Add(new EnemyProjectile(mainGame, position));
            }
            base.Update(gameTime);
        }

        //drawing the enemy.
        public override void Draw(GameTime gameTime)
        {      
            mainGame._spriteBatch.Draw(enemyTexture, position, Color.White);
            base.Draw(gameTime);
        }

        //a function that will get the enemybounds as a rectangle for colliding it with the player.
        public Rectangle getEnemyBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.enemyTexture.Width, this.enemyTexture.Height);
        }

        //this is a method where I play the collision sound effect when it is not null.
        public virtual void PlayCollisionSoundEffect()
        {
            if (this.collisionSoundEffect != null)
            {

                this.collisionSoundEffect.Play();
            }
        }

        //this method will be called when one of the enemies have been killed
        public virtual void Kill()
        {
            //if this methoed is executed, added a service to report that the gameLevelService to increment killed enemy for scoring purposes. 
            mainGame.gameLevelService.IncrementKilledEnemy();
        }

    }
}
