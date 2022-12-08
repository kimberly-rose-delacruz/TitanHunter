/*MeteorProjectile.cs
 *  This meteor projectile is the throwing of meteors coming from enemy's side going left. This is part of the difficult of the player's game where it added to the journey where it will be harder to destroy everything at ones.   
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
    //meteor projectile is throwing of meteors therefore I just inherited the same class of projectile to get same features of it.
    public class MeteorProjectile : Projectile
    {
        //instantiating different direction and speed for the meteor.
        public MeteorProjectile(MainGame mainGame, Vector2 newPosition) : base(mainGame, newPosition)
        {
            this.direction = Dir.Left;
            this.speed = 250;

            //adjusting the position if in case the position is beyond the stage height so I needed to change it height minue the wall height for it to re-position in the middle of the stage.
            if (position.Y > Shared.stage.Y - projectileTexture.Height - Shared.WALL_HEIGHT)
            {
                position.Y = Shared.stage.Y - projectileTexture.Height - Shared.WALL_HEIGHT;
            }

            //repositioning the meteor from above beyond the header height
            if (position.Y < HeaderComponent.HEADER_HEIGHT)
            {
                position.Y += HeaderComponent.HEADER_HEIGHT;
            }


        }

        //reuse existing function from Projectile and override its definition to load the texture for meteor
        protected override void InitializeTexture()
        {
            this.projectileTexture = mainGame.Content.Load<Texture2D>("images/meteor");
        }

        //reuse existing function from Projectile and override its definition for sound effect of projectile of meteor.
        protected override void InitializeSoundEffect()
        {
            this.projectileSoundEffect = mainGame.Content.Load<SoundEffect>("sounds/fire");
            this.projectileSoundEffect.Play();
           
        }

        //reuse existing function from inherited class Projectile to add an additional logic came from the gameLevelService to report the destory meteorProjectile by the player as a score.
        public override void Destroy()
        {
            //adding this statement to increment destroyed meteor to report it as a score.
            mainGame.gameLevelService.IncrementDestroyedMeteor();
            //re-executing the old behavior to play the collisioneffect for destroyed meteor if there is initialize soundEffect which is not equal to null..
            base.Destroy();
        }

    }
}
