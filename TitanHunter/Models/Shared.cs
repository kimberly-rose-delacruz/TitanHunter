using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter
{
    
    public class Shared
    {
        public static Vector2 stage;
        public const int SHARED_HEIGHT = 83;
    }

    public enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }
}
