using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework
{
    /*
     * ATTRIBUTE_TYPE: = {Size=1:4}
     * [ CAST | EDUC | NATI | OCCU | PROP | RELI | RESI | TITL ] 
     * 
     */

    public enum AttributeType
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("CAST")]
        Caste,

        [EnumTag("EDUC")]
        Education,

        [EnumTag("NATI")]
        Nationality,

        [EnumTag("OCCU")]
        Occupation,

        [EnumTag("PROP")]
        Property,

        [EnumTag("RELI")]
        Religion,

        [EnumTag("RESI")]
        Residence,

        [EnumTag("TITL")]
        Title
    }
}
