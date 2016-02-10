using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to return the Ratcliff Obershelp Similary Coefficient.  Additional information available at:
        /// http://www.morfoedro.it/doc.php?n=223&lang=en#RatcliffObershelp
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double RatcliffObershelpSimilarity(this string source, string target) {
            return (2 * Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Length + target.Length));
        }

    }

}
