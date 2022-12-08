/*EnemyProjectile.cs
 *  This class represents the throwing of balls of the enemies. It inherits the class of Projectile that has the same attributes and its function only overrides some methods in order to initialize some resources such as textures and sound effect.
 *  
 *  Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
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
    //to provide the same behavior as the projectile of my player's shuriken by throwing it to the enemy as a counter attack. I inherited the Projectile class for the enemy to throw back some fire ball as a counter towards the player
    public class EnemyProjectile : Projectile
    {

        //initliaze the values of the constant direction for the projectile of the ball towards to the position of the player which is Left.
        //constant speed of the throwing of the ball is provided.
        public EnemyProjectile(MainGame mainGame, Vector2 newPosition) : base(mainGame, newPosition)
        {
            this.direction = Dir.Left;
            this.speed = 300;
        }

        //this is just to give texture for the projectile of the enemy - by throwing the ball.
        protected override void InitializeTexture()
        {
            this.projectileTexture = mainGame.Content.Load<Texture2D>("images/ball");
        }

        //sound effect when the projectile has been thrown
        protected override void InitializeSoundEffect()
        {
            this.projectileSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/fire");
            this.projectileSoundEffect.Play();

        }

    }
}
