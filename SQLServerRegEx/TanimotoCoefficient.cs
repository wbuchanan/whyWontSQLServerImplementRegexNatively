using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to obtain the Tanimoto Distance Coefficient.
        /// Additional information available at:
        /// https://en.wikipedia.org/wiki/Jaccard_index#Tanimoto_coefficient_.28extended_Jaccard_coefficient.29
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double valued metric</returns>
        public static Double TanimotoCoefficient(this string source, string target) {
            double Na = source.Length;
            double Nb = target.Length;
            double Nc = source.Intersect(target).Count();
            return Nc / (Na + Nb - Nc);
        }

    }

}
