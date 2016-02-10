using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method that returns the Sorensen Dice Distance.  Additional information can be found at:
        /// http://en.wikipedia.org/wiki/S%C3%B8rensen%E2%80%93Dice_coefficient
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double value </returns>
        public static Double SorensenDiceDistance(this string source, string target) {
            return 1 - source.SorensenDiceIndex(target);
        }

        /// <summary>
        /// Method that returns the Sorensen Dice Distance.  Additional information can be found at:
        /// http://en.wikipedia.org/wiki/S%C3%B8rensen%E2%80%93Dice_coefficient
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A Double value </returns>
        public static Double SorensenDiceIndex(this string source, string target) {
            return (2 * Convert.ToDouble(source.Intersect(target).Count())) / (Convert.ToDouble(source.Length + target.Length));
        }

    }

}
