using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    /*
     * DATE_EXACT: = {Size=10:11}
     * <DAY> <MONTH> <YEAR_GREG>
     * 
     */
 
    public class DateExact : GedcomLine
    {
        public DateExact(Reporting Reporting)
            : base(Reporting)
        {
        }

        public DateExact(int Day, Month Month, YearGregorian Year, Reporting Reporting)
            : base(Reporting)
        {
            this.Day = Day;
            this.Month = Month;
            this.Year = Year;
        }

        public static DateExact Parse(string DateString, Reporting Reporting)
        {
            var date = new DateExact(Reporting);
            date.DateString = DateString;
            return date;
        }

        public int Day { get; set; }

        public Month Month { get; set; }

        public YearGregorian Year { get; set; }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string DateString
        {
            get
            {
                string str;

                if (Year.Equals(0))
                {
                    str = "";
                }
                else if (Month == Month.Unknown)
                {
                    str = Year.ToString();
                }
                else if (Day == 0)
                {
                    str = string.Format("{0} {1}", EnumTagUtil.GetFirstTagName(Month));
                }
                else
                {
                    str = $"{Day} {EnumTagUtil.GetFirstTagName(Month)} {Year}";
                }

                return str;
            }
            set
            {
                char[] splitChars = { ' ', '\t' };
                var words = value.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length != 3)
                {
                    Reporting.Warn("exact date doesn't have proper format: " + value);
                }

                if (words.Length == 3)
                {
                    Day = int.Parse(words[0]);
                }
                if (words.Length > 1)
                {
                    try
                    {
                        Month = (Month)EnumTagUtil.SelectMember(typeof(Month), words[1], Month);
                    }
                    catch
                    {
                        throw new FormatException("unknown gregorian month: " + words[1]);
                        //TODO: should we accept numbers instead of month names here, too?
                    }
                }

                Year = new YearGregorian(words[2]);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateExact de))
            {
                return false;
            }

            if (!CompareObjects(Day, de.Day))
            {
                return false;
            }

            if (!CompareObjects(Month, de.Month))
            {
                return false;
            }

            if (!CompareObjects(Year, de.Year))
            {
                return false;
            }

            return true;
        }
    }
}
