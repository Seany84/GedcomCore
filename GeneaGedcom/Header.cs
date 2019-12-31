using System;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
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
        //private EnumLine<Language> language;

        public Header(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "HEAD";
        }

        [Tag("SOUR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public HeadSource HeadSource { get; set; }

        [Tag("DEST")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string Destination { get; set; }

        [Tag("DATE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public DateExactTime TransmissionDate { get; set; }

        [Tag("SUBM")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public string SubmitterXRef { get; set; }

        [Tag("SUBN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public string SubmissionXRef { get; set; }

        [Tag("FILE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string Filename { get; set; }

        [Tag("COPR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string CopyrightGedcomFile { get; set; }

        [Tag("GEDC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public GedcomInformation_ GedcomInformation { get; set; }

        [Tag("CHAR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        public CharacterSet_ CharacterSet { get; set; }

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
        public Language LanguageOfText { get; set; }


        [Tag("PLAC")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Place_ Place { get; set; }

        [Tag("NOTE")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ContinueableText Note { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var header = obj as Header;
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
