using System;
using System.Collections.Generic;
using GedcomCore.Framework.Entities.Family;
using GedcomCore.Framework.Entities.Individual;
using GedcomCore.Framework.Entities.Multimedia;
using GedcomCore.Framework.Entities.Source;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities
{
    /* 
     * LINEAGE_LINKED_GEDCOM: =
     * This is a model of the lineage-linked GEDCOM structure for submitting data to other lineage-linked GEDCOM processing systems. A header and a trailer record are required, and they can enclose any number of data records. Tags from Appendix A must be used in the same context as shown in the following form. User defined tags (see <NEW_TAG>) are discouraged but when used must begin with an under-score.
     * 
     * 0 <<HEADER>> {1:1}
     * 0 <<SUBMISSION_RECORD>> {0:1}
     * 0 <<RECORD>> {1:M}
     * 0 TRLR {1:1} 
     * 
     */

    /// <summary>
    /// This is a model of the lineage-linked GEDCOM structure for submitting data after other
    /// lineage-linked GEDCOM processing systems. A header and a trailer record are required, 
    /// and they can enclose any number of data records.
    /// </summary>
    public class LineageLinkedGedcom : GedcomLine
    {
        public LineageLinkedGedcom(Reporting Reporting)
            : base(Reporting)
        {
            Records = new List<Record>();
        }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Tag("HEAD")]
        public Header.Header Header { get; set; }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Tag("SUBN")]
        public SubmissionRecord SubmissionRecord { get; set; }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneUnbounded)]
        [Tag("FAM", typeof(FamilyRecord))]
        [Tag("INDI", typeof(IndividualRecord))]
        [Tag("OBJE", typeof(MultimediaRecord))]
        [Tag("NOTE", typeof(NoteRecord))]
        [Tag("REPO", typeof(RepositoryRecord))]
        [Tag("SOUR", typeof(SourceRecord))]
        [Tag("SUBM", typeof(SubmitterRecord))]
        public List<Record> Records { get; set; }

        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Tag("TRLR")]
        public Trailer Trailer { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            if (!(obj is LineageLinkedGedcom gedcom))
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(Header, gedcom.Header))
            {
                return false;
            }

            if (!CompareObjects(SubmissionRecord, gedcom.SubmissionRecord))
            {
                return false;
            }

            if (!Records.Count.Equals(gedcom.Records.Count))
            {
                return false;
            }

            if (!CompareObjects(Trailer, gedcom.Trailer))
            {
                return false;
            }

            return true;
        }
    }
}
