using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICA_2
{    ////////////////////////////////////////////////////////////
    // ICA #02 - IENURABLE
    // Parmanvir Singh - Jan 25 2024
    // Submission Code : CMPE2800_1232_ICA01
    // Mostly test code stuff 
    ////////////////////////////////////////////////////////////
    public class KNIGHTS_CLASS : IEnumerable
    {
        (int, int) VALUE;//initilize the tupple

         (int x, int y)[] KnightMoves = {(2, 1), (2, -1), (-2, 1), (-2, -1),(1, 2), (1, -2), (-1, 2), (-1, -2)};//move valids 

        /// <summary>
        /// GIVING THE VAUES LIKE INITILIZING
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public KNIGHTS_CLASS(int x,int y) 
        {
            if (x < 0 || x > 7 || y < 0 || y > 7)//checking the moves valids
            {
                throw new ArgumentOutOfRangeException("Position must be within 0–7 for both x and y.");
            }

            VALUE = new(x, y);//setting the tuple
        }
        /// <summary>
        /// JUST A DEAFULT PROTOTYPE
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            foreach ((int,int) knight in KnightMoves)//ITERATING THE MOVES 
            {
                (int, int) newposition = new(knight.Item1+VALUE.Item1,knight.Item2+VALUE.Item2);
                //MAIN CORE OF THE CHESS CHECKING THE POSSIBLE MOVES
                if (newposition.Item1>= 0 && newposition.Item1 <= 7 && newposition.Item2 >= 0 && newposition.Item2 <= 7) 
                {
                    yield return newposition;
                }

            }
        }



    }
}
