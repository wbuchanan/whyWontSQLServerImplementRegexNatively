using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

public partial class SQLServerToProper {

    public SQLServerToProper() { }

    [SqlFunction(IsDeterministic = true, IsPrecise = true)]
    public String toProper(String columnString) {

        // Creates a TextInfo object from which the method will apply the casing rules
        TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

        // Constructs the proper cased string        
        String retval = myTI.ToTitleCase(myTI.ToLower(columnString));

        // Returns the String to the caller
        return retval;

    } // End of Method declaration

}
