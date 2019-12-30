using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class SourceRepositoryCitation
    {
        /* 
         *     +1 CALN <SOURCE_CALL_NUMBER>  {0:M}
         *        +2 MEDI <SOURCE_MEDIA_TYPE>  {0:1}
         * 
         */

        public partial class SourceCallNumber_ : GedcomLine
        {
            private string sourceCallNumber;
            private SourceMediaType mediaSourceType = SourceMediaType.Unknown;

            public SourceCallNumber_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "CALN";
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 120)]
            public string SourceCallNumber
            {
                get
                {
                    return sourceCallNumber;
                }
                set
                {
                    sourceCallNumber = value;
                }
            }

            [Tag("MEDI", SourceMediaType.Unknown)]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public SourceMediaType MediaSourceType
            {
                get
                {
                    return mediaSourceType;
                }
                set
                {
                    mediaSourceType = value;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                SourceCallNumber_ scn = obj as SourceCallNumber_;
                if (scn == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!SourceCallNumber.Equals(scn.SourceCallNumber))
                {
                    return false;
                }

                if (!MediaSourceType.Equals(scn.MediaSourceType))
                {
                    return false;
                }

                return true;
            }
        }
    }
}