using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Date
{
    /* 
     * MONTH_FREN: = {Size=4}
     * [ VEND | BRUM | FRIM | NIVO | PLUV | VENT | GERM |
     * FLOR | PRAI | MESS | THER | FRUC | COMP ]
     * 
     * Where:
     * VEND = VENDEMIAIRE
     * BRUM = BRUMAIRE
     * FRIM = FRIMAIRE
     * NIVO = NIVOSE
     * PLUV = PLUVIOSE
     * VENT = VENTOSE
     * GERM = GERMINAL
     * FLOR = FLOREAL
     * PRAI = PRAIRIAL
     * MESS = MESSIDOR
     * THER = THERMIDOR
     * FRUC = FRUCTIDOR
     * COMP = JOUR_COMPLEMENTAIRS 
     * 
     */

    public enum MonthFrench
    {
        [EnumTag("VEND")]
        Vendemiaire = 1,

        [EnumTag("BRUM")]
        Brumaire = 2,

        [EnumTag("FRIM")]
        Frimaire = 3,

        [EnumTag("NIVO")]
        Nivose = 4,

        [EnumTag("PLUV")]
        Pluvoise = 5,

        [EnumTag("VENT")]
        Ventose = 6,

        [EnumTag("GERM")]
        Germinal = 7,

        [EnumTag("FLOR")]
        Floreal = 8,

        [EnumTag("PRAI")]
        Prairial = 9,

        [EnumTag("MESS")]
        Messidor = 10,

        [EnumTag("THER")]
        Thermidor = 11,

        [EnumTag("FRUC")]
        Fructidor = 12,

        [EnumTag("COMP")]
        JourComplementairs = 13,

        [UnknownEnum]
        Unknown = 0
    }
}
