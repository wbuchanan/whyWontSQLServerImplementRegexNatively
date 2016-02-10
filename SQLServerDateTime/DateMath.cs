using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Namespace used for Date/Time CLR Functions/Calculations
/// </summary>
namespace SQLServerDateTime {

    /// <summary>
    /// Class used for Date Arithmetic
    /// </summary>
    public class DateMath {

        /// <summary>
        /// A Static time span equal to 1 day
        /// </summary>
        public static readonly TimeSpan day = new TimeSpan(1, 0, 0, 0, 0);

        /// <summary>
        /// A Static time span equal to 1 hour
        /// </summary>
        public static readonly TimeSpan hour = new TimeSpan(0, 1, 0, 0, 0);

        /// <summary>
        /// A Static time span equal to 1 minute
        /// </summary>
        public static readonly TimeSpan minute = new TimeSpan(0, 0, 1, 0, 0);

        /// <summary>
        /// A Static time span equal to 1 second
        /// </summary>
        public static readonly TimeSpan second = new TimeSpan(0, 0, 0, 1, 0);

        /// <summary>
        /// A Static time span equal to 1 millisecond
        /// </summary>
        public static readonly TimeSpan millisecond = new TimeSpan(0, 0, 0, 0, 1);

        /// <summary>
        /// Default class constructor
        /// </summary>
        public DateMath() { }

        /// <summary>
        /// Returns the decimal (e.g., fractional) valued difference in time between two days
        /// </summary>
        /// <param name="from">The DateTime value representing the starting point in time</param>
        /// <param name="to">The DateTime value representing the ending point in time</param>
        /// <param name="type">A String indicating to what level the difference should be estimated</param>
        /// <returns>Returns a double precision floating point value containing the difference</returns>
        public static Double decimalDifference(DateTime from, DateTime to, String type) {
            switch(type) {
                case "days":
                    return (to - from).TotalDays;
                case "hours":
                    return (to - from).TotalHours;
                case "minutes":
                    return (to - from).TotalMinutes;
                case "seconds":
                    return (to - from).TotalSeconds;
                case "milliseconds":
                    return (to - from).TotalMilliseconds;
                default:
                    return (to - from).TotalDays;
            }
        }

        /// <summary>
        /// Returns the integer valued difference in time between two days
        /// </summary>
        /// <param name="from">The DateTime value representing the starting point in time</param>
        /// <param name="to">The DateTime value representing the ending point in time</param>
        /// <param name="type">A String indicating to what level the difference should be estimated</param>
        /// <returns>Returns a double precision floating point value containing the integer difference.  This is done to
        /// prevent any overflow errors caused by differences of milliseconds across large periods of time.</returns>
        public static Double integerDifference(DateTime from, DateTime to, String type) {
            switch (type) {
                case "days":
                    return (to - from).Days;
                case "hours":
                    return (to - from).Hours;
                case "minutes":
                    return (to - from).Minutes;
                case "seconds":
                    return (to - from).Seconds;
                case "milliseconds":
                    return (to - from).Milliseconds;
                default:
                    return (to - from).Days;
            }
        }

        /// <summary>
        /// Returns the fractional valued interval between two days.  This method returns a slightly different result than the
        /// difference method because it is inclusive of the end date.
        /// </summary>
        /// <param name="from">The DateTime value representing the starting point in time</param>
        /// <param name="to">The DateTime value representing the ending point in time</param>
        /// <param name="type">A String indicating to what level the difference should be estimated</param>
        /// <returns>Returns a double precision floating point value containing the integer difference.  This is done to
        /// prevent any overflow errors caused by differences of milliseconds across large periods of time.</returns>
        public static Double decimalInterval(DateTime from, DateTime to, String type) {
            switch (type) {
                case "days":
                    return ((to - from) + day).TotalDays;
                case "hours":
                    return ((to - from) + hour).TotalHours;
                case "minutes":
                    return ((to - from) + minute).TotalMinutes;
                case "seconds":
                    return ((to - from) + second).TotalSeconds;
                case "milliseconds":
                    return ((to - from) + millisecond).TotalMilliseconds;
                default:
                    return ((to - from) + day).TotalDays;
            }
        }

        /// <summary>
        /// Returns the integer valued interval between two days.  This method returns a slightly different result than the
        /// difference method because it is inclusive of the end date.
        /// </summary>
        /// <param name="from">The DateTime value representing the starting point in time</param>
        /// <param name="to">The DateTime value representing the ending point in time</param>
        /// <param name="type">A String indicating to what level the difference should be estimated</param>
        /// <returns>Returns a double precision floating point value containing the integer difference.  This is done to
        /// prevent any overflow errors caused by differences of milliseconds across large periods of time.</returns>
        public static Double integerInterval(DateTime from, DateTime to, String type) {
            switch (type) {
                case "days":
                    return ((to - from) + day).Days;
                case "hours":
                    return ((to - from) + hour).Hours;
                case "minutes":
                    return ((to - from) + minute).Minutes;
                case "seconds":
                    return ((to - from) + second).Seconds;
                case "milliseconds":
                    return ((to - from) + millisecond).Milliseconds;
                default:
                    return ((to - from) + day).Days;
            }

        }

    } // End of class declaration

} // End of Namespace declaration

