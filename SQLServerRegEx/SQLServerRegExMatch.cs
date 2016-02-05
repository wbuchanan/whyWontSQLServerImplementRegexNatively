using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;


public class SQLServerRegexMatch {

    public Regex regex;

    public SQLServerRegexMatch(RegexOptions regexopts, String pattern) {
        this.regex = new Regex(pattern, regexopts);
    }

    // Method returning a boolean indicating whether the String matched the regular expression
    // returns 1 if true and 0 if false
    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public Int32 regexMatch(String columnString) {
        return this.regex.IsMatch(columnString) ? 1 : 0;
    }

}

