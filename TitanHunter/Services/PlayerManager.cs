/*PlayerManager.cs
 * 
 *  this class manages the player animation. this draw the rectangle frame from the sprite sheet uploaded in the content manager. 
 *  
 *  Revision History:
 *      Created on December 6, 2022 by Kimberly Rose Dela Cruz
 *  
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace TitanHunter.Services
{
    public class PlayerManager : DrawableGameComponent
    {
        //declaring variables
        protected Texture2D texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;
        private MainGame mainGame;

        public PlayerManager(MainGame game,
            Texture2D texture,
            int frames) : base(game)
        {
            this.mainGame = game;
            this.texture = texture;
            //getting the width of each frame from the sprite sheet.
            int width = texture.Width / frames;
            //total frames will be provided by the player class in order to produce the rectangles for reference
            Rectangles = new Rectangle[frames];

            //instantiate the rectangles based on the given frames
            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
            }
        }

        //draw the texture and its position based on the given texture from the sprite sheet.
        public override void Draw(GameTime gameTime)
        {
            mainGame._spriteBatch.Draw(this.texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
            base.Draw(gameTime);
        }
    }
}
