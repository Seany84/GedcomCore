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
        public SpouseToFamilyLink(Reporting Reporting)
            : base(Reporting)
        {
            Notes = new List<NoteStructure>();

            Tag = "FAMS";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string FamilyXRef { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var link = obj as SpouseToFamilyLink;
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
