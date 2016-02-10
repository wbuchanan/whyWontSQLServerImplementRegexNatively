using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString
{
    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to return the Hamming distance between two strings.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Hamming_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Int32 HammingDistance(this string source, string target) {
            var distance = 0;
            if (source.Length == target.Length) {
                for (int i = 0; i < source.Length; i++) {
                    if (!source[i].Equals(target[i]))  distance++;
                }
                return distance;
            }
            else return 99999;

        }

    }

}
