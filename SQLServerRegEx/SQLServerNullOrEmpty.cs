using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class SQLServerNullOrEmpty {

    public SQLServerNullOrEmpty() { 
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public Int32 nullOrEmpty(String columnString) {
        return String.IsNullOrEmpty(columnString) ? 1 : 0;
    }

}
