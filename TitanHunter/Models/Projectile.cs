/*Projectile.cs
 *         the parent class of all projectile classes such as playerProjectile, meteor Projectile and enemyProjectile to show and update the movement of a certain object towards a specific direction with specific speed.
 *         
 * REvision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1;

namespace TitanHunter.Models
{
    public class Projectile : DrawableGameComponent
    {
        //created a list for projectiles.
        public static List<Projectile> projectiles = new List<Projectile>();

        protected Vector2 position;
        protected int speed = 500;
        protected int radius = 18;
        protected Dir direction;
        protected bool collided = false;
        protected MainGame mainGame;
        protected Texture2D projectileTexture;
        protected SoundEffect projectileSoundEffect;
        protected SoundEffect collisionSoundEffect;
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Projectile(MainGame mainGame,
            Vector2 newPosition) : base(mainGame)
        {
            this.mainGame = mainGame;
            position = newPosition;
            direction = Dir.Right;
            //loading all resources  for both texture and soundEffect for the player.
            InitializeTexture();
            InitializeSoundEffect();

        }

        //initialization of texture for the player's projectile of shuriken shoot
        protected virtual void InitializeTexture()
        {
            this.projectileTexture = mainGame.Content.Load<Texture2D>("images/shuriken");
        }

        //initialize sound effect for player's projectile of shuriken shoot.
        protected virtual void InitializeSoundEffect()
        {
            this.projectileSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/shurikenSound");
            this.projectileSoundEffect.Play();
        }

        public override void Update(GameTime gameTime)
        {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //projectile direction whether the direction specified by the player, enemy and meteor.
            
            switch (direction)
            {
                case Dir.Right:
                    position.X += speed * dt;
                    break;
                case Dir.Left:
                    position.X -= speed * dt;
                    break;
                case Dir.Up:
                    position.Y += speed * dt;
                    break;
                case Dir.Down:
                    position.Y -= speed * dt;
                    break;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(projectileTexture, new Vector2(Position.X, Position.Y), Color.White);
            base.Draw(gameTime);
        }

        //created this function to return a rectangle for collision management when it will hit or collided with the enemy or the meteor projectile.
        public Rectangle GetProjectileBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.projectileTexture.Width, this.projectileTexture.Height);
        }

        //playing the sound effect for collision.
        public virtual void PlayCollisionSoundEffect()
        {
            //the collission will only play if the implementing class sets the collision soundeffect.
            if (this.collisionSoundEffect != null)
            {

                this.collisionSoundEffect.Play();
            }
        }

        //this is the method to player the collisionSoundeffect for the Projectile
        public virtual void Destroy()
        {
            PlayCollisionSoundEffect();
        }

    }
}
