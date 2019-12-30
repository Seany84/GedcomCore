using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * SOURCE_CITATION: =
     * 
     *   [  
     *   n SOUR @<XREF:SOUR>@    // pointer to source record   {1:1}
     *     +1 PAGE <WHERE_WITHIN_SOURCE>  {0:1}
     *     +1 EVEN <EVENT_TYPE_CITED_FROM>  {0:1}
     *       +2 ROLE <ROLE_IN_EVENT>  {0:1}
     *     +1 DATA        {0:1}
     *       +2 DATE <ENTRY_RECORDING_DATE>  {0:1}
     *       +2 TEXT <TEXT_FROM_SOURCE>  {0:M}
     *         +3 [ CONC | CONT ] <TEXT_FROM_SOURCE>  {0:M}
     *     +1 QUAY <CERTAINTY_ASSESSMENT>  {0:1}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   |              // Systems not using source records
     *   n SOUR <SOURCE_DESCRIPTION>  {1:1}
     *     +1 [ CONC | CONT ] <SOURCE_DESCRIPTION>  {0:M}
     *     +1 TEXT <TEXT_FROM_SOURCE>  {0:M}
     *        +2 [CONC | CONT ] <TEXT_FROM_SOURCE>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   ]
     * 
     * 
     * The data provided in the <<SOURCE_CITATION>> structure is source-related information specific to the data being cited. (See GEDCOM examples.) Systems that do not use SOURCE_RECORDS must use the second SOURce citation structure option. When systems which support SOURCE_RECORD structures encounter source citations which do not contain pointers to source records, that system will need to create a SOURCE_RECORD and store the <SOURCE_DESCRIPTION> information found in the non-structured source citation in either the title area of that SOURCE_RECORD, or if the title field is not large enough, place a "(See Notes)" text in the title area, and place the unstructured source description in the source record's note field.
     * 
     * The information intended to be placed in the citation structure includes:
     * 
     *     * A pointer to the SOURCE_RECORD, which contains a more general description of the source.
     *     * Information, such as a page number, on how to find the cited data within the source.
     *     * Actual text from the source that was used in making assertions, for example a date phrase as actually recorded or an applicable sentence from a letter, would be appropriate.
     *     * Data that allows an assessment of the relative value of one source over another for making the recorded assertions (primary or secondary source, etc.). Data needed for this assessment is how much time from the asserted fact and when the source event was recorded, what type of event was cited, and what was the role of this person in the cited event.
     *           -Date when the entry was recorded in source document, ".SOUR.DATA.DATE."
     *           -Event that initiated the recording, ".SOUR.EVEN."
     *           -Role of this person in the event, ".SOUR.EVEN.ROLE".
     * 
     */ 

    public partial class SourceCitation : GedcomLine
    {
        private string sourceXRef;
        private string page;
        private Event_ eventTypeCitedFrom;
        private Data_ data;
        private int certaintyAssessment;
        private List<MultimediaLink> multimedia;
        private List<NoteStructure> notes;
        private string sourceDescription;
        private List<ContinueableText> textFromSource;

        private bool isInline;

        private List<AdditionalLine> tmp;

        public SourceCitation(Reporting Reporting)
            : base(Reporting)
        {
            multimedia = new List<MultimediaLink>();
            notes = new List<NoteStructure>();
            textFromSource = new List<ContinueableText>();

            Tag = "SOUR";

            certaintyAssessment = -1;
        }

        [Tag("")]
        //TODO: length
        public string LineValue
        {
            get
            {
                if (isInline)
                {
                    return SourceDescription;
                }
                else
                {
                    return SourceXRef;
                }
            }
            set
            {
                if (value.StartsWith("@"))
                {
                    SourceXRef = value;
                    isInline = false;
                }
                else
                {
                    SourceDescription = value;
                    isInline = true;
                }

            }
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SourceXRef
        {
            get => sourceXRef;
            set => sourceXRef = value;
        }

        [Tag("CONT")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Continue
        {
            set
            {
                if (SourceDescription == null)
                {
                    SourceDescription = "";
                }
                SourceDescription += "\n" + value;
            }
        }

        [Tag("CONT", typeof(AdditionalLine))]
        [Tag("CONC", typeof(AdditionalLine))]
        [Length(1, 60)]
        public List<AdditionalLine> AdditionalLines => tmp;

        [Tag("CONC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public string Concatenate
        {
            set
            {
                if (SourceDescription == null)
                {
                    SourceDescription = "";
                }
                SourceDescription += value;
            }
        }

        [Tag("PAGE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,248)]
        public string Page
        {
            get => page;
            set => page = value;
        }

        [Tag("EVEN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Event_ Event
        {
            get => eventTypeCitedFrom;
            set => eventTypeCitedFrom = value;
        }

        [Tag("DATA")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Data_ Data
        {
            get => data;
            set => data = value;
        }

        [Tag("QUAY", -1)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public int CertaintyAssessment
        {
            get => certaintyAssessment;
            set => certaintyAssessment = value;
        }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia
        {
            get => multimedia;
            set => multimedia = value;
        }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes
        {
            get => notes;
            set => notes = value;
        }

        [Tag("SOUR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SourceDescription
        {
            get
            {
                if (sourceDescription == null)
                {
                    tmp = new List<AdditionalLine>();
                    return "";
                }

                var s = sourceDescription.Split(new[] { "\n" }, StringSplitOptions.None);
                tmp = new List<AdditionalLine>();
                for (var n = 0; n < s.Length - 1; n++)
                {
                    // a line (including spaces, the tag, etc) must not be longer than 255 characters
                    // it shoule be safe to assume that a content-part of 240 is ok
                    var parts = ContinueableText.MakeParts(s[n + 1], 240);

                    var contLine = new AdditionalLine(parts[0], Reporting);
                    contLine.Tag = "CONT";
                    tmp.Add(contLine);

                    for (var m = 1; m < parts.Length; m++)
                    {
                        var concLine = new AdditionalLine(parts[m], Reporting);
                        concLine.Tag = "CONC";
                        tmp.Add(concLine);
                    }
                }
                return s[0];
            }
            set => sourceDescription = value;
        }

        [Tag("TEXT", typeof(ContinueableText))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<ContinueableText> TextFromSource
        {
            get => textFromSource;
            set => textFromSource = value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var sc = obj as SourceCitation;
            if (sc == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(SourceXRef, sc.SourceXRef))
            {
                return false;
            }

            if (!CompareObjects(Page, sc.Page))
            {
                return false;
            }

            if (!CompareObjects(Event, sc.Event))
            {
                return false;
            }

            if (!CompareObjects(Data, sc.Data))
            {
                return false;
            }

            if (!CompareObjects(CertaintyAssessment, sc.CertaintyAssessment))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(sc.Multimedia.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(sc.Notes.Count))
            {
                return false;
            }

            if (!CompareObjects(SourceDescription, sc.SourceDescription))
            {
                return false;
            }

            if (!CompareObjects(TextFromSource, sc.TextFromSource))
            {
                return false;
            }

            return true;
        }
    }
}
