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
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;

                else if (isLooping)
                    FrameIndex = 0;
            }

            base.Update(gameTime);
        }


        public void setFrame(int frame)
        {
            FrameIndex = frame;
        }
    }
}
