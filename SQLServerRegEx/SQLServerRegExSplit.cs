using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Text.RegularExpressions;

namespace SQLServerRegEx {

    public class SQLServerRegexSplit {

        public Regex regex;

        public SQLServerRegexSplit(RegexOptions regexopts, String pattern) {
            this.regex = new Regex(pattern, regexopts);
        }

        // Method returning a boolean indicating whether the String matched the regular expression
        // returns 1 if true and 0 if false
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public String regexSplit(String columnString) {
            return String.Join(", ", this.regex.Split(columnString));
        }

    }

}
