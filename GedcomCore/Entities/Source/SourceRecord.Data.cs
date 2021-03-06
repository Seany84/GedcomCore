using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Source
{
    public partial class SourceRecord
    {
        /*
 *     +1 DATA        {0:1}
 *       +2 EVEN <EVENTS_RECORDED>  {0:M}
 *         +3 DATE <DATE_PERIOD>  {0:1}
 *         +3 PLAC <SOURCE_JURISDICTION_PLACE>  {0:1}
 *       +2 AGNC <RESPONSIBLE_AGENCY>  {0:1}
 *       +2 <<NOTE_STRUCTURE>>  {0:M}
 * 
 */

        public partial class Data_ : GedcomLine
        {
            public Data_(Reporting Reporting)
                : base(Reporting)
            {
                Events = new List<Event>();
                Notes = new List<NoteStructure>();

                Tag = "DATA";
            }

            [Tag("EVEN", typeof(Event))]
            [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
            public List<Event> Events { get; set; }

            [Tag("AGNC")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string ResponsibleAgency { get; set; }

            [Tag("NOTE", typeof(NoteStructure))]
            [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
            public List<NoteStructure> Notes { get; set; }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                if (!(obj is Data_ data))
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!Events.Count.Equals(data.Events.Count))
                {
                    return false;
                }

                if (!ResponsibleAgency.Equals(data.ResponsibleAgency))
                {
                    return false;
                }

                if (!Notes.Count.Equals(data.Notes.Count))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
