using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /*
     * AGE_AT_EVENT: = {Size=1:12}
     * [ < | > | <NULL>]
     * [ YYy MMm DDDd | YYy | MMm | DDDd |
     * YYy MMm | YYy DDDd | MMm DDDd |
     * CHILD | INFANT | STILLBORN ]
     * ]
     * Where :
     * > = greater than indicated age
     * < = less than indicated age
     * y = a label indicating years
     * m = a label indicating months
     * d = a label indicating days
     * YY = number of full years
     * MM = number of months
     * DDD = number of days
     * CHILD = age < 8 years
     * INFANT = age < 1 year
     * STILLBORN = died just prior, at, or near birth, 0 years
     * 
     * A number that indicates the age in years, months, and days that the principal was at the time of the associated event. Any labels must come after their corresponding number, for example; 4y 8m 10d. 
     * 
     */

    public class AgeAtEvent : GedcomLine
    {
        public AgeAtEvent(Reporting Reporting)
            : base(Reporting)
        {
        }

        public AgeAtEvent(string LineValue, Reporting Reporting)
            : base(Reporting)
        {
            Age = LineValue;
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string Age { get; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is AgeAtEvent age))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Age, age.Age))
            {
                return false;
            }

            return true;
        }

        protected bool Equals(AgeAtEvent other)
        {
            return base.Equals(other) && Age == other.Age;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Age);
        }
    }
}
