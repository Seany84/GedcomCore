using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GeneaGedcom
{
    /*
     * YEAR_GREG: = {Size=3:7}
     * [ <NUMBER> | <NUMBER>/<DIGIT><DIGIT> ]
     * The slash "/" <DIGIT><DIGIT> a year modifier which shows the possible date alternatives for pre-1752 date brought about by a changing the beginning of the year from MAR to JAN in the English calendar change of 1752, for example, 15 APR 1699/00. A (B.C.) appended to the <YEAR> indicates a date before the birth of Christ.
     * 
     * 
     */

    public class YearGregorian
    {
        private int year;

        private int alternative;

        private bool hasAlternative;

        public YearGregorian(int Year)
        {
            year = Year;
            hasAlternative = false;
        }

        public YearGregorian(string YearString)
        {
            this.YearString = YearString;
        }

        public bool IsUnknown
        {
            get
            {
                return (Year == 0) && !HasAlternative;
            }
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        public int Alternative
        {
            get
            {
                return alternative;
            }
            set
            {
                alternative = value;
            }
        }

        public bool HasAlternative
        {
            get
            {
                return hasAlternative;
            }
            set
            {
                hasAlternative = value;
            }
        }

        public string YearString
        {
            get
            {
                if (HasAlternative)
                {
                    return string.Format("{0}/{1:D2}", Year, Alternative);
                }
                else
                {
                    return Year.ToString();
                }
            }
            set
            {
                if (int.TryParse(value, out year))
                {
                    HasAlternative = false;
                    Alternative = -1;
                }
                else
                {
                    Regex regex = new Regex(@"^(\d+)/(\d\d)$");

                    Match match = regex.Match(value);

                    if (match.Success)
                    {
                        year = int.Parse(match.Groups[1].Value);
                        alternative = int.Parse(match.Groups[2].Value);
                        HasAlternative = true;
                    }
                    else
                    {
                        throw new FormatException("DateString didn't have a proper format");
                    }
                }
            }
        }

        public override string ToString()
        {
            return YearString;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            YearGregorian year = obj as YearGregorian;
            if (year == null)
            {
                return false;
            }

            if (!Year.Equals(year.Year))
            {
                return false;
            }

            if (!Alternative.Equals(year.Alternative))
            {
                return false;
            }

            return true;
        }

        public bool Equals(int Year)
        {
            string str = string.Format("{0:D4}", this.Year);
            string century = str.Substring(0, 2);
            string rest1 = str.Substring(2, 2);
            string rest2 = string.Format("{0:D2}", Alternative);

            string str2 = string.Format("{0:D4}", Year);
            string century2 = str2.Substring(0, 2);
            string rest = str2.Substring(2, 2);

            if (!century.Equals(century2))
            {
                return false;
            }

            if (rest.Equals(rest1) || rest.Equals(rest2))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
