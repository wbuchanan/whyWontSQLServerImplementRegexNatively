using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

public class SQLServerRegexReplace {

    public Regex regex;

    public SQLServerRegexReplace(RegexOptions regexopts, String pattern) {
        this.regex = new Regex(pattern, regexopts);
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public String regexReplace(String replaceWith, String columnString) {
        return this.regex.Replace(columnString, replaceWith);
    }

}
