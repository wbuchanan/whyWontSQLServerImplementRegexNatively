using System;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Defines class to expose methods for regular expression capabilities for SQL Server 2008.
/// In addition to regular expression methods, there are also methods available for string
/// distance estimation
/// </summary>
namespace SQLServerRegEx
{

    public class SQLRegex
    {

        // Sets compiled and culture invariant options for all subsequent regex calls
        public static readonly RegexOptions opts = RegexOptions.CultureInvariant;

        SQLRegex() { }

        // Method returning a boolean indicating whether the String matched the regular expression
        // returns 1 if true and 0 if false
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlInt32 regexMatch(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var regex = new SQLServerRegexMatch(opts, compStrings.from);
            return new SqlInt32(regex.regexMatch(compStrings.to));
        }

        // Method for replacing instances of a match of a regular expression (functionString) in a String (columnString)
        // with instances of a new String (replaceWith)
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString regexReplace(SqlString replaceWith, SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString, replaceWith);
            var regex = new SQLServerRegexReplace(opts, compStrings.from);
            return new SqlString(regex.regexReplace(compStrings.replace, compStrings.to));
        }

        // Method to split a String (columnString) based on matches on a regular expression (functionString)
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString regexSplit(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var regex = new SQLServerRegexSplit(opts, compStrings.from);
            return new SqlString(regex.regexSplit(compStrings.to));
        }

        // Returns a boolean if the datum in the SQL column (columnString) contains the exact match
        // to the specified String (functionString)
        // returns 1 if true and 0 if false
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlInt32 stringContains(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var strcont = new SQLServerStringContains();
            return new SqlInt32(SQLServerStringContains.stringContains(compStrings.from, compStrings.to));
        }

        // Returns the 0-based index of the position in columnString where the
        // String functionString begins
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlInt32 startIndex(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var idx = new SQLServerStartIndex();
            return new SqlInt32(SQLServerStartIndex.startIndex(compStrings.from, compStrings.to));
        }

        // Returns the 0-based index of the position in columnString where the
        // String functionString ends
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlInt32 endIndex(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var idx = new SQLServerEndIndex();
            return new SqlInt32(SQLServerEndIndex.endIndex(compStrings.from, compStrings.to));
        }

        // Method indicating whether the String value contains null/empty only
        // returns 1 if true and 0 if false
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlInt32 nullOrEmpty(SqlString columnString)
        {
            var compString = new CompStrings(columnString);
            var sqlnull = new SQLServerNullOrEmpty();
            return new SqlInt32(SQLServerNullOrEmpty.nullOrEmpty(compString.col));
        }

