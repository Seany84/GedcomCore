using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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

            DateFuture date = obj as DateFuture;
            if (date == null)
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
