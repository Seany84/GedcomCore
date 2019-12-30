using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public class DateExactTime : GedcomLine
    {
        private DateExact date;
        private Time time;

        public DateExactTime(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string DateString
        {
            get
            {
                return date.DateString;
            }
            set
            {
                date = DateExact.Parse(value, Reporting);
            }
        }

        [Tag("TIME")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Time Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            DateExactTime de = obj as DateExactTime;
            if (de == null)
            {
                return false;
            }

            if (!CompareObjects(date, de.date))
            {
                return false;
            }

            if (!CompareObjects(Time, de.Time))
            {
                return false;
            }

            return true;
        }

    }
}
