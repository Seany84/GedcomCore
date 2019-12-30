using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
        private DateExactTime date;
        private List<NoteStructure> notes;

        public ChangeDate(Reporting Reporting)
            : base(Reporting)
        {
            notes = new List<NoteStructure>();

            Tag = "CHAN";
        }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public DateExactTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
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

            ChangeDate cd = obj as ChangeDate;
            if (cd == null)
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
