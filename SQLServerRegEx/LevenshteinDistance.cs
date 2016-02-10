using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

#pragma warning disable CC0065 // Remove trailing whitespace
namespace FuzzyString
{
    public static partial class ComparisonMetrics
    {


        /// <summary>
        /// Method to return the Levenshtein Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Int32 LevenshteinDistance(this string source, string target) {
            if (source.Length == 0) { return target.Length; }
            if (target.Length == 0) { return source.Length; }
            var distance = 0;
            distance = source[source.Length - 1] == target[target.Length - 1] ? 0 : 1;
            return Math.Min(Math.Min(LevenshteinDistance(source.Substring(0, source.Length - 1), target) + 1,
                                     LevenshteinDistance(source, target.Substring(0, target.Length - 1))) + 1,
                                     LevenshteinDistance(source.Substring(0, source.Length - 1), target.Substring(0, target.Length - 1)) + distance);
        }

        /// <summary>
        /// Method to return the Normalized Levenshtein Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double NormalizedLevenshteinDistance(this string source, string target) {
            var unnormalizedLevenshteinDistance = source.LevenshteinDistance(target);
            return unnormalizedLevenshteinDistance - source.LevenshteinDistanceLowerBounds(target);
        }

        /// <summary>
        /// Method to return the upper bound on the Levenshtein Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Int32 LevenshteinDistanceUpperBounds(this string source, string target) {
            // If the two strings are the same length then the Hamming Distance is the upper bounds of the Levenshtien Distance.
            if (source.Length == target.Length) return source.HammingDistance(target);
            // Otherwise, the upper bound is the length of the longer string.
            else if (source.Length > target.Length) return source.Length;
            else if (target.Length > source.Length) return target.Length;
            return 9999;
        }

        /// <summary>
        /// Method to return the lower bound on the Levenshtein Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Int32 LevenshteinDistanceLowerBounds(this string source, string target) {

            // If the two strings are the same length then the lower bound is zero.
            return source.Length == target.Length ? 0 : Math.Abs(source.Length - target.Length);

        }

    }
}
