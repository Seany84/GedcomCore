using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * PLACE_STRUCTURE: =
     * 
     *   n PLAC <PLACE_VALUE>  {1:1}
     *     +1 FORM <PLACE_HIERARCHY>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     */ 

    public class Place : GedcomLine
    {
        public Place(Reporting Reporting)
            : base(Reporting)
        {
            SourceCitations = new List<SourceCitation>();
            Notes = new List<NoteStructure>();

            Tag = "PLAC";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,120)]
        public string Value { get; set; }

        [Tag("FORM")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 120)]
        public string PlaceHierarchy { get; set; }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var place = obj as Place;
            if (place == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Value, place.Value))
            {
                return false;
            }

            if (!CompareObjects(PlaceHierarchy, PlaceHierarchy))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(place.SourceCitations.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(place.Notes))
            {
                return false;
            }

            return true;
        }
    }
}
