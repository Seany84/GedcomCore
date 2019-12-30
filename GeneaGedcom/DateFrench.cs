using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_FREN: = {Size=4:35}
     * [ <YEAR> | <MONTH_FREN> <YEAR> | <DAY> <MONTH_FREN> <YEAR> ]
     * See <MONTH_FREN> 
     * 
     */

    public class DateFrench : DateCalendar
    {
        private int day;
        private MonthFrench month;
        private int year;

        public DateFrench(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;
        }

        public override Calendars UsedCalendar => Calendars.French;

        public override string DateString
        {
            get
            {
                if (Year != 0)
                {
                    if (Month != MonthFrench.Unknown)
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
                    Reporting.Warn("french date doesn't have proper format: " + value);
                }

                var dayIndex = words.Length - 3;
                var monthIndex = words.Length - 2;
                var yearIndex = words.Length - 1;

                if (dayIndex >= 0)
                {
                    day = int.Parse(words[dayIndex]);
                }

                if (monthIndex >= 0)
                {
                    try
                    {
                        month = (MonthFrench)EnumTagUtil.SelectMember(typeof(MonthFrench), words[monthIndex], month);
                    }
                    catch
                    {
                        throw new FormatException("unknown french month: " + words[monthIndex]);
                    }
                }

                if (yearIndex >= 0)
                {
                    year = int.Parse(words[yearIndex]);
                }
            }
        }

        public int Day
        {
            get => day;
            set => day = value;
        }

        public MonthFrench Month
        {
            get => month;
            set => month = value;
        }

        public int Year
        {
            get => year;
            set => year = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var df = obj as DateFrench;
            if (df == null)
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Day, df.Day))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Month, df.Month))
            {
                return false;
            }

            if (!GedcomLine.CompareObjects(Year, df.Year))
            {
                return false;
            }

            return true;
        }

    }
}
