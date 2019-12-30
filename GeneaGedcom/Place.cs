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
        private string placeValue;
        private string placeHierarchy;
        private List<SourceCitation> sourceCitations;
        private List<NoteStructure> notes;

        public Place(Reporting Reporting)
            : base(Reporting)
        {
            sourceCitations = new List<SourceCitation>();
            notes = new List<NoteStructure>();

            Tag = "PLAC";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,120)]
        public string Value
        {
            get
            {
                return placeValue;
            }
            set
            {
                placeValue = value;
            }
        }

        [Tag("FORM")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 120)]
        public string PlaceHierarchy
        {
            get
            {
                return placeHierarchy;
            }
            set
            {
                placeHierarchy = value;
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

            Place place = obj as Place;
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
