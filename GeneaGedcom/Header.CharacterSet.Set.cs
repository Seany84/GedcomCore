using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
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
