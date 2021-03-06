using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Source
{
    /* 
     * SOURCE_REPOSITORY_CITATION: =
     * 
     *   n  REPO @<XREF:REPO>@ {1:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 CALN <SOURCE_CALL_NUMBER>  {0:M}
     *        +2 MEDI <SOURCE_MEDIA_TYPE>  {0:1}
     * 
     * This structure is used within a source record to point to a name and address record of the holder of the source document. Formal and informal repository name and addresses are stored in the REPOSITORY_RECORD. Informal repositories include owner's of an unpublished work or of a rare published source, or a keeper of personal collections. An example would be the owner of a family Bible containing unpublished family genealogical entries. More formal repositories, such as the Family History Library, should show a call number of the source at that repository. The call number of that source should be recorded using a subordinate CALN tag. Systems which do not structure a repository name and address interface should store the information about where the source record is stored in the <<NOTE_STRUCTURE>> of this structure. 
     * 
     */ 

    public partial class SourceRepositoryCitation : GedcomLine
    {
        public SourceRepositoryCitation(Reporting Reporting)
            : base(Reporting)
        {
            Notes = new List<NoteStructure>();
            SourceCallNumber = new List<SourceCallNumber_>();

            Tag = "REPO";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string RepositoryXRef { get; set; }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        [Tag("CALN", typeof(SourceCallNumber_))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCallNumber_> SourceCallNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is SourceRepositoryCitation src))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(RepositoryXRef, src.RepositoryXRef))
            {
                return false;
            }

            if (!Notes.Count.Equals(src.Notes.Count))
            {
                return false;
            }

            if (!CompareObjects(SourceCallNumber, src.SourceCallNumber))
            {
                return false;
            }

            return true;
        }
    }
}
