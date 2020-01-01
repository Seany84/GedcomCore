using System;
using System.Collections.Generic;
using GedcomCore.Framework.Entities.Source;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /* 
     * PERSONAL_NAME_STRUCTURE: =
     * 
     *   n  NAME <NAME_PERSONAL>  {1:1}
     *     +1 NPFX <NAME_PIECE_PREFIX>  {0:1}
     *     +1 GIVN <NAME_PIECE_GIVEN>  {0:1}
     *     +1 NICK <NAME_PIECE_NICKNAME>  {0:1}
     *     +1 SPFX <NAME_PIECE_SURNAME_PREFIX>  {0:1}
     *     +1 SURN <NAME_PIECE_SURNAME>  {0:1}
     *     +1 NSFX <NAME_PIECE_SUFFIX>  {0:1}
     *     +1 <<SOURCE_CITATION>>  {0:M}
     *       +2 <<NOTE_STRUCTURE>>  {0:M}
     *       +2 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     * 
     * 
     * The name value is formed in the manner the name is normally spoken, with the given name and family name (surname) separated by slashes (/). (See <NAME_PERSONAL>.) Based on the dynamic nature or unknown compositions of naming conventions, it is difficult to provide more detailed name piece structure to handle every case. The NPFX, GIVN, NICK, SPFX, SURN, and NSFX tags are provided optionally for systems that cannot operate effectively with less structured information. For current future compatibility, all systems must construct their names based on the <NAME_PERSONAL> structure. Those using the optional name pieces should assume that few systems will process them, and most will not provide the name pieces. Future GEDCOM releases (6.0 and later) will likely apply a very different strategy to resolve this problem, possibly using a sophisticated parser and a name-knowledge database. 
     * 
     */

    public class PersonalName : GedcomLine
    {
        private string name;
        private string prefix;
        private string given;
        private string nickname;
        private string surnamePrefix;
        private string surname;
        private string suffix;
        private List<SourceCitation> sourceCitations;

        public PersonalName(Reporting Reporting)
            : base(Reporting)
        {
            sourceCitations = new List<SourceCitation>();
            Notes = new List<NoteStructure>();

            Tag = "NAME";
        }

        public PersonalName(string Name, Reporting Reporting)
            : this(Reporting)
        {
            this.Name = Name;
        }


        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,120)]
        public string Name
        {
            get => name;
            set
            {
                name = value;

                checkForSeperatedNamePieces();
            }
        }

        public bool HasSeperatedNamePieces { get; private set; }

        [Tag("NPFX")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,30)]
        public string NamePiecePrefix
        {
            get => prefix;
            set
            {
                prefix = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("GIVN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,120)]
        public string NamePieceGiven
        {
            get => given;
            set
            {
                given = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("NICK")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,30)]
        public string NamePieceNickname
        {
            get => nickname;
            set
            {
                nickname = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("SPFX")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 30)]
        public string NamePieceSurnamePrefix
        {
            get => surnamePrefix;
            set
            {
                surnamePrefix = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("SURN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1, 120)]
        public string NamePieceSurname
        {
            get => surname;
            set
            {
                surname = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("NSFX")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,30)]
        public string NamePieceSuffix
        {
            get => suffix;
            set
            {
                suffix = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("SOUR", typeof(SourceCitation))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<SourceCitation> SourceCitations
        {
            get => sourceCitations;
            set
            {
                sourceCitations = value;

                checkForSeperatedNamePieces();
            }
        }

        [Tag("NOTE", typeof(NoteStructure))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<NoteStructure> Notes { get; set; }

        private void checkForSeperatedNamePieces()
        {
            HasSeperatedNamePieces = false;

            if ((prefix != null) && string.IsNullOrEmpty(prefix))
            {
                HasSeperatedNamePieces = true;
                return;
            }

            if ((given != null) && string.IsNullOrEmpty(given))
            {
                HasSeperatedNamePieces = true;
                return;
            }

            if ((nickname != null) && string.IsNullOrEmpty(nickname))
            {
                HasSeperatedNamePieces = true;
                return;
            }

            if ((surnamePrefix != null) && string.IsNullOrEmpty(surnamePrefix))
            {
                HasSeperatedNamePieces = true;
                return;
            }

            if ((surname != null) && string.IsNullOrEmpty(surname))
            {
                HasSeperatedNamePieces = true;
                return;
            }

            if ((suffix != null) && string.IsNullOrEmpty(suffix))
            {
                HasSeperatedNamePieces = true;
                return;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var name = obj as PersonalName;
            if (name == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Name, name.Name))
            {
                return false;
            }

            if (!CompareObjects(NamePiecePrefix, name.NamePiecePrefix))
            {
                return false;
            }

            if (!CompareObjects(NamePieceGiven, name.NamePieceGiven))
            {
                return false;
            }

            if (!CompareObjects(NamePieceNickname, name.NamePieceNickname))
            {
                return false;
            }

            if (!CompareObjects(NamePieceSurnamePrefix, name.NamePieceSurnamePrefix))
            {
                return false;
            }

            if (!CompareObjects(NamePieceSurname, name.NamePieceSurname))
            {
                return false;
            }

            if (!CompareObjects(NamePieceSuffix, name.NamePieceSuffix))
            {
                return false;
            }

            if (!SourceCitations.Count.Equals(name.SourceCitations.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(name.Notes.Count))
            {
                return false;
            }

            if (!CompareObjects(HasSeperatedNamePieces, name.HasSeperatedNamePieces))
            {
                return false;
            }

            return true;
        }
    }
}
