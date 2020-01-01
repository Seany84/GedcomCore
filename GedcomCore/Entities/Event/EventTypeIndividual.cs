using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Event
{
    /*
     * EVENT_TYPE_INDIVIDUAL: = {Size=3:4}
     * [ ADOP | BIRT | BAPM | BARM | BASM |
     * BLES | BURI | CENS | CHR | CHRA |
     * CONF | CREM | DEAT | EMIG | FCOM |
     * GRAD | IMMI | NATU | ORDN |
     * RETI | PROB | WILL | EVEN ]
     * 
     * A code used to indicate the type of family event. The definition is the same as the corresponding event tag defined in Appendix A. 
     * 
     */

    public enum EventTypeIndividual
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
        Barm,

        [EnumTag("BASM")]
        Basm,

        [EnumTag("BLES")]
        Bles,

        [EnumTag("BURI")]
        Burrial,

        [EnumTag("CENS")]
        Cens,

        [EnumTag("CHR")]
        Chr,

        [EnumTag("CHRA")]
        Chra,

        [EnumTag("CONF")]
        Conf,

        [EnumTag("CREM")]
        Crem,

        [EnumTag("DEAT")]
        Death,

        [EnumTag("EMIG")]
        Emig,

        [EnumTag("FCOM")]
        Fcom,

        [EnumTag("GRAD")]
        Grad,

        [EnumTag("IMMI")]
        Immi,

        [EnumTag("NATU")]
        Natu,

        [EnumTag("ORDN")]
        Ordn,

        [EnumTag("RETI")]
        Reti,

        [EnumTag("PROB")]
        Prob,

        [EnumTag("WILL")]
        Will,

        [EnumTag("EVEN")]
        Even
    }
}
