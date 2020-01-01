using System;
using GedcomCore.Framework.Entities.Date;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Header
{
    public partial class HeadSource
    {
        /* 
 *       +2 DATA <NAME_OF_SOURCE_DATA>  {0:1}
 *         +3 DATE <PUBLICATION_DATE>  {0:1}
 *         +3 COPR <COPYRIGHT_SOURCE_DATA>  {0:1}
 * 
 */

        public class Data_ : GedcomLine
        {
            public Data_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "DATA";
            }

            [Tag("")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            [Length(1, 90)]
            public string NameOfSourceData { get; set; }

            [Tag("DATE")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public DateExact PublicationDate { get; set; }

            [Tag("COPR")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            [Length(1, 90)]
            public string Copyright { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var data = obj as Data_;
                if (data == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!NameOfSourceData.Equals(data.NameOfSourceData))
                {
                    return false;
                }

                if (!PublicationDate.Equals(data.PublicationDate))
                {
                    return false;
                }

                if (!Copyright.Equals(data.Copyright))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
