using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    /*
     * DATE: = {Size=4:35}
     * [ <DATE_CALENDAR_ESCAPE> | <NULL>]
     * <DATE_CALENDAR> 
     * 
     */

    public class Date : DateValue
    {
        public Date(Reporting Reporting)
            : base(Reporting)
        {
        }

        public Date(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override string DateString
        {
            get => EnumTagUtil.GetFirstTagName(Calendar) + " " + DateCalendar.DateString;
            set
            {
                value = value.Trim();

                if (value.Contains("FROM", StringComparison.OrdinalIgnoreCase) || value.Contains("TO", StringComparison.OrdinalIgnoreCase))
                {
                    throw new FormatException($"{value} appears to be a date of type DatePeriod");
                }

                var date = "";

                try
                {
                    var cal = value.Substring(0, value.IndexOf(' '));

                    if (EnumTagUtil.HasMember(typeof(Calendars), cal, Calendar))
                    // if the first part really was a calendar-string
                    {
                        date = value.Substring(value.IndexOf(' ') + 1);
                        Calendar = (Calendars)EnumTagUtil.SelectMember(typeof(Calendars), cal, Calendar);
                    }
                    else
                    {
                        date = value;
                        Calendar = Calendars.Unknown;
                    }
                }
                catch
                {
                    date = value;
                }
                finally
                {
                    DateCalendar = createDateCalendar(date);
                    Calendar = DateCalendar.UsedCalendar;
                }
            }
        }

        public DateCalendar DateCalendar { get; set; }

        public Calendars Calendar { get; set; }

        private DateCalendar createDateCalendar(string DateCalendarText)
        {
            switch (Calendar)
            {
                case Calendars.French:
                    return new DateFrench(DateCalendarText, Reporting);

                case Calendars.Gregorian:
                    return new DateGregorian(DateCalendarText, Reporting);

                case Calendars.Hebrew:
                    return new DateHebrew(DateCalendarText, Reporting);

                case Calendars.Julian:
                    return new DateJulian(DateCalendarText, Reporting);

                case Calendars.Roman:
                    Reporting.Warn("Roman Calendar is reserved for future use. Trying to guess");
                    return DateCalendar.GuessCalendarType(DateCalendarText, Reporting);

                case Calendars.Unknown:
                default:
                    return DateCalendar.GuessCalendarType(DateCalendarText, Reporting);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateCalendar dc))
            {
                return false;
            }

            if (!CompareObjects(DateCalendar, dc))
            {
                return false;
            }

            if (!CompareObjects(Calendar, dc.UsedCalendar))
            {
                return false;
            }

            return true;
        }
    }
}
