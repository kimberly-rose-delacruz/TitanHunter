/*PlayerAnimation.cs
 *   this is a class for playerAnimation where I handle the sprite sheets to make the player texture like a 2D moving object within the game when it is being controlled by the user using keyboard keys up, left, right, and down.
 *   
 *   Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TitanHunter.Services;

namespace TitanHunter.Models
{
    //it inherits the playerManager class where it gives the setting of the texture and frames to be used by the playerAnimation.
    public class PlayerAnimation : PlayerManager
    {
        private float timeElapsed;
        public bool isLooping = true;
        private float timeToUpdate;
        public int FramesPerSecond { set { timeToUpdate = 1f / value; } }
        private MainGame mainGame;
        public PlayerAnimation(MainGame game, Texture2D Texture, int frames, int fps) : base(game, Texture, frames)
        {
            this.mainGame = game;
            FramesPerSecond = fps;
        }

        public override void Update(GameTime gameTime)
        {
            
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //the time to update will be deducted to the timeElapsed or time that past by 
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                //if frameIndex value based from the spritesheet is still less than the total length of frames provided from the spritesheet
                //in my case I have 3 frames so, it will be move tot he next frame to update in the game.
                if (FrameIndex < Rectangles.Length - 1)
                {
                    FrameIndex++;
                }
                else if (isLooping)
                {
                    //then return back to the initial position so it will show the animation of player is moving based on controller set by the player using keyboard keys.
                    FrameIndex = 0;
                }
            }

            base.Update(gameTime);
        }

        //method to set frame according to the provided frame index value.
        public void setFrame(int frame)
        {
            FrameIndex = frame;
        }
    }
}
