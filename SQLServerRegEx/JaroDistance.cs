using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to estimate the Jaro Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaro%E2%80%93Winkler_distance
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double JaroDistance(this string source, string target) {
            var m = source.Intersect(target).Count();
            if (m == 0) return 0;
            else {
                var sourceTargetIntersetAsString = "";
                var targetSourceIntersetAsString = "";
                var sourceIntersectTarget = source.Intersect(target);
                var targetIntersectSource = target.Intersect(source);
                foreach (char character in sourceIntersectTarget) { sourceTargetIntersetAsString += character; }
                foreach (char character in targetIntersectSource) { targetSourceIntersetAsString += character; }
                var t = sourceTargetIntersetAsString.LevenshteinDistance(targetSourceIntersetAsString) / 2;
                return ((m / source.Length) + (m / target.Length) + ((m - t) / m)) / 3;
            }
        }

    }

}
