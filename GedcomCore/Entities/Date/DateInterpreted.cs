using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    /*
     * INT <DATE> (<DATE_PHRASE>) | 
     * 
     */ 

    public class DateInterpreted : DateValue
    {
        private const string interpret = "INT ";
        private const string parantheseOpen = " (";
        private const string parantheseClose = ")";
        private int interpretIndex;
        private int parantheseOpenIndex;
        private int parantheseCloseIndex;

        public DateInterpreted(string DateString, Reporting Reporting)
            : base(Reporting)
        {
            this.DateString = DateString;

            Tag = "INT";
        }

        public override string DateString
        {
            get => "";
            set
            {
                interpretIndex = value.IndexOf(interpret);
                parantheseOpenIndex = value.IndexOf(parantheseOpen);
                parantheseCloseIndex = value.IndexOf(parantheseClose);

                if ((interpretIndex == -1) || (parantheseOpenIndex == -1) || (parantheseCloseIndex == -1))
                {
                    throw new FormatException("not a interpreted date");
                }

                Date = new Date(value.Substring(interpretIndex + interpret.Length, parantheseOpenIndex), Reporting);
                Interpretation = new DatePhrase(value.Substring(parantheseOpenIndex + 1, parantheseCloseIndex), Reporting);
            }
        }

        public Date Date { get; set; }

        public DatePhrase Interpretation { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateInterpreted di))
            {
                return false;
            }

            if (!CompareObjects(Date, di.Date))
            {
                return false;
            }

            if (!CompareObjects(Interpretation, di.Interpretation))
            {
                return false;
            }

            return true;
        }
    }
}
