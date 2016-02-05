using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class SQLServerStartIndex {

    public SQLServerStartIndex() {
    }

    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public Int32 startIndex(String functionString, String columnString) {
        return columnString.LastIndexOf(functionString);
    }

}
