using System;
using System.Collections.Generic;
using GedcomCore.Framework.Entities.Date;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Source
{
    public partial class SourceCitation
    {
        /* 
 *     +1 DATA        {0:1}
 *       +2 DATE <ENTRY_RECORDING_DATE>  {0:1}
 *       +2 TEXT <TEXT_FROM_SOURCE>  {0:M}
 *         +3 [ CONC | CONT ] <TEXT_FROM_SOURCE>  {0:M}
 * 
 */

        public class Data_ : GedcomLine
        {
            public Data_(Reporting Reporting)
                : base(Reporting)
            {
                TextFromSource = new List<ContinueableText>();

                Tag = "DATA";
            }

            [Tag("DATE")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string EntryRecordingDateString
            {
                get => EntryRecordingDate == null ? "" : EntryRecordingDate.DateString;
                set => EntryRecordingDate = DateValue.CreateDateValue(value, Reporting);
            }

            public DateValue EntryRecordingDate { get; set; }

            [Tag("TEXT", typeof(ContinueableText))]
            [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
            public List<ContinueableText> TextFromSource { get; set; }

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

                if (!EntryRecordingDate.Equals(data.EntryRecordingDate))
                {
                    return false;
                }

                if (!TextFromSource.Equals(data.TextFromSource))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
