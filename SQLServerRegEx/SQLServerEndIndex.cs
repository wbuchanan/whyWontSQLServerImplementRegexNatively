using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerEndIndex {

        public SQLServerEndIndex() {
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static Int32 endIndex(String functionString, String columnString) {
            return columnString.LastIndexOf(functionString);
        }

    }

}