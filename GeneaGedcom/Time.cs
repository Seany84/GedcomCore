using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using System.Text.RegularExpressions;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * TIME_VALUE: = {Size=1:12}
     * [ hh:mm:ss.fs ]
     * The time of a specific event, usually a computer-timed event, where:
     * hh =hours on a 24-hour clock
     * mm =minutes
     * ss =seconds (optional)
     * fs =decimal fraction of a second (optional) 
     * 
     */ 

    public class Time : GedcomLine
    {
        private int hours;
        private int minutes;
        private int seconds;
        private int secondFractions;

        public Time(Reporting Reporting)
            : base(Reporting)
        {
        }


        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,12)]
        public string TimeValue
        {
            get
            {
                string formatString;

                if (seconds != 0)
                {
                    if (secondFractions != 0)
                    {
                        formatString = "{0}:{1}:{2}.{3}";
                    }
                    else
                    {
                        formatString = "{0}:{1}:{2}";
                    }
                }
                else
                {
                    formatString = "{0}:{1}";
                }

                return string.Format(formatString, Hours, Minutes, Seconds, SecondFractions);
                
            }
            set
            {
                var regex = new Regex(@"\s*(\d\d?):(\d\d?)(:(\d\d?)(\.(\d\d?))?)?");

                var match = regex.Match(value);

                if (match.Success)
                {
                    Hours = int.Parse(match.Groups[1].Value);
                    Minutes = int.Parse(match.Groups[2].Value);

                    if (match.Groups[4].Success)
                    {
                        Seconds = int.Parse(match.Groups[4].Value);
                    }

                    if (match.Groups[6].Success)
                    {
                        SecondFractions = int.Parse(match.Groups[6].Value);
                    }
                }
                else
                {
                    throw new FormatException(string.Format("unsupported time-format: {0}", value));
                }
            }
        }

        public int Hours
        {
            get
            {
                return hours;
            }
            set
            {
                hours = value;
            }
        }

        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                minutes = value;
            }
        }

        public int Seconds
        {
            get
            {
                return seconds;
            }
            set
            {
                seconds = value;
            }
        }

        public int SecondFractions
        {
            get
            {
                return secondFractions;
            }
            set
            {
                secondFractions = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var time = obj as Time;
            if (time == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Hours, time.Hours))
            {
                return false;
            }

            if (!CompareObjects(Minutes, time.Minutes))
            {
                return false;
            }

            if (!CompareObjects(Seconds, time.Seconds))
            {
                return false;
            }

            if (!CompareObjects(SecondFractions, time.SecondFractions))
            {
                return false;
            }

            return true;
        }
    }
}
