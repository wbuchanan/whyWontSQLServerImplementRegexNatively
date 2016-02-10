using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;

/// <summary>
/// The name space used to distinguish Date/Time functions from regular Expression functions
/// </summary>
namespace SQLServerDateTime {

    /// <summary>
    /// Class used for DateTime CLR functions
    /// </summary>
    public class SQLServerDateTime {

        /// <summary>
        /// DateMath object initialized and used to call non-static methods from a static interface
        /// </summary>
        public static readonly DateMath dm = new DateMath();

        /// <summary>
        /// Method to estimate the difference between two dates
        /// </summary>
        /// <param name="from">The starting date</param>
        /// <param name="to">The ending date</param>
        /// <param name="type">A String value indicating to which level the type should be returned</param>
        /// <returns>The double value for the corresponding type (e.g., includes partial units)</returns>
        public static SqlDouble decimalDifference(SqlDateTime from, SqlDateTime to, SqlString type) {
            var dates = convertSqlDates(from, to);
            return new SqlDouble(DateMath.decimalDifference(dates[0], dates[1], checkType(type)));
        }

        /// <summary>
        /// Method to estimate the difference between two dates
        /// </summary>
        /// <param name="from">The starting date</param>
        /// <param name="to">The ending date</param>
        /// <param name="type">A String value indicating to which level the type should be returned</param>
        /// <returns>The integer value for the corresponding type</returns>
        public static SqlDouble intervalDifference(SqlDateTime from, SqlDateTime to, SqlString type) {
            var dates = convertSqlDates(from, to);
            return new SqlDouble(DateMath.integerDifference(dates[0], dates[1], checkType(type)));
        }

        /// <summary>
        /// Method to estimate the interval between two dates.  This is different from the difference in
        /// that it will include the to value as part of the estimation.
        /// </summary>
        /// <param name="from">The starting date</param>
        /// <param name="to">The ending date</param>
        /// <param name="type">A String value indicating to which level the type should be returned</param>
        /// <returns>The integer value for the corresponding type</returns>
        public static SqlDouble decimalInterval(SqlDateTime from, SqlDateTime to, SqlString type) {
            var dates = convertSqlDates(from, to);
            return new SqlDouble(DateMath.decimalDifference(dates[0], dates[1], checkType(type)));
        }

        /// <summary>
        /// Method to estimate the interval between two dates.  This is different from the difference in
        /// that it will include the to value as part of the estimation.
        /// </summary>
        /// <param name="from">The starting date</param>
        /// <param name="to">The ending date</param>
        /// <param name="type">A String value indicating to which level the type should be returned</param>
        /// <returns>The integer value for the corresponding type</returns>
        public static SqlDouble intervalInterval(SqlDateTime from, SqlDateTime to, SqlString type) {
            var dates = convertSqlDates(from, to);
            return new SqlDouble(DateMath.integerDifference(dates[0], dates[1], checkType(type)));
        }

        /// <summary>
        /// Convenience method used to translate SqlDateTime types to DateTime types
        /// </summary>
        /// <param name="from">A SqlDateTime value passed from a method with the same parameter name</param>
        /// <param name="to">A SqlDateTime value passed from a method with the same parameter name</param>
        /// <returns>A two element array of DateTime values where the first element is the
        /// translated value of the from parameter and the second element is the translated value of the
        /// to parameter.</returns>
        public static DateTime[] convertSqlDates(SqlDateTime from, SqlDateTime to) {
            var dates = new DateTime[2];
            dates[0] = (DateTime)from;
            dates[1] = (DateTime)to;
            return dates;
        }

        /// <summary>
        /// Method used to check for valid type arguments for class methods
        /// </summary>
        /// <param name="type">A SqlString value</param>
        /// <returns>A valid form of the type or the default value (days)</returns>
        public static String checkType(SqlString type) {
            var typeArg = type.ToString().ToLower();
            switch(typeArg) {
                case "d":
                case "day":
                case "days":
                    return "days";
                case "h":
                case "hour":
                case "hours":
                    return "hours";
                case "m":
                case "min":
                case "minute":
                case "minutes":
                    return "minutes";
                case "s":
                case "sec":
                case "second":
                case "seconds":
                    return "seconds";
                case "mil":
                case "milli":
                case "millisecond":
                case "milliseconds":
                    return "milliseconds";
                default:
                    return "days";
            } // End of Switch statement

        } // End of Method declaration

    } // End of class declaration

} // End of namespace declaration


