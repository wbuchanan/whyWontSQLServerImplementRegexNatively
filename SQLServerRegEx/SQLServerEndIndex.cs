using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class SQLServerEndIndex {

    public SQLServerEndIndex() {
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public Int32 endIndex(String functionString, String columnString) {
        return columnString.LastIndexOf(functionString);
    }

}
