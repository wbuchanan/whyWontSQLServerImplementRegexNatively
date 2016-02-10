using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method that returns the Szymkiewicz-Simpson Coefficient.  Additional information is available at:
        /// https://en.wikipedia.org/wiki/Overlap_coefficient
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double OverlapCoefficient(this string source, string target) {
            return (Convert.ToDouble(source.Intersect(target).Count())) / Convert.ToDouble(Math.Min(source.Length, target.Length));
        }

    }

}