        // Defines method that returns a list of indices of the positions of a String match
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString indexList(SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString);
            var idx = new SQLServerIndexList();
            return new SqlString(SQLServerIndexList.indexList(compStrings.from, compStrings.to));
        } // End of method declaration

        // Method to provide a direct replace implementation (similar to existing functionality in SQL)
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString stringReplace(SqlString replaceWith, SqlString functionString, SqlString columnString)
        {
            var compStrings = new CompStrings(functionString, columnString, replaceWith);
            var strrep = new SQLServerStringReplace(compStrings.from, compStrings.replace);
            return new SqlString(strrep.stringReplace(compStrings.to));
        } // End of Method declaration

        // Method used to return a String with proper case rules applied to it
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString toProper(SqlString columnString)
        {
            var compString = new CompStrings(columnString);
            var prop = new SQLServerToProper();
            return new SqlString(SQLServerToProper.toProper(compString.col.ToLower()));
        } // End of Method declaration

        // Method used to estimate the Levenshtein String Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble levenshtein(SqlString string1, SqlString string2)
        {
            var compStrings = new CompStrings(string1, string2);
            var sdist = new StringDist(compStrings.from, compStrings.to);
            return new SqlDouble(sdist.levenshtein());
        }

        // Method used to estimate the Hamming String Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble hamming(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.hamming());
        }

        // Method used to estimate the Jaro Winkler String Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble jaroWinkler(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.jaroWinkler());
        }

        // Method used to estimate the Jaro Winkler String Prefix Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble jaroWinklerPrefix(SqlString string1, SqlString string2, SqlDouble scale)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.jaroWinklerPrefix((double)scale));
        }

        // Method used to estimate the Upper Bound of the Levenshtein Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble levenshteinUpper(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.levenshteinUpper());
        }

        // Method used to estimate the Lower Bound of the Levenshtein Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble levenshteinLower(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.levenshteinLower());
        }

        // Method used to estimate the Normalized Levenshtein Distance
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble levenshteinNormalized(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.levenshteinNormalized());
        }

        // Method used to estimate the Tanimoto Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble tanimotoCoefficient(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.tanimotoCoefficient());
        }

        // Method used to estimate the Jaro String Distance
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble jaccard(SqlString string1, SqlString string2) {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.jaccard());
        }


        // Method used to estimate the Jaro String Distance
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble jaro(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.jaro());
        }

        // Method used to estimate the Sorensen String Index Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble sorensenIndex(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.sorensenIndex());
        }

        // Method used to estimate the Sorensen String Distance Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble sorensenDistance(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.sorensenDistance());
        }

        // Method used to estimate the Ratcliff Obershelp Similarity coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble ratcliff(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.ratcliff());
        }

        // Method used to estimate the Overlap Coefficient
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlDouble overlap(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlDouble(sdist.overlap());
        }

        // Method used to return the longest common substring between
        // a pair of strings
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString longestSubString(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlString(sdist.longestSubString());
        }

        // Method used to return the longest common subsequence of strings between
        // a pair of strings
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlString longestSubSequence(SqlString string1, SqlString string2)
        {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlString(sdist.longestSubSequence());
        }

        // Method used to estimate whether or not the two strings are an approximate match
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static SqlBoolean approximatelySame(SqlString string1, SqlString string2,
                                SqlString comparisonOptions, SqlString toleranceOptions)
        {
            var compString = new CompStrings(string1, string2,
                                          toleranceOptions, comparisonOptions);
            var sdist = new StringDist(compString.from, compString.to);
            return new SqlBoolean(sdist.approxSame(compString.comp, compString.tolerance));
        }

        /*
        [SqlFunction(IsDeterministic = true, IsPrecise = true, FillRowMethodName = "distanceMetricRecord")]
        public static IEnumerable allDistanceMetrics(SqlString string1, SqlString string2) {
            var compString = new CompStrings(string1, string2);
            var sdist = new StringDist(compString.from, compString.to);
            return sdist.allMetrics();
        }

        /// <summary>
        /// Method used to return a vector of string distance metrics
        /// </summary>
        /// <param name="obj">An object containing a dictionary of double values with the distance metrics</param>
        /// <param name="Hamming_Distance">The Haming Distance Coefficient</param>
        /// <param name="Jaccard_Distance">The Jaccard Distance Coefficient</param>
        /// <param name="Jaro_Distance">The Jaro Distance Coefficient</param>
        /// <param name="Jaro_Winkler_Distance">The Jaro-Winkler Distance Coefficient</param>
        /// <param name="Levenshtein_Distance">The Levenshtein Distance Coefficient</param>
        /// <param name="Levenshtein_Distance_Lower_Bound">The Lower Bound of the Levenshtein Distance Coefficient</param>
        /// <param name="Levenshtein_Distance_Upper_Bound">The Upper Bound of the Levenshtein Distance Coefficient</param>
        /// <param name="Normalized_Levenshtein_Distance">The normalized Levenshtein Distance Coefficient</param>
        /// <param name="Overlap_Coefficient">The Overlap Coefficient</param>
        /// <param name="Ratcliff_Obershelp_Similarity">The Ratcliff-Obershelp Similarity Coefficient</param>
        /// <param name="Sorensen_Dice_Index">The Sorensen Dice Index</param>
        /// <param name="Sorensen_Dice_Distance">The Sorensen Dice Distance Coefficient</param>
        /// <param name="Tanimoto_Coefficient">The Tanimoto Coefficient</param>
        public static void distanceMetricRecord(Object obj, out Double Hamming_Distance,
        out Double Jaccard_Distance, out Double Jaro_Distance, out Double Jaro_Winkler_Distance,
        out Double Levenshtein_Distance, out Double Levenshtein_Distance_Lower_Bound,
        out Double Levenshtein_Distance_Upper_Bound, out Double Normalized_Levenshtein_Distance,
        out Double Overlap_Coefficient, out Double Ratcliff_Obershelp_Similarity,
        out Double Sorensen_Dice_Index, out Double Sorensen_Dice_Distance,
        out Double Tanimoto_Coefficient) {

            var values = (AllDistanceMetrics)obj;
            
            Hamming_Distance = values.Hamming_Distance;
            Jaccard_Distance = values.Jaccard_Distance;
            Jaro_Distance = values.Jaro_Distance;
            Jaro_Winkler_Distance = values.Jaro_Winkler_Distance;
            Levenshtein_Distance = values.Levenshtein_Distance;
            Levenshtein_Distance_Lower_Bound = values.Levenshtein_Distance_Lower_Bound;
            Levenshtein_Distance_Upper_Bound = values.Levenshtein_Distance_Upper_Bound;
            Normalized_Levenshtein_Distance = values.Normalized_Levenshtein_Distance;
            Overlap_Coefficient = values.Overlap_Coefficient;
            Ratcliff_Obershelp_Similarity = values.Ratcliff_Obershelp_Similarity;
            Sorensen_Dice_Index = values.Sorensen_Dice_Index;
            Sorensen_Dice_Distance = values.Sorensen_Dice_Distance;
            Tanimoto_Coefficient = values.Tanimoto_Coefficient;

        }

        */

    } // End of class declaration

}