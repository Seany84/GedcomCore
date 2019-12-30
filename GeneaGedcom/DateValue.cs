using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * DATE_VALUE: = {Size=1:35}
     * [
     * <DATE> |
     * <DATE_PERIOD> |
     * <DATE_RANGE>
     * <DATE_APPROXIMATED> |
     * INT <DATE> (<DATE_PHRASE>) |
     * (<DATE_PHRASE>)
     * ]
     * 
     * The DATE_VALUE represents the date of an activity, attribute, or event where:
     * INT =Interpreted from knowledge about the associated date phrase included in parentheses.
     * 
     * An acceptable alternative to the date phrase choice is to use one of the other choices such as <DATE_APPROXIMATED> choice as the DATE line value and then include the <DATE_PHRASE> as a NOTE value subordinate to the DATE line.
     * 
     * The date value can take on the date form of just a date, an approximated date, between a date and another date, and from one date to another date. The preferred form of showing date imprecision, is to show, for example, MAY 1890 rather than ABT 12 MAY 1890. This is because limits have not been assigned to the precision of the prefixes such as ABT or EST.
     * 
     */
 
    public abstract class DateValue : GedcomLine
    {
        protected DateValue(Reporting Reporting)
            : base(Reporting)
        {
        }

        public abstract string DateString
        {
            get;
            set;
        }

        [Tag("")]
        public string Datestring
        {
            get
            {
                return DateString;
            }
            set
            {
                DateString = value;
            }
        }

        public static DateValue CreateDateValue(string DateString, Reporting Reporting)
        {
            Type t = DateTypeSelector(DateString, Reporting);

            Type[] ctorTypes = new Type[] { DateString.GetType(), Reporting.GetType() };
            object[] ctorValues = new object[] { DateString, Reporting };

            return t.GetConstructor(ctorTypes).Invoke(ctorValues) as DateValue;
        }

        public static Type DateTypeSelector(string DateString, Reporting Reporting)
        {
            //TODO: optimize method; use at least a bit of heuristic ; )
            try
            {
                new Date(DateString, Reporting);
                return typeof(Date);
            }catch{}

            try
            {
                new DatePeriod(DateString, Reporting);
                return typeof(DatePeriod);
            }catch{}

            try
            {
                new DateRange(DateString, Reporting);
                return typeof(DateRange);
            }catch{}

            try
            {
                new DateApproximated(DateString, Reporting);
                return typeof(DateApproximated);
            }catch{}

            try
            {
                new DateInterpreted(DateString, Reporting);
                return typeof(DateInterpreted);
            }catch{}

            try
            {
                new DatePhrase(DateString, Reporting);
                return typeof(DatePhrase);
            }catch{}


            throw new FormatException("isn't a date value: " + DateString);
        }
    }
}
