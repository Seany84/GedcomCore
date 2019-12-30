using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_JULN: = {Size=4:35}
     * [ <YEAR> | <MONTH> <YEAR> | <DAY> <MONTH> <YEAR> ]
     * 
     */

    public class DateJulian : DateCalendar
    {
        public DateJulian(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override Calendars UsedCalendar => Calendars.Julian;

        public override string DateString
        {
            get
            {
                if (Year != 0)
                {
                    if (Month != Month.Unknown)
                    {
                        if (Day != 0)
                        {
                            return string.Format("{0} {1} {2}", Day, EnumTagUtil.GetFirstTagName(Month), Year);
                        }
                        else
                        {
                            return string.Format("{0} {1}", EnumTagUtil.GetFirstTagName(Month), Year);
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
                    Reporting.Warn("julian date doesn't have proper format: " + value);
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
                        Month = (Month)EnumTagUtil.SelectMember(typeof(Month), words[monthIndex], Month);
                    }
                    catch
                    {
                        throw new FormatException("unknown gregorian month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    Year = int.Parse(words[yearIndex]);
                }
            }
        }

        public int Day { get; set; }

        public Month Month { get; set; }

        public int Year { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dj = obj as DateJulian;
            if (dj == null)
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Day, dj.Day))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Month, dj.Month))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Year, dj.Year))
            {
                return false;
            }

            return true;
        }
    }
}
