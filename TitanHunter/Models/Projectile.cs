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

        public Projectile(MainGame mainGame,
            Vector2 newPosition) : base(mainGame)
        {
            this.mainGame = mainGame;


            position = newPosition;
            direction = Dir.Right;
            //loading the projectile texture
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

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        //To do implementation for collision
        public bool Collided
        {
            get { return collided; }
            set { collided = value; }
        }

        public override void Update(GameTime gameTime)
        {

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

        public Rectangle getBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.projectileTexture.Width, this.projectileTexture.Height);
        }

        public virtual void PlayCollisionSoundEffect()
        {
            if (this.collisionSoundEffect != null)
            {

                this.collisionSoundEffect.Play();
            }
        }

        public virtual void Destroy()
        {
            PlayCollisionSoundEffect();
           
        }

    }
}
