using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    /* 
     * DATE_HEBR: = {Size=4:35}
     * [ <YEAR> | <MONTH_HEBR> <YEAR> | <DAY> <MONTH_HEBR> <YEAR> ]
     * See <MONTH_HEBR>. 
     * 
     */

    public class DateHebrew : DateCalendar
    {
        public DateHebrew(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override Calendars UsedCalendar => Calendars.Hebrew;

        public int Day { get; set; }

        public MonthHebrew Month { get; set; }

        public int Year { get; set; }

        public override string DateString
        {
            get
            {
                if (Year != 0)
                {
                    if (Month != MonthHebrew.Unknown)
                    {
                        if (Day != 0)
                        {
                            return $"{Day} {EnumTagUtil.GetFirstTagName(Month)} {Year}";
                        }
                        else
                        {
                            return $"{EnumTagUtil.GetFirstTagName(Month)} {Year}";
                        }
                    }
                    else
                    {
                        return Year.ToString();
                    }
                }
                else
                {
                    return "";
                }
            }
            set
            {
                char[] splitChars = { ' ', '\t' };
                var words = value.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 3)
                {
                    Reporting.Warn("hebrew date doesn't have proper format: " + value);
                }

                var dayIndex = words.Length - 3;
                var monthIndex = words.Length - 2;
                var yearIndex = words.Length - 1;

                if (dayIndex >= 0)
                {
                    Day = int.Parse(words[dayIndex]);
                }

                if (monthIndex >= 0)
                {
                    try
                    {
                        Month = (MonthHebrew)EnumTagUtil.SelectMember(typeof(MonthHebrew), words[monthIndex], Month);
                    }
                    catch
                    {
                        throw new FormatException("unknown hebrew month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    Year = int.Parse(words[yearIndex]);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateHebrew dh))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Day, dh.Day))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Month, dh.Month))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Year, dh.Year))
            {
                return false;
            }

            return true;
        }
    }
}
