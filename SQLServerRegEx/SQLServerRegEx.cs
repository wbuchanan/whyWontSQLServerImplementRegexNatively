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

public partial class SQLRegex {

    // Sets compiled and culture invariant options for all subsequent regex calls
    public static readonly RegexOptions opts = RegexOptions.CultureInvariant;

    SQLRegex() { }

    // Method returning a boolean indicating whether the String matched the regular expression
    // returns 1 if true and 0 if false
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlInt32 regexMatch(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerRegexMatch regex = new SQLServerRegexMatch(opts, compStrings.from);
        return new SqlInt32(regex.regexMatch(compStrings.to));
    }

    // Method for replacing instances of a match of a regular expression (functionString) in a String (columnString)
    // with instances of a new String (replaceWith)
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString regexReplace(SqlString replaceWith, SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString, replaceWith);
        SQLServerRegexReplace regex = new SQLServerRegexReplace(opts, compStrings.from);
        return new SqlString(regex.regexReplace(compStrings.replace, compStrings.to));
    }

    // Method to split a String (columnString) based on matches on a regular expression (functionString)
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString regexSplit(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerRegexSplit regex = new SQLServerRegexSplit(opts, compStrings.from);
        return new SqlString(regex.regexSplit(compStrings.to));
    }

    // Returns a boolean if the datum in the SQL column (columnString) contains the exact match
    // to the specified String (functionString)
    // returns 1 if true and 0 if false
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlInt32 stringContains(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerStringContains strcont = new SQLServerStringContains();
        return new SqlInt32(strcont.stringContains(compStrings.from, compStrings.to));
    }

    // Returns the 0-based index of the position in columnString where the 
    // String functionString begins
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlInt32 startIndex(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerStartIndex idx = new SQLServerStartIndex();
        return new SqlInt32(idx.startIndex(compStrings.from, compStrings.to));
    }

    // Returns the 0-based index of the position in columnString where the 
    // String functionString ends
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlInt32 endIndex(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerEndIndex idx = new SQLServerEndIndex();
        return new SqlInt32(idx.endIndex(compStrings.from, compStrings.to));
    }

    // Method indicating whether the String value contains null/empty only
    // returns 1 if true and 0 if false
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlInt32 nullOrEmpty(SqlString columnString) {
        CompStrings compString = new CompStrings(columnString);
        SQLServerNullOrEmpty sqlnull = new SQLServerNullOrEmpty();
        return new SqlInt32(sqlnull.nullOrEmpty(compString.col));
    }

    // Defines method that returns a list of indices of the positions of a String match
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString indexList(SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString);
        SQLServerIndexList idx = new SQLServerIndexList();
        return new SqlString(idx.indexList(compStrings.from, compStrings.to));
    } // End of method declaration

    // Method to provide a direct replace implementation (similar to existing functionality in SQL)
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString stringReplace(SqlString replaceWith, SqlString functionString, SqlString columnString) {
        CompStrings compStrings = new CompStrings(functionString, columnString, replaceWith);
        SQLServerStringReplace strrep = new SQLServerStringReplace(compStrings.from, compStrings.replace);
        return new SqlString(strrep.stringReplace(compStrings.to));
    } // End of Method declaration

    // Method used to return a String with proper case rules applied to it
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString toProper(SqlString columnString) {
        CompStrings compString = new CompStrings(columnString);
        SQLServerToProper prop = new SQLServerToProper();
        return new SqlString(prop.toProper(compString.col.ToLower()));
    } // End of Method declaration

    /*
    // Method used to estimate the Levenshtein String Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble levenshtein(SqlString string1, SqlString string2) {
        CompStrings compStrings = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compStrings.from, compStrings.to);
        return new SqlDouble(sdist.levenshtein());
    }

    // Method used to estimate the Hamming String Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble hamming(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.hamming());
    }

    // Method used to estimate the Jaro Winkler String Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble jaroWinkler(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.jaroWinkler());
    }

    // Method used to estimate the Jaro Winkler String Prefix Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble jaroWinklerPrefix(SqlString string1, SqlString string2, SqlDouble scale) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.jaroWinklerPrefix((double) scale));
    }

    // Method used to estimate the Upper Bound of the Levenshtein Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble levenshteinUpper(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.levenshteinUpper());
    }

    // Method used to estimate the Lower Bound of the Levenshtein Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble levenshteinLower(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.levenshteinLower());
    }

    // Method used to estimate the Normalized Levenshtein Distance
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble levenshteinNormalized(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.levenshteinNormalized());
    }

    // Method used to estimate the Tanimoto Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble tanimotoCoefficient(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.tanimotoCoefficient());
    }

    // Method used to estimate the Jaro String Distance
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble jaro(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.jaro());
    }

    // Method used to estimate the Sorensen String Index Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble sorensenIndex(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.sorensenIndex());
    }

    // Method used to estimate the Sorensen String Distance Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble sorensenDistance(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.sorensenDistance());
    }

    // Method used to estimate the Ratcliff Obershelp Similarity coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble ratcliff(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.ratcliff());
    }

    // Method used to estimate the Overlap Coefficient
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlDouble overlap(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlDouble(sdist.overlap());
    }

    // Method used to return the longest common substring between
    // a pair of strings
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString longestSubString(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlString(sdist.longestSubString());
    }

    // Method used to return the longest common subsequence of strings between
    // a pair of strings
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlString longestSubSequence(SqlString string1, SqlString string2) {
        CompStrings compString = new CompStrings(string1, string2);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlString(sdist.longestSubSequence());
    }

    // Method used to estimate whether or not the two strings are an approximate match
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public static SqlBoolean approximatelySame(SqlString string1, SqlString string2,
                            SqlString comparisonOptions, SqlString toleranceOptions) {
        CompStrings compString = new CompStrings(string1, string2,
                                      toleranceOptions, comparisonOptions);
        StringDist sdist = new StringDist(compString.from, compString.to);
        return new SqlBoolean(sdist.approxSame(compString.comp, compString.tolerance));
    }

    */

} // End of class declaration

