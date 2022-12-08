/*PlayerProjectile.cs
 *          This is just inheriting the projectile class as its parent class, since there is nothing to change in here as I already included the structre of the parent class as the player's projectile this will still behave the same way as it is. This will only throw some shurikens to the enemies as an indicator of killing them by using the shuriken image
 *          
 *  Revision History:
 *          Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Models
{
    public class PlayerProjectile : Projectile
    {
        public PlayerProjectile(MainGame mainGame, Vector2 newPosition) : base(mainGame, newPosition)
        {

        }
    }
}
