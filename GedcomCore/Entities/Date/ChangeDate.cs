using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Date
{
    /*
     * CHANGE_DATE: =
     * 
     *   n  CHAN          {1:1}
     *     +1 DATE <CHANGE_DATE>  {1:1}
     *       +2 TIME <TIME_VALUE>  {0:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     * The change date is intended to only record the last change to a record. Some systems may want to manage the change process with more detail, but it is sufficient for GEDCOM purposes to indicate the last time that a record was modified. 
     * 
     */

    public class ChangeDate : GedcomLine
    {
        public ChangeDate(Reporting Reporting)
            : base(Reporting)
        {
            Notes = new List<NoteStructure>();

            Tag = "CHAN";
        }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public DateExactTime Date { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is ChangeDate cd))
            {
                return false;
            }

            if (!CompareObjects(Date, cd.Date))
            {
                return false;
            }

            if (!Notes.Count.Equals(cd.Notes))
            {
                return false;
            }

            return true;
        }

    }
}
