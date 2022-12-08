/*Score.cs
 *      A class the holds the information of the player's total score and playtime to be displayed in the High SCore scene page.
 *      
 * Revision History:
 *      Created on December 7, 2022 by Kimberly Rose Dela Cruz
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TitanHunter.Models
{
    public class Score
    {
        public int PlayerTotalScore { get; set; }
        public DateTime PlayTime { get; set; }

    }
}
