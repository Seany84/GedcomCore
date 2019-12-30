using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * CHILD_TO_FAMILY_LINK: =
     * 
     *   n  FAMC @<XREF:FAM>@  {1:1}
     *     +1 PEDI <PEDIGREE_LINKAGE_TYPE>  {0:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */ 

    public class ChildToFamilyLink : GedcomLine
    {
        private string familyXRef;
        private List<PedigreeLinkageType> pedigreeLinkageTypes;
        private List<NoteStructure> notes;

        public ChildToFamilyLink(Reporting Reporting)
            : base(Reporting)
        {
            pedigreeLinkageTypes = new List<PedigreeLinkageType>();
            notes = new List<NoteStructure>();

            Tag = "FAMC";
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

        [Tag("PEDI", typeof(PedigreeLinkageType))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<PedigreeLinkageType> PedigreeLinkageTypes
        {
            get
            {
                return pedigreeLinkageTypes;
            }
            set
            {
                pedigreeLinkageTypes = value;
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

            var link = obj as ChildToFamilyLink;
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

            if (!CompareObjects(PedigreeLinkageTypes, link.PedigreeLinkageTypes))
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
