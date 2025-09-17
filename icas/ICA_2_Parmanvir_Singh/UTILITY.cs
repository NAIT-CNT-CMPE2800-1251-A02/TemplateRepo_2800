using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICA_2
{
    public static class UTILITY 
    {
       static Random random = new Random();
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            List<T> l = new List<T>(source);

            if (source == null) { throw new ArgumentException("NULL LIST"); }

            void Swap(int a, int b)
            {
                T temp = l[a];
                l[a] = l[b];
                l[b] = temp;
            }

            for (int i = 0; i < source.Count(); i++)
            {
                int randomelement= random.Next(i,source.Count());

                //T j = source.ElementAt(randomelement);
                Swap(i,randomelement);

                yield return l[i];

            }
        }

        public static IEnumerable<List<T>> Peel<T>(this IEnumerable<T> source) 
        {
            int left = 0;
            int right = source.Count()-1;

            while (left <= right)
            {
                if (left == right)
                {
                    yield return new List<T> { source.ElementAt(left) };
                }
                else
                {
                    yield return new List<T> { source.ElementAt(left), source.ElementAt(right) };
                }
                ++left;--right;
            }
        }

        public static IEnumerable<T> Sample<T>(this IEnumerable<T> source) 
        {
            if (source.Count() == 0) { yield break; }

            while (true) 
            {
                int i= random.Next(0,source.Count());
                yield return source.ElementAt(i);
            }
        
        }

    }
}
