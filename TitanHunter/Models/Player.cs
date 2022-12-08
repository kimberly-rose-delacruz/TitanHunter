/*Player.cs
 *      This class is the representation of the main player of the game. it composes of the GameManager for services, PlayerAnimatinon for 2D animation of the controlling of walk directions, Projectile class for throwing shurikens.
 *      
 *   Revision History:
 *          Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
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
        private GameManager gameLevelService;

        private Texture2D killTexture;
        private Rectangle killHitPoint;

        private PlayerAnimation playerAnimation;
        private PlayerAnimation[] playerAnimations = new PlayerAnimation[3];
        private const int FRAME = 3;
        private const int FRAME_PER_SECOND = 6;
        private const int MOVEMENT_SPEED_X = 4;
        private const int MOVEMENT_SPEED_Y = 4;

        //this is to initialize all necessary resources and gamemanager service and to use again the maingame to manage the update and draw methods.
        public Player(MainGame game) : base(game)
        {
            this.mainGame = game;
            this.gameLevelService = game.gameLevelService;

            //i separate the methods for resources due to its categorization where all textures should have the texture of the effect when player is dead it will show the pow, the main texture of the player, and also the animation sprite sheets.
            InitializeTexture();
            //while this sound effect is just the resources for gameover and gamewon
            InitializeSoundEffect();
            xMovementSpeed = new Vector2(MOVEMENT_SPEED_X, 0);
            yMovementSpeed = new Vector2(0, MOVEMENT_SPEED_Y);
            //default position of the player will be in the left most area of the stage half of the stage height.
            this.position = new Vector2(Shared.stage.X / 8 - playerTexture.Width / 2, Shared.stage.Y / 2 - playerTexture.Height / 2);

        }

        //this is the method i use to initialize the resource for texture.
        public void InitializeTexture()
        {
            this.playerTexture = mainGame.Content.Load<Texture2D>("images/playerR");
            this.killTexture = mainGame.Content.Load<Texture2D>("images/pow");
            playerAnimations[0] = new PlayerAnimation(mainGame, mainGame.Content.Load<Texture2D>("images/walkdown"), FRAME, FRAME_PER_SECOND);
            playerAnimations[1] = new PlayerAnimation(mainGame, mainGame.Content.Load<Texture2D>("images/walkright"), FRAME, FRAME_PER_SECOND);
            playerAnimations[2] = new PlayerAnimation(mainGame, mainGame.Content.Load<Texture2D>("images/walkUp"), FRAME, FRAME_PER_SECOND);

            //default player animation to show walk right position 
            this.playerAnimation = this.playerAnimations[1];
        }

        //this is the method i use to initialize the resource for soundEffect when player wins or loses.
        public void InitializeSoundEffect()
        {
            collisionSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/gameover");
            gameWonSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/gamewon");
        }

        //this method will reset the player's position where it started. 
        public void Reset()
        {
            this.position = new Vector2(Shared.stage.X / 8 - playerTexture.Width / 2, Shared.stage.Y / 2 - playerTexture.Height / 2);
        }
        
        //when game has been called to update something from the game it will do this part.
        public override void Update(GameTime gameTime)
        {
            //within this logic, it composes the logic when player moves up, down, left and right. it will show correct sprite sheet animation to use in correspond to the key press by the player. 
            KeyboardState keyboardState = Keyboard.GetState();
            isMoving = false;

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (position.X > 0)
                {
                    position -= xMovementSpeed;
                    playerAnimation = playerAnimations[1];
                    isMoving = true;
                }
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {

                if (position.X + playerTexture.Width < Shared.stage.X)
                {
                    position += xMovementSpeed;
                    playerAnimation = playerAnimations[1];
                    isMoving = true;
                }
            }



            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (position.Y > Shared.SHARED_HEIGHT)
                {
                    position -= yMovementSpeed;
                    playerAnimation = playerAnimations[2];
                    isMoving = true;
                }

            }


            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (position.Y + playerTexture.Height < Shared.stage.Y - Shared.WALL_HEIGHT)
                {
                    position += yMovementSpeed;
                    playerAnimation = playerAnimations[0];
                    isMoving = true;
                }

            }

            //when player presses the spacebar it will create a new projectile image of the shuriken to show the player is attacking the enemies to kill titans or destroy the meteors.
            if (keyboardState.IsKeyDown(Keys.Space) && kStateOld.IsKeyUp(Keys.Space))
            {
                //Player projectile is a new form of a projectile where this is the default projectile of the shuriken.
                Projectile.projectiles.Add(new PlayerProjectile(mainGame, position));
                playerAnimation = playerAnimations[1];
                isMoving = false;
            }

            //this is to give the player animation position  
            playerAnimation.Position = new Vector2(position.X, position.Y);

            //there is also condition if isMoving is true it will update the player animation.
            if (isMoving)
            {
                playerAnimation.Update(gameTime);
            }
            else
            {
                //if it's not moving then set frameIndex to 1.
                playerAnimation.setFrame(1);
            }

            kStateOld = keyboardState;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Begin();

            playerAnimation.Draw(gameTime);

            //if game is over true then it will show the pow texture in between of the rectangle of the player  and the powtexture so it will look like it collided.
            if (gameLevelService.IsGameOver() == true)
            {
                mainGame._spriteBatch.Draw(killTexture, new Vector2(killHitPoint.X - killTexture.Width / 2, killHitPoint.Y - killTexture.Height / 2), Color.White);
            }

            mainGame._spriteBatch.End();
            base.Draw(gameTime);
        }

        //created this function to return a rectangle to get the playerbounds for collision of it towards the enemyprojectile or meteorprojectile
        public Rectangle GetPlayerBounds()
        {

            return new Rectangle((int)this.position.X, (int)this.position.Y, this.playerTexture.Width, this.playerTexture.Height);
        }

        //this is a method to play the collision sound effect when it is being used from the loaded content.
        public void PlayCollisionSoundEffect()
        {
            if (this.collisionSoundEffect != null)
            {
                //play the collision sound effect when collision happens.
                this.collisionSoundEffect.Play();
            }
        }

        //play the game won sound effect when the player reach the goal of the game to kill all the spawned titans within the stage.
        public void PlayGameWonSoundEffect()
        {
            if(this.gameWonSoundEffect !=null)
            {
                 this.gameWonSoundEffect.Play();
            }
        }

        //method to when player is getting killed by the enemies or hit by the meteorprojectile.
        public void Kill(Rectangle hitPoint)
        {
            //setting the killHitPoint 
            killHitPoint = hitPoint;
            //gameLevelService to set IsGameOver true and report it to let the whole game that player is alreaedy dead and show some messages to headercomponent that game is over.
            gameLevelService.PlayerIsDead();
            //then we can now use the method to play the collision sound effect.
            PlayCollisionSoundEffect();        
        }

        //method to know that gameending completely when player has won the game by killing x number of enemies.
        public void GameEnd()
        {
            //I used a function from the game manager to know if the player won the game by knowing the condition if the player killed total of x number of titans and clear all of them
            if(gameLevelService.IsGameWon() == true)
            {
                //then play the game won sound effect. same case in the header component where I did call this IsGamewon true then it will show the message above.
                PlayGameWonSoundEffect();
            }
        }

    }
}
