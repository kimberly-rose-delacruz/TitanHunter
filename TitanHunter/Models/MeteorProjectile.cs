using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Models
{
    public class MeteorProjectile : Projectile
    {
        

        public MeteorProjectile(MainGame mainGame, Vector2 newPosition) : base(mainGame, newPosition)
        {
            this.direction = Dir.Left;
            this.speed = 250;


            if(position.Y < HeaderComponent.HEADER_HEIGHT)
            {
                position.Y += HeaderComponent.HEADER_HEIGHT;
            }
        }

        protected override void InitializeTexture()
        {
            this.projectileTexture = mainGame.Content.Load<Texture2D>("images/meteor");
        }

        protected override void InitializeSoundEffect()
        {
            this.projectileSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/fire");
            this.projectileSoundEffect.Play();
           
        }
    }
}
