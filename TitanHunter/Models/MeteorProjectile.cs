﻿using Microsoft.Xna.Framework;
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
            var newPositionValue = position.Y - 100;

            //this is to avoid the meteor showing completely out of the bottom of the stage
            if(newPositionValue > 0)
            {
                position.Y = newPositionValue;
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