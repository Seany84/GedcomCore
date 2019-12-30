using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * SUBMITTER_RECORD: =
     * 
     *   n  @<XREF:SUBM>@   SUBM {1:1}
     *     +1 NAME <SUBMITTER_NAME>  {1:1}
     *     +1 <<ADDRESS_STRUCTURE>>  {0:1}
     *     +1 <<MULTIMEDIA_LINK>>  {0:M}
     *     +1 LANG <LANGUAGE_PREFERENCE>  {0:3}
     *     +1 RFN <SUBMITTER_REGISTERED_RFN>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     * 
     * The submitter record identifies an individual or organization that contributed information contained in the GEDCOM transmission. All records in the transmission are assumed to be submitted by the SUBMITTER referenced in the HEADer, unless a SUBMitter reference inside a specific record points at a different SUBMITTER record. 
     * 
     */ 

    public class SubmitterRecord : Record
    {
        private string name;
        private Address address;
        private List<string> phoneNumbers;
        private List<MultimediaLink> multimedia;
        private List<Language> languages;
        private string submitterRegisteredRFN;
        private string automatedRecordId;
        private ChangeDate changeDate;

        public SubmitterRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            phoneNumbers = new List<string>();
            multimedia = new List<MultimediaLink>();
            languages = new List<Language>();

            Tag = "SUBM";
        }

        [Tag("NAME")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,60)]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        [Tag("ADDR")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        [Tag("PHON", typeof(string))]
        [Quantity(0, 3)]
        [Length(1, 25)]
        public List<string> PhoneNumbers
        {
            get
            {
                return phoneNumbers;
            }
            set
            {
                phoneNumbers = value;
            }
        }

        [Tag("OBJE", typeof(MultimediaLink))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<MultimediaLink> Multimedia
        {
            get
            {
                return multimedia;
            }
            set
            {
                multimedia = value;
            }
        }

        [Tag("LANG", typeof(Language))]
        [Quantity(0,3)]
        public List<Language> LanguagePreference
        {
            get
            {
                return languages;
            }
            set
            {
                languages = value;
            }
        }

        [Tag("RFN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,30)]
        public string SubmitterRegisteredRFN
        {
            get
            {
                return submitterRegisteredRFN;
            }
            set
            {
                submitterRegisteredRFN = value;
            }
        }

        [Tag("RIN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,12)]
        public string AutomatedRecordId
        {
            get
            {
                return automatedRecordId;
            }
            set
            {
                automatedRecordId = value;
            }
        }

        [Tag("CHAN")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public ChangeDate ChangeDate
        {
            get
            {
                return changeDate;
            }
            set
            {
                changeDate = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            SubmitterRecord subm = obj as SubmitterRecord;
            if (subm == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Name, subm.Name))
            {
                return false;
            }

            if (!CompareObjects(Address, subm.Address))
            {
                return false;
            }

            if (!PhoneNumbers.Count.Equals(subm.PhoneNumbers.Count))
            {
                return false;
            }

            if (!Multimedia.Count.Equals(subm.Multimedia.Count))
            {
                return false;
            }

            if (!LanguagePreference.Count.Equals(subm.LanguagePreference.Count))
            {
                return false;
            }

            if (!CompareObjects(SubmitterRegisteredRFN, subm.SubmitterRegisteredRFN))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, subm.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, subm.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
