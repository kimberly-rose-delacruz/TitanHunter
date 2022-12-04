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
        SpriteBatch spriteBatch;
        protected Texture2D texture;
        public Vector2 Position = Vector2.Zero;
        public Color Color = Color.White;
        public Vector2 Origin;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;
        protected Rectangle[] Rectangles;
        protected int FrameIndex = 0;


        public PlayerManager(Game game,
            Texture2D texture,
            int frames) : base(game)
        {
            this.texture = texture;
            int width = texture.Width / frames;
            Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.texture, Position, Rectangles[FrameIndex], Color, Rotation, Origin, Scale, SpriteEffect, 0f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
