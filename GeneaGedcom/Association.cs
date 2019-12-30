using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /*
     * ASSOCIATION_STRUCTURE: =
     * 
     *   n  ASSO @<XREF:INDI>@  {0:M}
     *     +1 RELA <RELATION_IS_DESCRIPTOR>  {1:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     * 
     *   [NOTE: The ASSOCIATION_STRUCTURE can only link to an INDIVIDUAL_RECORD.]
     * 
     */ 

    public class Association : GedcomLine
    {
        private string indiXRef;
        private string relationIsDescriptor;
        private List<NoteStructure> notes;
        private List<SourceCitation> sourceCitations;

        public Association(Reporting Reporting)
            : base(Reporting)
        {
            notes = new List<NoteStructure>();
            sourceCitations = new List<SourceCitation>();

            Tag = "ASSO";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string IndiXRef
        {
            get
            {
                return indiXRef;
            }
            set
            {
                indiXRef = value;
            }
        }

        [Tag("RELA")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,25)]
        public string RelationIsDescriptor
        {
            get
            {
                return relationIsDescriptor;
            }
            set
            {
                relationIsDescriptor = value;
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

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations
        {
            get
            {
                return sourceCitations;
            }
            set
            {
                sourceCitations = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            Association assoc = obj as Association;
            if (assoc == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(IndiXRef, assoc.IndiXRef))
            {
                return false;
            }

            if (!CompareObjects(RelationIsDescriptor, assoc.RelationIsDescriptor))
            {
                return false;
            }

            if (!Notes.Count.Equals(assoc.Notes.Count))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(assoc.SourceCitations.Count))
            {
                return false;
            }

            return true;
        }
    }
}
