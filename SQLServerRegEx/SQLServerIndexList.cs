using System;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace SQLServerRegEx {

    public class SQLServerIndexList {

        public SQLServerIndexList() { }

        // Defines method that returns a list of indices of the positions of a String match
        [SqlFunction(IsDeterministic = true, IsPrecise = true)]
        public static String indexList(String functionString, String columnString) {

            // Defines the starting index value
            var start = SQLServerStartIndex.startIndex(functionString, columnString);

            // Defines the ending index value
            var end = SQLServerEndIndex.endIndex(functionString, columnString);

            // Initializes a String object to store the result that will be returned
            var retval = "";

            // Loops over the integers between the start and end value
            for (int i = start; i <= end; i++) {

                // For all but the final iteration append ', ' to the index value
                if (i != end) retval += i.ToString() + ", ";

                // For the last iteration just add the index value to the String
                else retval += i.ToString();

            } // End of Loop over the index integers

            // Return the String value to the caller
            return retval;

        } // End of method declaration

    }

}