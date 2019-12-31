using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework
{
    /* 
     * ORDINANCE_PROCESS_FLAG: = {Size=2:3}
     * [ yes | no ]
     * A flag that indicates whether submission should be processed for clearing temple ordinances. 
     * 
     */ 

    public enum OrdinanceProcessFlag
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("yes")]
        Yes,

        [EnumTag("no")]
        No
    }
}
