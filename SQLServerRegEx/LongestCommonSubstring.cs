using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FuzzyString {

    public static partial class ComparisonMetrics {

        public static string LongestCommonSubstring(this string source, string target) {
            if (String.IsNullOrEmpty(source) || String.IsNullOrEmpty(target)) return null;
            var L = new int[source.Length, target.Length];
            var maximumLength = 0;
            var lastSubsBegin = 0;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < source.Length; i++) {
                for (int j = 0; j < target.Length; j++) {
                    if (source[i] != target[j]) L[i, j] = 0;
                    else {
                        if ((i == 0) || (j == 0)) L[i, j] = 1;
                        else L[i, j] = 1 + L[i - 1, j - 1];
                        if (L[i, j] > maximumLength) {
                            maximumLength = L[i, j];
                            var thisSubsBegin = i - L[i, j] + 1;
                            //if the current LCS is the same as the last time this block ran
                            if (lastSubsBegin == thisSubsBegin) {
                                stringBuilder.Append(source[i]);
                            }
                            //this block resets the string builder if a different LCS is found
                            else {
                                lastSubsBegin = thisSubsBegin;
                                stringBuilder.Length = 0; //clear it
                                stringBuilder.Append(source.Substring(lastSubsBegin, (i + 1) - lastSubsBegin));
                            }
                        }
                    }

                }

            }
            return stringBuilder.ToString();
        } // End of Method declaration

    } // End of class declaration

}
