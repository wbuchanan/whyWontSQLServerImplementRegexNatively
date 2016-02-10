using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerStringReplace {

        public string oldString;
        public string newString;

        public SQLServerStringReplace(String replaceThis, String withThis) {
            this.oldString = replaceThis;
            this.newString = withThis;
        }

        public String stringReplace(String columnString) {
            return columnString.Replace(this.oldString, this.newString);
        }

    }

}
