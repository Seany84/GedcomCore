using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
        private string age;

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
        public string Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var age = obj as AgeAtEvent;
            if (age == null)
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
    }
}
