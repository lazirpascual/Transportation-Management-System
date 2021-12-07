/* -- FILEHEADER COMMENT --
    FILE		:	ValueConverter.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the StatusConverter class.
*/

using System;
using System.Windows.Data;

namespace Transportation_Management_System
{
    ///
    /// \class StatusConverter
    ///
    /// \brief The purpose of this class is to convert the status of an Order, from 0-1 to Active-Completed and vice versa.
    /// This is used when displaying the status value, where it is binded to a GridViewColumn to make the convertion.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return "Active";

                case 1:
                    return "Completed";

                default:
                    return "Active";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "Active":
                    return 0;

                case "Completed":
                    return 1;

                default:
                    return 0;
            }
        }
    }

    ///
    /// \class DateConverter
    ///
    /// \brief The purpose of this class is to convert the date of an Order, from the default null value to N/A and vice versa.
    /// This is used when displaying the date value, where it is binded to a GridViewColumn to make the convertion.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class DateConverter : IValueConverter
    {
        private static readonly DateTime dateTime = new DateTime(DateTime.MinValue.ToOADate());
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            const string date = DateTime.MinValue.ToString();
            switch (value.ToString())
            {
                //case "1/1/0001 12:00:00 AM":
                case date:
                    return "N/A";

                default:
                    return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "N/A":
                    return null;

                default:
                    return value;
            }
        }
    }

    ///
    /// \class QuantityConverter
    ///
    /// \brief The purpose of this class is to convert the quantity of an Order, from 0 to N/A and vice versa.
    /// This is used when displaying the quantity value, where it is binded to a GridViewColumn to make the convertion.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class QuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case 0:
                    return "N/A";
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "N/A":
                    return 0;
                default:
                    return value;
            }
        }
    }

    ///
    /// \class DirectionConverter
    ///
    /// \brief The purpose of this class is to convert the direction of a Route, from a null value to END and vice versa.
    /// This is used when displaying the direction, where it is binded to a GridViewColumn to make the convertion.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class DirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "Null":
                    return "END";
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "END":
                    return "Null";
                default:
                    return value;
            }
        }
    }

    ///
    /// \class TimeConverter
    ///
    /// \brief The purpose of this class is to convert the total time of a Route, from 0 to N/A and vice versa.
    /// This is used when displaying the time, where it is binded to a GridViewColumn to make the convertion.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case 0.0:
                    return "N/A";
                default:
                    return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                System.Globalization.CultureInfo culture)
        {
            switch (value)
            {
                case "N/A":
                    return 0;
                default:
                    return value;
            }
        }
    }
}
