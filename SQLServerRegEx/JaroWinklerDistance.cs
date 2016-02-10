using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to return the Jaro-Winkler Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double JaroWinklerDistance(this string source, string target) {
            var jaroDistance = source.JaroDistance(target);
            var commonPrefixLength = CommonPrefixLength(source, target);
            return jaroDistance + (commonPrefixLength * 0.1 * (1 - jaroDistance));
        }

        /// <summary>
        /// Method to return the Jaro-Winkler Distance Coefficient with adjustment for the prefix.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <param name="p">The prefix scale to use</param>
        /// <returns>A Double valued metric</returns>
        public static Double JaroWinklerDistanceWithPrefixScale(this string source, string target, double p) {
            var prefixScale = 0.1;
            prefixScale = p > 0.25 ? 0.25 : p < 0 ? 0 : p;
            var jaroDistance = source.JaroDistance(target);
            var commonPrefixLength = CommonPrefixLength(source, target);
            return jaroDistance + (commonPrefixLength * prefixScale * (1 - jaroDistance));
        }

        /// <summary>
        /// Method to return the common prefix length used by the Jaro-Winkler methods in this class.
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double CommonPrefixLength(string source, string target) {
            var maximumPrefixLength = 4;
            var commonPrefixLength = 0;
            if (source.Length <= 4 || target.Length <= 4) { maximumPrefixLength = Math.Min(source.Length, target.Length); }
            for (int i = 0; i < maximumPrefixLength; i++) {
                if (source[i].Equals(target[i])) { commonPrefixLength++; }
                else { return commonPrefixLength; }
            }
            return commonPrefixLength;
        }

    }

}
