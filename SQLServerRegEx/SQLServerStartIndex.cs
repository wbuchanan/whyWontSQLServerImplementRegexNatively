using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerStartIndex {

        public SQLServerStartIndex() {
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static Int32 startIndex(String functionString, String columnString) {
            return columnString.LastIndexOf(functionString);
        }

    }

}
