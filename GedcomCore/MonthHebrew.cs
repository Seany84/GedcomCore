using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework
{
    /* 
     * MONTH_HEBR: = {Size=3}
     * [ TSH | CSH | KSL | TVT | SHV | ADR | ADS |
     * NSN | IYR | SVN | TMZ | AAV | ELL ]
     * 
     * Where:
     * TSH = Tishri
     * CSH = Cheshvan
     * KSL = Kislev
     * TVT = Tevet
     * SHV = Shevat
     * ADR = Adar
     * ADS = Adar Sheni
     * NSN = Nisan
     * IYR = Iyar
     * SVN = Sivan
     * TMZ = Tammuz
     * AAV = Av
     * ELL = Elul
     * 
     */

    public enum MonthHebrew
    {
        [EnumTag("TSH")]
        Tishri,

        [EnumTag("KSL")]
        Cheshvan,

        [EnumTag("KSL")]
        Kislev,

        [EnumTag("TVT")]
        Tevet,

        [EnumTag("SHV")]
        Shevat,

        [EnumTag("ADR")]
        Adar,

        [EnumTag("ADS")]
        AdarSheni,

        [EnumTag("NSN")]
        Nisan,

        [EnumTag("IYR")]
        Iyar,

        [EnumTag("SVN")]
        Sivan,

        [EnumTag("TMZ")]
        Tammuz,

        [EnumTag("AAV")]
        Av,

        [EnumTag("ELL")]
        Elul,

        [UnknownEnum]
        Unknown
    }
}
