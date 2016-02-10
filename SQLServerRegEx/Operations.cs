using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class Operations {

        /// <summary>
        /// Method to capitalize a string (e.g., make all letters uniformly upper cased)
        /// </summary>
        /// <param name="source">The string on which to perform the operation</param>
        /// <returns>A string that is returned with all upper-cased values</returns>
        public static string Capitalize(this string source) {
            return source.ToUpper();
        }

        /// <summary>
        /// Splits string into an array of individual characters/string elements
        /// </summary>
        /// <param name="source">A string to split into the container class</param>
        /// <returns>An array of strings containing the individual string elements</returns>
        public static string[] SplitIntoIndividualElements(string source) {
            var stringCollection = new string[source.Length];
            for (int i = 0; i < stringCollection.Length; i++) {
                stringCollection[i] = source[i].ToString();
            }
            return stringCollection;
        }

        /// <summary>
        /// Method that combines an enumerable list of strings into a single string
        /// </summary>
        /// <param name="source">An enumerable interface for strings</param>
        /// <returns>A single string value</returns>
        public static string MergeIndividualElementsIntoString(IEnumerable<string> source) {
            var returnString = "";
            for (int i = 0; i < source.Count(); i++) {
                returnString += source.ElementAt<string>(i);
            }
            return returnString;
        }

        /// <summary>
        /// Method to return a string as a list of individual string elements of varying lengths
        /// </summary>
        /// <param name="source">The string on which the operation should be performed</param>
        /// <returns>A list of strings</returns>
        public static List<string> ListPrefixes(this string source)  {
            var prefixes = new List<string>();
            for (int i = 0; i < source.Length; i++) {
                prefixes.Add(source.Substring(0, i));
            }
            return prefixes;
        }

        /// <summary>
        /// A method to return a list of Bi-grams
        /// </summary>
        /// <param name="source">The string on which to perform the operation</param>
        /// <returns>A list of string elements containing the bi-grams of the string</returns>
        public static List<string> ListBiGrams(this string source) {
            return ListNGrams(source, 2);
        }

        /// <summary>
        /// A method to return a list of Tri-grams
        /// </summary>
        /// <param name="source">The string on which to perform the operation</param>
        /// <returns>A list of string elements containing the Tri-grams of the string</returns>
        public static List<string> ListTriGrams(this string source) {
            return ListNGrams(source, 3);
        }

        /// <summary>
        /// A method to return a list of N-grams
        /// </summary>
        /// <param name="source">The string on which to perform the operation</param>
        /// <param name="n">The length of the individual n-gram elements</param>
        /// <returns>A list of string elements containing the n-grams of the string</returns>
        public static List<string> ListNGrams(this string source, int n) {
            var nGrams = new List<string>();
            if (n > source.Length) return null;
            else if (n == source.Length) {
                nGrams.Add(source);
                return nGrams;
            }
            else {
                for (int i = 0; i < source.Length - n; i++) {
                    nGrams.Add(source.Substring(i, n));
                }
                return nGrams;
            }
        }

    } // End of Class declaration

} // End of Namespace declaration
