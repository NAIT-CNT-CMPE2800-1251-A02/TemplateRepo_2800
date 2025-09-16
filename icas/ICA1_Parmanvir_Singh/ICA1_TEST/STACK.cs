using System;
using System.Collections.Generic;
using System.Linq;

////////////////////////////////////////////////////////////
// ICA #01 - Generics, Extension Methods, Indexers
// Parmanvir Singh - Jan 25 2024
// Submission Code : CMPE2800_1232_ICA01
// Mostly test code stuff 
////////////////////////////////////////////////////////////

namespace ICA1_TEST
{

    public class STACK<T> : Stack<T>
    {
        /// <summary>
        /// Indexer that allows access to the stack by its *categorized index*.
        /// For example: stack[0] returns the first categorized element.
        /// </summary>
        /// <param name="index">Zero-based index of the categorized collection.</param>
        /// <returns>The key (element) at the given position.</returns>
      
        public T this[int index]
        {
            get
            {
                // Convert the categorized dictionary to a list of key-value pairs.
                // Each KeyValuePair<T,int> represents an element and its count.
                List<KeyValuePair<T, int>> category = this.Categorize().ToList();

                // Validate index boundaries.
                if (index < 0 || index >= category.Count)
                {
                    throw new IndexOutOfRangeException(
                        $"Index {index} is out of range for categorized stack (valid range: 0–{category.Count - 1})."
                    );
                }

                // Return the element (key) at the given index.
                return category[index].Key;
            }
        }

        /// <summary>
        /// Indexer that allows access to the stack by a *key* (element).
        /// Returns the categorized value (such as frequency count) for that key.
        /// </summary>
        /// <param name="key">The element to look up.</param>
        /// <returns>The categorized value for the given key.</returns>
        public int this[T key]
        {
            get
            {
                // Build categorized dictionary of elements and their values.
                Dictionary<T, int> categorized = this.Categorize();

                // Ensure the requested key exists.
                if (!categorized.ContainsKey(key))
                {
                    throw new ArgumentException(
                        $"Key '{key}' does not exist in categorized elements."
                    );
                }

                // Return the value (count) associated with the key.
                return categorized[key];
            }
        }
    }
}
