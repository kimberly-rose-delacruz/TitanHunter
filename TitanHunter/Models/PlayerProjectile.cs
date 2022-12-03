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
