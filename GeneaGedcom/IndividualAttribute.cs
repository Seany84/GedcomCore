using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    /* 
     * INDIVIDUAL_ATTRIBUTE_STRUCTURE: =
     * 
     *   [
     *   n  CAST <CASTE_NAME>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  DSCR <PHYSICAL_DESCRIPTION>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  EDUC <SCHOLASTIC_ACHIEVEMENT>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  IDNO <NATIONAL_ID_NUMBER>   {1:1}*
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  NATI <NATIONAL_OR_TRIBAL_ORIGIN>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  NCHI <COUNT_OF_CHILDREN>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  NMR <COUNT_OF_MARRIAGES>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  OCCU <OCCUPATION>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  PROP <POSSESSIONS>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  RELI <RELIGIOUS_AFFILIATION>   {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  RESI           {1:1}  
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  SSN <SOCIAL_SECURITY_NUMBER>   {0:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   |
     *   n  TITL <NOBILITY_TYPE_TITLE>  {1:1}
     *     +1 <<EVENT_DETAIL>>  {0:1}
     *   ]
     * 
     * 
     * * Note: The usage of IDNO requires that the subordinate TYPE tag be used to define what kind of number is assigned to IDNO. 
     * 
     */
 
    public class IndividualAttribute : EventDetail
    {
        public IndividualAttribute(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(0,1)]
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException();
            }

            var attrib = obj as IndividualAttribute;
            if (attrib == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Text, attrib.Text))
            {
                return false;
            }

            return true;
        }
    }
}
