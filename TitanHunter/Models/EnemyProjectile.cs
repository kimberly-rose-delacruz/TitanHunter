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
    public class EnemyProjectile : Projectile
    {
        public EnemyProjectile(MainGame mainGame, Vector2 newPosition) : base(mainGame, newPosition)
        {
            this.direction = Dir.Left;
            this.speed = 300;
        }


        protected override void InitializeTexture()
        {
            this.projectileTexture = mainGame.Content.Load<Texture2D>("images/ball");
        }

        protected override void InitializeSoundEffect()
        {
            this.projectileSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/fire");
            this.projectileSoundEffect.Play();

        }

    }
}
