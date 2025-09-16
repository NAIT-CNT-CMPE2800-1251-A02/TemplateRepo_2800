using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


////////////////////////////////////////////////////////////
// ICA #01 - Generics, Extension Methods, Indexers
// Parmanvir Singh - Jan 25 2024
// Submission Code : CMPE2800_1232_ICA01
// Mostly test code stuff 
////////////////////////////////////////////////////////////
namespace ICA1_TEST
{

    public static class CLASS_1
    {
       static Random random = new Random();


        //public static Dictionary<int, int> Categorize(this List<int> sourcelist)
        //{
        //    Dictionary<int, int> TEMP = new Dictionary<int, int>();

        //    foreach (int item in sourcelist)
        //    {
        //        if (TEMP.ContainsKey(item)) { TEMP[item]++; }

        //        else { TEMP.Add(item, 1); }

        //    }
        //    TEMP = TEMP.OrderBy(x => x.Key).ToDictionary();


        //    return TEMP;

        //}

        //public static Dictionary<T, int> Categorize<T>(this List<T> sourcelist)
        //{
        //    Dictionary <T, int> TEMP = new Dictionary<T, int>();

        //    foreach (T item in sourcelist)
        //    {
        //        if (TEMP.ContainsKey(item)) { TEMP[item]++; }

        //        else
        //        {
        //            TEMP.Add(item, 1); 
        //        }

        //    }

        //    return TEMP.OrderBy(x => x.Key).ToDictionary(x=>x.Key,y=>y.Value);

        //}


        /// <summary>
        /// Groups elements of a collection into a dictionary of frequencies.
        /// For each unique element, counts how many times it appears.
        /// </summary>
        public static Dictionary<K, int> Categorize<K>(this IEnumerable<K> sourcelist)
        {
            if (sourcelist == null)
                throw new ArgumentNullException("Source collection cannot be null.");

            Dictionary<K, int> counts = new Dictionary<K, int>();

            foreach (K item in sourcelist)
            {
                if (counts.ContainsKey(item))
                    counts[item]++;
                else
                    counts.Add(item, 1);
            }

            return counts.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
        }

        /// <summary>
        /// Returns the second-to-last element in a sequence.
        /// </summary>
        /// <typeparam name="T">The element type.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <returns>
        /// The second-to-last element if available.
        /// Returns a new instance of T if there is only one element.
        public static T Penultimate<T>(this IEnumerable<T> source) where T : new()
        {
            if (source == null)
                return new T();

            if (!source.Any())
                throw new ArgumentException("Collection is empty, cannot get penultimate element.");

            if (source.Count() < 2)
                return new T();

            return source.Reverse().Skip(1).First();
        }

        /// <summary>
        /// Returns a random (Key, Value) pair from a dictionary.
        /// </summary>
        /// <typeparam name="K">The key type.</typeparam>
        /// <typeparam name="V">The value type.</typeparam>
        /// <param name="source">The dictionary source.</param>
        /// <returns>A random KeyValuePair as a tuple.</returns>
        public static (K Key, V Value) Rando<K, V>(this Dictionary<K, V> source)
        {
            if (source == null || source.Count == 0)
                throw new ArgumentException("Dictionary is empty, cannot select random element.");

            int m = random.Next(source.Count());

            return ((source.ElementAt(m)).Key, source.ElementAt(m).Value);
        }

        /// <summary>
        /// Finds the first adjacent duplicate element in a sequence.
        /// </summary>
        /// <typeparam name="K">The element type (must implement IEquatable).</typeparam>
        /// <param name="source">The sequence of elements.</param>
        /// <returns>The first element that appears twice in a row, or default(K) if none exist.
        public static K AdjacentDuplicate<K>(this IEnumerable<K> source) where K : IEquatable<K>
        {
            if (source == null)
                throw new ArgumentNullException("Source collection cannot be null.");

            IEnumerator<K> enumerator = source.GetEnumerator();
            
                if (!enumerator.MoveNext())
                    return default (K); // Empty sequence → no duplicates.

                K previous = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    K current = enumerator.Current;

                    if (previous.Equals(current))
                        return current;

                    previous = current;
                }
            

            return default (K); // No adjacent duplicates found.
        }

        /// <summary>
        /// Returns a string representation of a sequence and its element count.
        /// </summary>
        /// <typeparam name="K">The element type.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <returns>
        /// A tuple (string display, int count).
        /// If the sequence is empty, returns ("[EMPTY]", 0).
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if the source is null.</exception>
        public static (string displaystring, int count) StringDisplay<K>(this IEnumerable<K> source)
        {
            if (source == null)
                throw new ArgumentNullException("Source collection cannot be null.");

            List<K> list = source.ToList();

            if (list.Count == 0)
                return ("[EMPTY]", 0);

            string disp = string.Join(",", list.Select(x => x?.ToString() ?? "null"));

            return (disp, list.Count);
        }
    }

}

