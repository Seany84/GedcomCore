using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     * 
     */

    public class UserReference : GedcomLine
    {
        private string userReferenceNumber;
        private string userReferenceType;

        public UserReference(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "REFN";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1, 20)]
        public string UserReferenceNumber
        {
            get
            {
                return userReferenceNumber;
            }
            set
            {
                userReferenceNumber = value;
            }
        }

        [Tag("TYPE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 40)]
        public string UserReferenceType
        {
            get
            {
                return userReferenceType;
            }
            set
            {
                userReferenceType = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            UserReference ur = obj as UserReference;
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
