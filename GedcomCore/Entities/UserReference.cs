using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /* 
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     * 
     */

    public class UserReference : GedcomLine
    {
        public UserReference(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "REFN";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1, 20)]
        public string UserReferenceNumber { get; set; }

        [Tag("TYPE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 40)]
        public string UserReferenceType { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var ur = obj as UserReference;
            if (ur == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(UserReferenceNumber, ur.UserReferenceNumber))
            {
                return false;
            }

            if (!CompareObjects(UserReferenceType, ur.UserReferenceType))
            {
                return false;
            }

            return true;
        }
    }
}
