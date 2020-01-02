using System;
using System.Collections.Generic;
using GedcomCore.Framework.Entities.Source;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
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
        public Association(Reporting Reporting)
            : base(Reporting)
        {
            Notes = new List<NoteStructure>();
            SourceCitations = new List<SourceCitation>();

            Tag = "ASSO";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string IndiXRef { get; set; }

        [Tag("RELA")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,25)]
        public string RelationIsDescriptor { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is Association assoc))
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
