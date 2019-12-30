using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /*
     * EVENT_ATTRIBUTE_TYPE: = {Size=1:15}
     * [ <EVENT_TYPE_INDIVIDUAL> |
     * <EVENT_TYPE_FAMILY> |
     * <ATTRIBUTE_TYPE> ]
     * A code that classifies the principal event or happening that caused the source record entry to be created. If the event or attribute doesn't translate to one of these tag codes, then a user supplied code is expected, but will be considered as the generic tag EVEN for source certainty evaluation. 
     * 
     */
    public enum EventAttributeType
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("ADOP")]
        Adoption,

        [EnumTag("BIRT")]
        Birth,

        [EnumTag("BAPM")]
        Baptism,

        [EnumTag("BARM")]
        BarMitzvah,

        [EnumTag("BASM")]
        BasMitzvah,

        [EnumTag("BLES")]
        Blessing,

        [EnumTag("BURI")]
        Burial,

        [EnumTag("CENS")]
        Census,

        [EnumTag("CHR")]
        Christening,

        [EnumTag("CHRA")]
        AdultChristening,

        [EnumTag("CONF")]
        Confirmation,

        [EnumTag("CREM")]
        Cremation,

        [EnumTag("DEAT")]
        Death,

        [EnumTag("EMIG")]
        Emigration,

        [EnumTag("FCOM")]
        FirstCommunion,

        [EnumTag("GRAD")]
        Gradudation,

        [EnumTag("IMMI")]
        Immigration,

        [EnumTag("NATU")]
        Naturalization,

        [EnumTag("ORDN")]
        Ordination,

        [EnumTag("RETI")]
        Retirement,

        [EnumTag("PROB")]
        Probate,

        [EnumTag("WILL")]
        Will,

        [EnumTag("EVEN")]
        [UnknownEnum]
        Event,

        [EnumTag("ANUL")]
        Annulment,

        [EnumTag("DIV")]
        Divorce,

        [EnumTag("DIVF")]
        DivorceFiled,

        [EnumTag("ENGA")]
        Engagement,

        [EnumTag("MARR")]
        Marriage,

        [EnumTag("MARB")]
        MarriageBann,

        [EnumTag("MARC")]
        MarriageContract,

        [EnumTag("MARL")]
        MarriageLicense,

        [EnumTag("MARS")]
        MarriageSettlement,

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
