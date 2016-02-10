using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerNullOrEmpty {

        public SQLServerNullOrEmpty() {
        }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static Int32 nullOrEmpty(String columnString) {
            return String.IsNullOrEmpty(columnString) ? 1 : 0;
        }

    }

}