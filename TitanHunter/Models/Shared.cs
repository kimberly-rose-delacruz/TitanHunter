/*Shared.cs
 *      this class is just holding an constant Vector 2 stage as a reference for the preferred height and width set from the initialize value of stage of my game. I also include in this page the enum for direction to be accessible by the whole code.
 *      
 * Revision History
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
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
        public const int WALL_HEIGHT = 25;
    }

    public enum Dir
    {
        Down,
        Up,
        Left,
        Right
    }
}
