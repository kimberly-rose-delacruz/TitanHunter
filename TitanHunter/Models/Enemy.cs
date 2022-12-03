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
        public double enemyAttackTimer = 0D;
        public double enemyAttackResetTimeValue = 3.5D;

        public static List<Enemy> enemies = new List<Enemy>();
        public Vector2 position { get; set; }
        private MainGame mainGame;
        private Texture2D enemyTexture { get; set; }
        protected SoundEffect collisionSoundEffect;

        public Enemy(MainGame game, Vector2 position) : base(game)
        {
            
            var newPositionValue = position.Y - 70;

            this.mainGame = game;

            if(newPositionValue > 0)
            {
                position.Y = newPositionValue;
            }
            this.position = position;
            InitializeTexture();

        }

        protected virtual void InitializeTexture()
        {
            this.enemyTexture = mainGame.Content.Load<Texture2D>("images/enemy");
        }

        //todo insert here the initilizeSoundEffect()

        public override void Update(GameTime gameTime)
        {

            enemyAttackTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if(enemyAttackTimer <= 0)
            {
                enemyAttackTimer = enemyAttackResetTimeValue;
                Projectile.projectiles.Add(new EnemyProjectile(mainGame, position));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
      
            mainGame._spriteBatch.Draw(enemyTexture, position, Color.White);
            base.Draw(gameTime);
        }

        public Rectangle getEnemyBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.enemyTexture.Width, this.enemyTexture.Height);
        }

        public virtual void PlayCollisionSoundEffect()
        {
            if (this.collisionSoundEffect != null)
            {

                this.collisionSoundEffect.Play();
            }
        }
        
        public virtual void Kill()
        {

        }

    }
}
