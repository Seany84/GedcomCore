using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * SPOUSE_TO_FAMILY_LINK: =
     * 
     *   n FAMS @<XREF:FAM>@  {1:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */ 

    public class SpouseToFamilyLink : GedcomLine
    {
        private string familyXRef;
        private List<NoteStructure> notes;

        public SpouseToFamilyLink(Reporting Reporting)
            : base(Reporting)
        {
            notes = new List<NoteStructure>();

            Tag = "FAMS";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string FamilyXRef
        {
            get
            {
                return familyXRef;
            }
            set
            {
                familyXRef = value;
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

            SpouseToFamilyLink link = obj as SpouseToFamilyLink;
            if (link == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(FamilyXRef, link.FamilyXRef))
            {
                return false;
            }

            if (!Notes.Count.Equals(link.Notes.Count))
            {
                return false;
            }

            return true;
        }
    }
}
