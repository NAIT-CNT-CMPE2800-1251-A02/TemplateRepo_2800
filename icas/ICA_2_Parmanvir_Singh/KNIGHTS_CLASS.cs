using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICA_2
{
    public class KNIGHTS_CLASS : IEnumerable
    {
        (int, int) VALUE;//initilize the tupple

         (int x, int y)[] KnightMoves = {(2, 1), (2, -1), (-2, 1), (-2, -1),(1, 2), (1, -2), (-1, 2), (-1, -2)};//move valids 

        public KNIGHTS_CLASS(int x,int y) 
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)//checking the moves valids
            {
                throw new ArgumentOutOfRangeException("Position must be within 0–7 for both x and y.");
            }

            VALUE = new(x, y);//setting the tuple
        }
        public IEnumerator GetEnumerator()
        {
            foreach ((int,int) knight in KnightMoves) 
            {
                (int, int) newposition = new(knight.Item1+VALUE.Item1,knight.Item2+VALUE.Item2);

                if (newposition.Item1>= 0 && newposition.Item1 <= 7 && newposition.Item2 >= 0 && newposition.Item2 <= 7) 
                {
                    yield return newposition;
                }

            }
        }



    }
}
