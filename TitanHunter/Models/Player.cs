using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Services;

namespace TitanHunter.Models
{
    public class Player : DrawableGameComponent
    {
        public Texture2D playerTexture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 xMovementSpeed { get; set; }
        public Vector2 yMovementSpeed { get; set; }


        private KeyboardState kStateOld = Keyboard.GetState();

        private MainGame mainGame;

        public bool isMoving = false;

        private SoundEffect collisionSoundEffect;

        private SoundEffect gameWonSoundEffect;

        private GameLevelService gameLevelService;

        private Texture2D killTexture;

        private Rectangle killHitPoint;


        public Player(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;
            InitializeTexture();
            InitializeSoundEffect();
            xMovementSpeed = new Vector2(4, 0);
            yMovementSpeed = new Vector2(0, 4);
            this.position = new Vector2(Shared.stage.X / 8 - playerTexture.Width / 2, Shared.stage.Y / 2 - playerTexture.Height / 2);

        }

        public void InitializeTexture()
        {
             this.playerTexture = mainGame.Content.Load<Texture2D>("images/playerR");
             this.killTexture = mainGame.Content.Load<Texture2D>("images/pow");

        }

        public void InitializeSoundEffect()
        {
            collisionSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/gameover");
            gameWonSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/gamewon");
        }

        public void Reset()
        {
            this.position = new Vector2(Shared.stage.X / 8 - playerTexture.Width / 2, Shared.stage.Y / 2 - playerTexture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            isMoving = false;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (position.X > 0)
                {
                    position -= xMovementSpeed;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {

                if (position.X + playerTexture.Width < Shared.stage.X)
                {
                    position += xMovementSpeed;
                }
            }



            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (position.Y > Shared.SHARED_HEIGHT)
                {
                    position -= yMovementSpeed;
                }

            }


            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (position.Y + playerTexture.Height < Shared.stage.Y)
                {
                    position += yMovementSpeed;
                }

            }


            if (keyboardState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                //Player projectile is a new form of a projectile where this is the default projectile of the shuriken.
                Projectile.projectiles.Add(new PlayerProjectile(mainGame, position));
            }

            kStateOld = keyboardState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();
            mainGame._spriteBatch.Draw(playerTexture, position, Color.White);

            if(gameLevelService.IsGameOver() == true)
            {
                mainGame._spriteBatch.Draw(killTexture, new Vector2(killHitPoint.X-killTexture.Width/2, killHitPoint.Y-killTexture.Height/2), Color.White);
            }

            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }


        public Rectangle GetPlayerBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.playerTexture.Width, this.playerTexture.Height);
        }

        public void PlayCollisionSoundEffect()
        {
            if (this.collisionSoundEffect != null)
            {

                this.collisionSoundEffect.Play();
            }
        }

        public void PlayGameWonSoundEffect()
        {
            if(this.gameWonSoundEffect !=null)
            {
                 this.gameWonSoundEffect.Play();
            }
        }

        public void Kill(Rectangle hitPoint)
        {
            killHitPoint = hitPoint;
            gameLevelService.PlayerIsDead();
            PlayCollisionSoundEffect();        
        }

        public void GameEnd()
        {
            if(gameLevelService.IsGameWon() == true)
            {
                PlayGameWonSoundEffect();
            }
        }

    }
}
