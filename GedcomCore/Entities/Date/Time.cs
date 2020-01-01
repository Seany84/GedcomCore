using System;
using System.Text.RegularExpressions;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
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

                if (Seconds != 0)
                {
                    if (SecondFractions != 0)
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
                    throw new FormatException($"unsupported time-format: {value}");
                }
            }
        }

        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public int SecondFractions { get; set; }

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
