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
    public static class UTILITY 
    {
       static Random random = new Random();//make the random so we can use it in later in the code

        /// <summary>
        /// JUST TO DO SIMPLE SHUFFLES AND CONVERT THE TO LIST FOR EASY ASSISABLE
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            List<T> l = new List<T>(source);//making the list bcz after 1 hour searching it is not possible to do without converting

            if (source == null) { throw new ArgumentException("NULL LIST"); }//extra safety
            //make the local function so swaping be simple
            void Swap(int a, int b)
            {
                T temp = l[a];//swaping to first then second to first
                l[a] = l[b];
                l[b] = temp;
            }

            for (int i = 0; i < source.Count(); i++)
            {
                int randomelement= random.Next(i,source.Count());//making a random point 

                //T j = source.ElementAt(randomelement);//cant work without converting
                Swap(i,randomelement);

                yield return l[i];

            }
        }

        /// <summary>
        /// PEEL- STARTIG FORM 1 AND FROM LAST 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<List<T>> Peel<T>(this IEnumerable<T> source) 
        {
            if (source is null) 
            {
                throw new ArgumentNullException("NULL");
            
            }

            int left = 0;
            int right = source.Count()-1;

            while (left <= right)//UNTIL GO TO MIDPOINT
            {
                if (left == right)//RETURN ONE ELEM
                {
                    yield return new List<T> { source.ElementAt(left) };
                }
                else
                {
                    yield return new List<T> { source.ElementAt(left), source.ElementAt(right) };//returning as tuple
                }
                ++left;--right;//MAKING THE CODE WORK
            }
        }

        /// <summary>
        /// EVERYHTING is in user control it fonn GO INTIL INFINITE LOOPING
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> Sample<T>(this IEnumerable<T> source) 
        {
            if (source.Count() == 0) { yield break; }

            while (true) //RUN INDEFINATE
            {
                int i= random.Next(0,source.Count());
                yield return source.ElementAt(i);//RETURN A SPECIFIC ELEMENT GENERAT FROM SOURCE AT RANDOM POSITION
            }
        
        }

    }
}
