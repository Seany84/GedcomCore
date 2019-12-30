using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE: = {Size=4:35}
     * [ <DATE_CALENDAR_ESCAPE> | <NULL>]
     * <DATE_CALENDAR> 
     * 
     */

    public class Date : DateValue
    {
        private DateCalendar dateCalendar;
        private Calendars calendar;

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
            get
            {
                return EnumTagUtil.GetFirstTagName(calendar) + " " + dateCalendar.DateString;
            }
            set
            {
                value = value.Trim();

                string date = "";

                try
                {
                    string cal = value.Substring(0, value.IndexOf(' '));

                    if (EnumTagUtil.HasMember(typeof(Calendars), cal, calendar))
                    // if the first part really was a calendar-string
                    {
                        date = value.Substring(value.IndexOf(' ') + 1);
                        Calendar = (Calendars)EnumTagUtil.SelectMember(typeof(Calendars), cal, calendar);
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

        public DateCalendar DateCalendar
        {
            get
            {
                return dateCalendar;
            }
            set
            {
                dateCalendar = value;
            }
        }

        public Calendars Calendar
        {
            get
            {
                return calendar;
            }
            set
            {
                calendar = value;
            }
        }

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

            DateCalendar dc = obj as DateCalendar;
            if (dc == null)
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
