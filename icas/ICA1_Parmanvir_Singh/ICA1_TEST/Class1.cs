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
            if (sourcelist == null)//if its null just do Expection
                throw new ArgumentNullException("Source collection cannot be null.");

            Dictionary<K, int> counts = new Dictionary<K, int>();

            foreach (K item in sourcelist)
            {
                if (counts.ContainsKey(item))//if it contain thhen add to the source
                    counts[item]++;
                else
                    counts.Add(item, 1);//else make the key 
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
        public static T Penultimate<T>(this IEnumerable<T> source)
            where T : new()
        {
            if (source == null)//checking the null
                return new T();

            if (!source.Any())//if its empty nothing there
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
        public static K AdjacentDuplicate<K>(this IEnumerable<K> source)
            where K : IEquatable<K>
        {
            if (source == null)//checking the null
                throw new ArgumentNullException("Source collection cannot be null.");

            IEnumerator<K> enumerator = source.GetEnumerator();
            
                if (!enumerator.MoveNext())//if its empty the gona return deafut type
                    return default (K); 

                K previous = enumerator.Current;//making the first to the previous

                while (enumerator.MoveNext())//going to move next make the next eement to the current & current to the previous
                {
                    K current = enumerator.Current;

                    if (previous.Equals(current))
                        return current;

                    previous = current;
                }
            //JUST THE Logic thing /*
            //"Take the first element and mark it as “previous.”Move through the
            //rest of the collection one element at a time:  Set the current e
            //lement.If the current element is the same as the previous one, return it.Ot
            //herwise, update “previous” to this current element and keep going.If no consecutive duplicates are found, nothing is returned.*/
            return default(K);
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

            //if its 0 return the empty as string
            if (source.Count() == 0)
                return ("[EMPTY]", 0);
                                                        //if its nothing there make string 'null'
            return (string.Join(",", source.Select(x => x?.ToString() ?? "null")), source.Count());
        }
    }

}

