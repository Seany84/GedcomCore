using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_PHRASE: = {Size=1:35}
     * (<TEXT>)
     * Any statement offered as a date when the year is not recognizable to a date parser, but which gives information about when an event occurred. The date phrase is enclosed in matching parentheses. 
     * 
     */
    public class DatePhrase : DateValue
    {
        public DatePhrase(string Phrase, Reporting Reporting)
            : base(Reporting)
        {
            DateString = Phrase;
        }

        public override string DateString
        {
            get => "";
            set => Phrase = value;
        }

        public string Phrase { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var dp = obj as DatePhrase;
            if (dp == null)
            {
                return false;
            }

            if (!CompareObjects(Phrase, dp.Phrase))
            {
                return false;
            }

            return true;
        }
    }
}
