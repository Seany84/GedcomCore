using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities.Header
{
    public partial class Header
    {
        public partial class CharacterSet_ : GedcomLine
        {
            public enum Set_
            {
                [EnumTag("ANSEL")]
                ANSEL,

                [EnumTag("ASCII")]
                ASCII,

                [EnumTag("UNICODE")]
                Unicode
            }
        }
    }
}
