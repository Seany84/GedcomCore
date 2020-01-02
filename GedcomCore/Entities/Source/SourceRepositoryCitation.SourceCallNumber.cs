using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Source
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
            public SourceCallNumber_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "CALN";
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 120)]
            public string SourceCallNumber { get; set; }

            [Tag("MEDI", SourceMediaType.Unknown)]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public SourceMediaType MediaSourceType { get; set; } = SourceMediaType.Unknown;

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                if (!(obj is SourceCallNumber_ scn))
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
