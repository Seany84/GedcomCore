using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class Header
    {
        /* 
 *     +1 GEDC        {1:1}
 *       +2 VERS <VERSION_NUMBER>  {1:1}
 *       +2 FORM <GEDCOM_FORM>  {1:1}
 * 
 */

        public class GedcomInformation_ : GedcomLine
        {
            private static string[] acceptedFormats = { "LINEAGE-LINKED" };

            private string format;

            public GedcomInformation_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "GEDC";
            }

            [Tag("VERS")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            public string Version { get; set; }

            [Tag("FORM")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            public string Format
            {
                get => format;
                set
                {
                    foreach (var f in acceptedFormats)
                    {
                        if (value.Equals(f))
                        {
                            format = value;

                            return;
                        }
                    }

                    throw new FormatException(string.Format("invalid value: {0}", value));
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                var info = obj as GedcomInformation_;
                if (info == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!Format.Equals(info.Format))
                {
                    return false;
                }

                if (!Version.Equals(info.Version))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
