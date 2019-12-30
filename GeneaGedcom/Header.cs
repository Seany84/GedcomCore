using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * HEADER: =
     * 
     *   n HEAD          {1:1}
     *     +1 SOUR <APPROVED_SYSTEM_ID>  {1:1}
     *       +2 VERS <VERSION_NUMBER>  {0:1}
     *       +2 NAME <NAME_OF_PRODUCT>  {0:1}
     *       +2 CORP <NAME_OF_BUSINESS>  {0:1}
     *         +3 <<ADDRESS_STRUCTURE>>  {0:1}
     *       +2 DATA <NAME_OF_SOURCE_DATA>  {0:1}
     *         +3 DATE <PUBLICATION_DATE>  {0:1}
     *         +3 COPR <COPYRIGHT_SOURCE_DATA>  {0:1}
     *     +1 DEST <RECEIVING_SYSTEM_NAME>  {0:1*}
     *     +1 DATE <TRANSMISSION_DATE>  {0:1}
     *       +2 TIME <TIME_VALUE>  {0:1}
     *     +1 SUBM @<XREF:SUBM>@  {1:1}
     *     +1 SUBN @<XREF:SUBN>@  {0:1}
     *     +1 FILE <FILE_NAME>  {0:1}
     *     +1 COPR <COPYRIGHT_GEDCOM_FILE>  {0:1}
     *     +1 GEDC        {1:1}
     *       +2 VERS <VERSION_NUMBER>  {1:1}
     *       +2 FORM <GEDCOM_FORM>  {1:1}
     *     +1 CHAR <CHARACTER_SET>  {1:1}
     *       +2 VERS <VERSION_NUMBER>  {0:1}
     *     +1 LANG <LANGUAGE_OF_TEXT>  {0:1}
     *     +1 PLAC        {0:1}
     *       +2 FORM <PLACE_HIERARCHY>  {1:1}
     *     +1 NOTE <GEDCOM_CONTENT_DESCRIPTION>  {0:1}
     *       +2 [CONT|CONC] <GEDCOM_CONTENT_DESCRIPTION>  {0:M}
     * 
     * 
     * * NOTE:
     * Submissions to the Family History Department for Ancestral File submission or for clearing temple ordinances must use a DESTination of ANSTFILE or TempleReady .
     * 
     * The header structure provides information about the entire transmission. The SOURce system name identifies which system sent the data. The DESTination system name identifies the intended receiving system.
     * 
     * Additional GEDCOM standards will be produced in the future to reflect GEDCOM expansion and maturity. This requires the reading program to make sure it can read the GEDC.VERS and the GEDC.FORM values to insure proper readability. The CHAR tag is required. All character codes greater than 0x7F must be converted to ANSEL . (See Chapter 3.) 
     * 
     */
 
    public partial class Header : GedcomLine
    {
        private HeadSource source;
        private string destination;
        private DateExactTime transmissionDate;
        private string submitterXRef;
        private string submissionXref;
        private string file;
        private string copyrightGedcomFile;
        private GedcomInformation_ gedcomInformation;
        private CharacterSet_ characterSet;
        //private EnumLine<Language> language;
        private Language language;
        private Place_ place;
        private ContinueableText note;

        public Header(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "HEAD";
        }

        [Tag("SOUR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public HeadSource HeadSource
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }

        [Tag("DEST")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
            }
        }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public DateExactTime TransmissionDate
        {
            get
            {
                return transmissionDate;
            }
            set
            {
                transmissionDate = value;
            }
        }

        [Tag("SUBM")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string SubmitterXRef
        {
            get
            {
                return submitterXRef;
            }
            set
            {
                submitterXRef = value;
            }
        }

        [Tag("SUBN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SubmissionXRef
        {
            get
            {
                return submissionXref;
            }
            set
            {
                submissionXref = value;
            }
        }

        [Tag("FILE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Filename
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }

        [Tag("COPR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string CopyrightGedcomFile
        {
            get
            {
                return copyrightGedcomFile;
            }
            set
            {
                copyrightGedcomFile = value;
            }
        }

        [Tag("GEDC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public GedcomInformation_ GedcomInformation
        {
            get
            {
                return gedcomInformation;
            }
            set
            {
                gedcomInformation = value;
            }
        }

        [Tag("CHAR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public CharacterSet_ CharacterSet
        {
            get
            {
                return characterSet;
            }
            set
            {
                characterSet = value;
            }
        }

        /*
        [Tag("LANG")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public EnumLine<Language> LanguageOfText
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }
         * */

        [Tag("LANG", Language.Unknown)]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Language LanguageOfText
        {
            get
            {
                return language;
            }
            set
            {
                language = value;
            }
        }


        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Place_ Place
        {
            get
            {
                return place;
            }
            set
            {
                place = value;
            }
        }

        [Tag("NOTE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText Note
        {
            get
            {
                return note;
            }
            set
            {
                note = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            Header header = obj as Header;
            if (header == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(HeadSource, header.HeadSource))
            {
                return false;
            }

            if (!CompareObjects(Destination, header.Destination))
            {
                return false;
            }

            if (!CompareObjects(TransmissionDate, header.TransmissionDate))
            {
                return false;
            }

            if (!CompareObjects(SubmitterXRef, header.SubmitterXRef))
            {
                return false;
            }

            if (!CompareObjects(SubmissionXRef, header.SubmissionXRef))
            {
                return false;
            }

            if (!CompareObjects(Filename, header.Filename))
            {
                return false;
            }

            if (!CompareObjects(CopyrightGedcomFile, header.CopyrightGedcomFile))
            {
                return false;
            }

            if (!CompareObjects(GedcomInformation, header.GedcomInformation))
            {
                return false;
            }

            if (!CompareObjects(CharacterSet, header.CharacterSet))
            {
                return false;
            }

            if (!CompareObjects(LanguageOfText, header.LanguageOfText))
            {
                return false;
            }

            if (!CompareObjects(Place, header.Place))
            {
                return false;
            }

            if (!CompareObjects(Note, header.Note))
            {
                return false;
            }

            return true;
        }
    }
}
