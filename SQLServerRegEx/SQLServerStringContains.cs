using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerStringContains {

        public SQLServerStringContains() { }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static Int32 stringContains(String functionString, String columnString) {
            return columnString.Contains(functionString) ? 1 : 0;
        }

    }

}


