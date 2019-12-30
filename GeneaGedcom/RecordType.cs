using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /*
     * RECORD_TYPE: = {Size=3:4}
     * [ FAM | INDI | NOTE | OBJE | REPO | SOUR | SUBM | SUBN ]
     * An indicator of the record type being pointed to or used. For example if in an ASSOciation, an INDIvidual record were to be ASSOciated with a FAM record then:
     * 
     *       0 INDI
     *         1 ASSO @F1@
     *           2 TYPE FAM   /* ASSOCIATION is with a FAM record.
     *           2 RELA Witness at marriage
     * 
     * 
     */
 
    [Obsolete]
    public enum RecordType
    {
        [EnumTag("FAM")]
        Family,

        [EnumTag("INDI")]
        Individual,

        [EnumTag("NOTE")]
        Note,

        [EnumTag("OBJE")]
        Object,

        [EnumTag("REPO")]
        Repository,

        [EnumTag("SOUR")]
        Source,

        [EnumTag("SUBM")]
        Submitter,

        [EnumTag("SUBN")]
        Submission
    }
}
