using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     *     +1 SOUR <APPROVED_SYSTEM_ID>  {1:1}
     *       +2 VERS <VERSION_NUMBER>  {0:1}
     *       +2 NAME <NAME_OF_PRODUCT>  {0:1}
     *       +2 CORP <NAME_OF_BUSINESS>  {0:1}
     *         +3 <<ADDRESS_STRUCTURE>>  {0:1}
     *       +2 DATA <NAME_OF_SOURCE_DATA>  {0:1}
     *         +3 DATE <PUBLICATION_DATE>  {0:1}
     *         +3 COPR <COPYRIGHT_SOURCE_DATA>  {0:1}
     * 
     * 
     */
    public partial class HeadSource : GedcomLine
    {
        private string approvedSystemId;
        private string versionNumber;
        private string productName;
        private Corporation_ corporation;
        private Data_ data;

        public HeadSource(Reporting Reporting)
            : base(Reporting)
        {
            Tag = "SOUR";
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,20)]
        public string ApprovedSystemId
        {
            get
            {
                return approvedSystemId;
            }
            set
            {
                approvedSystemId = value;
            }
        }

        [Tag("VERS")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,15)]
        public string VersionNumber
        {
            get
            {
                return versionNumber;
            }
            set
            {
                versionNumber = value;
            }
        }

        [Tag("NAME")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        [Length(1,90)]
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
            }
        }

        [Tag("CORP")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Corporation_ Corporation
        {
            get
            {
                return corporation;
            }
            set
            {
                corporation = value;
            }
        }

        [Tag("DATA")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
        public Data_ SourceData
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            HeadSource hs = obj as HeadSource;
            if (hs == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(ApprovedSystemId, hs.ApprovedSystemId))
            {
                return false;
            }

            if (!CompareObjects(VersionNumber, hs.VersionNumber))
            {
                return false;
            }

            if (!CompareObjects(ProductName, hs.ProductName))
            {
                return false;
            }

            if (!CompareObjects(Corporation, hs.Corporation))
            {
                return false;
            }

            if (!CompareObjects(SourceData, hs.SourceData))
            {
                return false;
            }

            return true;
        }
    }
}
