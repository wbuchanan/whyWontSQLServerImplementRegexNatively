using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        /// <summary>
        /// Method to return the longest common subsequence of characters
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>A string containing the longest common subsequence of characters</returns>
        public static string LongestCommonSubsequence(this string source, string target) {
            var C = LongestCommonSubsequenceLengthTable(source, target);
            return Backtrack(C, source, target, source.Length, target.Length);
        }

        /// <summary>
        /// Method used internally by the LongestCommonSubsequence method but exposed publicly in case anyone has a need for it.
        /// </summary>
        /// <param name="source">The string that will serve as the base for comparisons</param>
        /// <param name="target">The string that is being compared against the base</param>
        /// <returns>An array of 4-byte Integer values that form the tabled used to determine the longest common sub-sequence</returns>
        public static Int32[,] LongestCommonSubsequenceLengthTable(string source, string target) {
            var C = new int[source.Length + 1, target.Length + 1];
            for (int i = 0; i < source.Length + 1; i++) { C[i, 0] = 0; }
            for (int j = 0; j < target.Length + 1; j++) { C[0, j] = 0; }
            for (int i = 1; i < source.Length + 1; i++) {
                for (int j = 1; j < target.Length + 1; j++) {
                    if (source[i - 1].Equals(target[j - 1])) C[i, j] = C[i - 1, j - 1] + 1;
                    else C[i, j] = Math.Max(C[i, j - 1], C[i - 1, j]);
                }
            }
            return C;
        }


        private static string Backtrack(int[,] C, string source, string target, int i, int j) {
            return i == 0 || j == 0 ? "" : source[i - 1].Equals(target[j - 1]) ? Backtrack(C, source, target, i - 1, j - 1) + source[i - 1] : C[i, j - 1] > C[i - 1, j] ? Backtrack(C, source, target, i, j - 1) : Backtrack(C, source, target, i - 1, j);
        }

    }

}
