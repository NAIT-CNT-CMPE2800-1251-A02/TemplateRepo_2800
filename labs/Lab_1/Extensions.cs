using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_1.HandS;

namespace Lab_1
{
    public static class Extension
    {
        
            // Enumerates surviving types lazily
            public static IEnumerable<HandType> ActiveTypes(this IEnumerable<Hand> hands)
            {
                foreach (var h in hands)
                    yield return h.Type;
            }

            public static HandType? IsWinCondition(this IEnumerable<Hand> hands)
            {
                var distinct = hands.ActiveTypes().Distinct().ToList();
                return distinct.Count() == 1 ? distinct[0] : null;
            }
    }




    
}
