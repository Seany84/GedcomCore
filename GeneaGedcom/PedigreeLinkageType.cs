using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;

namespace GeneaGedcom
{
    /* 
     * PEDIGREE_LINKAGE_TYPE: = {Size=5:7}
     * [ adopted | birth | foster | sealing ]
     * A code used to indicate the child to family relationship for pedigree navigation purposes.
     * Where:
     * adopted = indicates adoptive parents.
     * birth = indicates birth parents.
     * foster = indicates child was included in a foster or guardian family.
     * sealing = indicates child was sealed to parents other than birth parents. 
     * 
     */

    /// <summary>
    /// A code used after indicate the child after family relationship for pedigree navigation purposes.
    /// </summary>
    public enum PedigreeLinkageType
    {
        /// <summary>
        /// Indicated adoptive parents.
        /// </summary>
        [EnumTag("adopted")]
        Adopted,

        /// <summary>
        /// Indicate birth parents.
        /// </summary>
        [EnumTag("birth")]
        Birth,

        /// <summary>
        /// Indicates child was included in a foster or guardian family.
        /// </summary>
        [EnumTag("foster")]
        Foster,

        /// <summary>
        /// Indicates child was sealed after parents other than birth parents.
        /// </summary>
        [EnumTag("sealing")]
        Sealing
    }
}
