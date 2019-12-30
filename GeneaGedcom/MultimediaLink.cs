using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * MULTIMEDIA_LINK: =
     * 
     *   [          // embedded form
     *   n  OBJE @<XREF:OBJE>@  {1:1}
     *   |          // linked form
     *   n  OBJE           {1:1}
     *     +1 FORM <MULTIMEDIA_FORMAT>  {1:1}
     *     +1 TITL <DESCRIPTIVE_TITLE>  {0:1}
     *     +1 FILE <MULTIMEDIA_FILE_REFERENCE>  {1:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *   ]
     * 
     * 
     * This structure provides two options in handling the GEDCOM multimedia interface. The first alternative (embedded) includes all of the data, including the multimedia object, within the transmission file. The embedded method includes pointers to GEDCOM records that contain encoded image or sound objects. Each record represents a multimedia object or object fragment. An object fragment is created by breaking the multimedia files into several multimedia object records of 32K or less. These fragments are tied together by chaining from one multimedia object fragment to the next in sequence. This procedure will help manage the size of a multimedia GEDCOM record so that existing systems which are not expecting large multimedia records may discard the records without crashing due to the size of the record. Systems which handle embedded multimedia can reconstitute the multimedia fragments by decoding the object fragments and concatenating them to the assigned multimedia file.
     * 
     * The second method allows the GEDCOM context to be connected to an external multimedia file. This process is only managed by GEDCOM in the sense that the appropriate file name is included in the GEDCOM file in context, but the maintenance and transfer of the multimedia files are external to GEDCOM. 
     * 
     */

    public class MultimediaLink : GedcomLine
    {
        private string multimediaXRef;
        private MultimediaFormat format;
        private string title;
        private string fileReference;
        private List<NoteStructure> notes;

        private bool isEmbedded;

        public MultimediaLink(Reporting Reporting)
            : base(Reporting)
        {
            notes = new List<NoteStructure>();

            isEmbedded = true;

            Tag = "OBJE";

            format = MultimediaFormat.Unknown;
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string MultimediaXRef
        {
            get => multimediaXRef;
            set
            {
                multimediaXRef = value;

                isEmbedded = false;
            }
        }

        [Tag("FORM", MultimediaFormat.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public MultimediaFormat MultimediaFormat
        {
            get => format;
            set
            {
                if (isEmbedded)
                {
                    throw new InvalidOperationException("FORM is not valid for embedded multimedia links");
                }

                format = value;
            }
        }

        [Tag("TITL")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,248)]
        public string Title
        {
            get => title;
            set
            {
                if (isEmbedded)
                {
                    throw new InvalidOperationException("TITL is not valid for embedded multimedia links");
                }

                title = value;
            }
        }

        [Tag("FILE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,30)]
        public string FileReference
        {
            get => fileReference;
            set
            {
                if (isEmbedded)
                {
                    throw new InvalidOperationException("FILE is not valid for embedded multimedia links");
                }

                fileReference = value;
            }
        }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes
        {
            get => notes;
            set
            {
                if (isEmbedded)
                {
                    throw new InvalidOperationException("NOTE is not valid for embedded multimedia links");
                }

                notes = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var ml = obj as MultimediaLink;
            if (ml == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(MultimediaXRef, ml.MultimediaXRef))
            {
                return false;
            }

            if (!CompareObjects(MultimediaFormat, ml.MultimediaFormat))
            {
                return false;
            }

            if (!CompareObjects(Title, ml.Title))
            {
                return false;
            }

            if (!CompareObjects(FileReference, ml.FileReference))
            {
                return false;
            }

            if (!Notes.Count.Equals(ml.Notes.Count))
            {
                return false;
            }

            return true;
        }
    }
}
