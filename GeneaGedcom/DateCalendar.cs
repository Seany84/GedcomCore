using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_CALENDAR: = {Size=4:35}
     * [ <DATE_GREG> | <DATE_JULN> | <DATE_HEBR> | <DATE_FREN> |
     * <DATE_FUTURE> ]
     * 
     * The selection is based on the <DATE_CALENDAR_ESCAPE> that precedes the <DATE_CALENDAR> value immediately to the left. If <DATE_CALENDAR_ESCAPE> doesn't appear at this point, then @#DGREGORIAN@ is assumed. No future calendar types will use words (e.g., month names) from this list: FROM, TO, BEF, AFT, BET, AND, ABT, EST, CAL, or INT. When only a day and month appears as a DATE value it is considered a date phrase and not a valid date form.
     * 
     *   Date Escape    Syntax Selected
     *   -----------    -------------
     *   @#DGREGORIAN@  <DATE_GREG>
     *   @#DJULIAN@     <DATE_JULN>
     *   @#DHEBREW@     <DATE_HEBR>
     *   @#DFRENCH R@   <DATE_FREN>
     *   @#DROMAN@      for future definition
     *   @#DUNKNOWN@    calendar not known
     * 
     */

    public abstract class DateCalendar : Date
    {
        protected DateCalendar(Reporting Reporting)
            : base(Reporting)
        {
        }

        public abstract Calendars UsedCalendar
        {
            get;
        }

        public static DateCalendar GuessCalendarType(string DateCalendarText, Reporting Reporting)
        {
            var mf = MonthFrench.Brumaire;
            var m = Month.April;
            var mh = MonthHebrew.Adar;

            if (guessMonth(DateCalendarText, typeof(MonthFrench), mf))
            {
                return new DateFrench(DateCalendarText, Reporting);
            }

            if (guessMonth(DateCalendarText, typeof(Month), m))
            {
                return new DateGregorian(DateCalendarText, Reporting);
            }

            if (guessMonth(DateCalendarText, typeof(MonthHebrew), mh))
            {
                return new DateHebrew(DateCalendarText, Reporting);
            }

            Reporting.Warn("couldn't find any suitable calendar. falling back to default: julian");
            return new DateJulian(DateCalendarText, Reporting);
        }

        private static bool guessMonth(string DateCalendarText, Type MonthType, ValueType obj)
        {
            string month;

            var words = DateCalendarText.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 3)
            {
                month = words[1];
            }
            else if (words.Length == 2)
            {
                month = words[0];
            }
            else
            {
                month = words[0];
            }

            try
            {
                EnumTagUtil.SelectMember(MonthType, month, obj);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
