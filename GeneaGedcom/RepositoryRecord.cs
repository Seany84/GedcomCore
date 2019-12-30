using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * REPOSITORY_RECORD: =
     * 
     *   n  @<XREF:REPO>@ REPO  {1:1}
     *     +1 NAME <NAME_OF_REPOSITORY>   {0:1}
     *     +1 <<ADDRESS_STRUCTURE>>  {0:1}
     *     +1 <<NOTE_STRUCTURE>>  {0:M}
     *     +1 REFN <USER_REFERENCE_NUMBER>  {0:M}
     *       +2 TYPE <USER_REFERENCE_TYPE>  {0:1}
     *     +1 RIN <AUTOMATED_RECORD_ID>  {0:1}
     *     +1 <<CHANGE_DATE>>  {0:1}
     * 
     */ 

    public class RepositoryRecord : Record
    {
        private string name;
        private Address address;
        private List<string> phoneNumbers;
        private List<NoteStructure> notes;
        private List<UserReference> userReferences;
        private string automatedRecordId;
        private ChangeDate changeDate;

        public RepositoryRecord(string XRef, Reporting Reporting)
            : base(XRef, Reporting)
        {
            phoneNumbers = new List<string>();
            notes = new List<NoteStructure>();
            userReferences = new List<UserReference>();

            Tag = "REPO";
        }

        [Tag("NAME")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
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

        [Tag("ADDR", typeof(Address))]
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

        [Tag("REFN", typeof(UserReference))]
        [Quantity(QuantityAttribute.PredefinedQuantities.Unbounded)]
        public List<UserReference> UserReferences
        {
            get
            {
                return userReferences;
            }
            set
            {
                userReferences = value;
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

        [Tag("CHAN", typeof(ChangeDate))]
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

            var rr = obj as RepositoryRecord;
            if (rr == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Name, rr.Name))
            {
                return false;
            }

            if (!CompareObjects(Address, rr.Address))
            {
                return false;
            }

            if (!PhoneNumbers.Count.Equals(rr.PhoneNumbers.Count))
            {
                return false;
            }

            if (!Notes.Count.Equals(rr.Notes.Count))
            {
                return false;
            }

            if (!UserReferences.Count.Equals(rr.UserReferences.Count))
            {
                return false;
            }

            if (!CompareObjects(AutomatedRecordId, rr.AutomatedRecordId))
            {
                return false;
            }

            if (!CompareObjects(ChangeDate, rr.ChangeDate))
            {
                return false;
            }

            return true;
        }
    }
}
