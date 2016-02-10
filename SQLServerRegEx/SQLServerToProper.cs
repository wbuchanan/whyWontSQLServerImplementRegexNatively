using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Globalization;

namespace SQLServerRegEx {

    public class SQLServerToProper {

        public SQLServerToProper() { }

        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static String toProper(String columnString) {

            // Creates a TextInfo object from which the method will apply the casing rules
            var myTI = new CultureInfo("en-US", false).TextInfo;

            // Constructs the proper cased string
            var retval = myTI.ToTitleCase(myTI.ToLower(columnString));

            // Returns the String to the caller
            return retval;

        } // End of Method declaration

    }

}