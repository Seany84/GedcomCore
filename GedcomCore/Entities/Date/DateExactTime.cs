using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    public class DateExactTime : GedcomLine
    {
        private DateExact date;

        public DateExactTime(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string DateString
        {
            get => date.DateString;
            set => date = DateExact.Parse(value, Reporting);
        }

        [Tag("TIME")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Time Time { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateExactTime de))
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
