using System;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    public abstract class DateFuture : DateCalendar
    {
        protected DateFuture(Reporting Reporting)
            : base(Reporting)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is DateFuture date))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            return true;
        }
    }
}
