using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
            private List<Event> events;
            private string responsibleAgency;
            private List<NoteStructure> notes;

            public Data_(Reporting Reporting)
                : base(Reporting)
            {
                events = new List<Event>();
                notes = new List<NoteStructure>();

                Tag = "DATA";
            }

            [Tag("EVEN", typeof(Event))]
            [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
            public List<Event> Events
            {
                get
                {
                    return events;
                }
                set
                {
                    events = value;
                }
            }

            [Tag("AGNC")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string ResponsibleAgency
            {
                get
                {
                    return responsibleAgency;
                }
                set
                {
                    responsibleAgency = value;
                }
            }

            [Tag("NOTE", typeof(NoteStructure))]
            [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
            public List<NoteStructure> Notes
            {
                get
                {
                    return notes;
                }
                set
                {
                    notes = value;
                }
            }

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
