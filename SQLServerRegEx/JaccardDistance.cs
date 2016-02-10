using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to return the Jaccard Distance Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaccard_index
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double JaccardDistance(this string source, string target) {
            return 1 - source.JaccardIndex(target);
        }

        /// <summary>
        /// Method to return the Jaccard Similarity Coefficient.  Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaccard_index
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double JaccardIndex(this string source, string target) {
            return (Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Union(target).Count()));
        }

    }

}
