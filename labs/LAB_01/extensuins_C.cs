using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using static LAB_FINAL_01.HandS;
using GDIDrawer;

namespace LAB_FINAL_01
{
    public static class HandExtensions
    {

        /// <summary>
        /// Deetermining the winner 
        /// </summary>
        /// <param name="hands"></param>
        /// <returns></returns>
        public static HandType? GetWinner(this IEnumerable<Hand> hands)
        {
           List<HandType> distinctTypes = hands.Select(h => h.Type).Distinct().ToList();
            return distinctTypes.Count == 1 ? distinctTypes[0] : null;
        }
        public static Dictionary<HandType, int> GetTypeCounts(this IEnumerable<Hand> hands)
        {
            return hands.GroupBy(h => h.Type).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
