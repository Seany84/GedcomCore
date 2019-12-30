using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    public partial class MultimediaRecord
    {
        /* 
 * Encoding and decoding multimedia records is described in the GEDCOM 5.5 standard
 * in Appendix E, found at http://homepages.rootsweb.com/~pmcbride/gedcom/55gcappe.htm
 */

        public class Blob_ : GedcomLine
        {
            private ContinueableText encodedText;

            private byte[] binaryData;

            private static Dictionary<byte, char> encodingTable;

            private static Dictionary<char, byte> decodingTable;

            public Blob_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "BLOB";
            }

            static Blob_()
            {
                encodingTable = new Dictionary<byte, char>();

                encodingTable.Add(0x00, '.');
                encodingTable.Add(0x01, '/');
                encodingTable.Add(0x02, '0');
                encodingTable.Add(0x03, '1');
                encodingTable.Add(0x04, '2');
                encodingTable.Add(0x05, '3');
                encodingTable.Add(0x06, '4');
                encodingTable.Add(0x07, '5');
                encodingTable.Add(0x08, '6');
                encodingTable.Add(0x09, '7');
                encodingTable.Add(0x0A, '8');
                encodingTable.Add(0x0B, '9');
                encodingTable.Add(0x0C, 'A');
                encodingTable.Add(0x0D, 'B');
                encodingTable.Add(0x0E, 'C');
                encodingTable.Add(0x0F, 'D');
                encodingTable.Add(0x10, 'E');
                encodingTable.Add(0x11, 'F');
                encodingTable.Add(0x12, 'G');
                encodingTable.Add(0x13, 'H');
                encodingTable.Add(0x14, 'I');
                encodingTable.Add(0x15, 'J');
                encodingTable.Add(0x16, 'K');
                encodingTable.Add(0x17, 'L');
                encodingTable.Add(0x18, 'M');
                encodingTable.Add(0x19, 'N');
                encodingTable.Add(0x1A, 'O');
                encodingTable.Add(0x1B, 'P');
                encodingTable.Add(0x1C, 'Q');
                encodingTable.Add(0x1D, 'R');
                encodingTable.Add(0x1E, 'S');
                encodingTable.Add(0x1F, 'T');
                encodingTable.Add(0x20, 'U');
                encodingTable.Add(0x21, 'V');
                encodingTable.Add(0x22, 'W');
                encodingTable.Add(0x23, 'X');
                encodingTable.Add(0x24, 'Y');
                encodingTable.Add(0x25, 'Z');
                encodingTable.Add(0x26, 'a');
                encodingTable.Add(0x27, 'b');
                encodingTable.Add(0x28, 'c');
                encodingTable.Add(0x29, 'd');
                encodingTable.Add(0x2A, 'e');
                encodingTable.Add(0x2B, 'f');
                encodingTable.Add(0x2C, 'g');
                encodingTable.Add(0x2D, 'h');
                encodingTable.Add(0x2E, 'i');
                encodingTable.Add(0x2F, 'j');
                encodingTable.Add(0x30, 'k');
                encodingTable.Add(0x31, 'l');
                encodingTable.Add(0x32, 'm');
                encodingTable.Add(0x33, 'n');
                encodingTable.Add(0x34, 'o');
                encodingTable.Add(0x35, 'p');
                encodingTable.Add(0x36, 'q');
                encodingTable.Add(0x37, 'r');
                encodingTable.Add(0x38, 's');
                encodingTable.Add(0x39, 't');
                encodingTable.Add(0x3A, 'u');
                encodingTable.Add(0x3B, 'v');
                encodingTable.Add(0x3C, 'w');
                encodingTable.Add(0x3D, 'x');
                encodingTable.Add(0x3E, 'y');
                encodingTable.Add(0x3F, 'z');

                decodingTable = new Dictionary<char, byte>();

                foreach (KeyValuePair<byte, char> pair in encodingTable)
                {
                    decodingTable.Add(pair.Value, pair.Key);
                }
            }

            [Tag("CONT")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneUnbounded)]
            [Length(1, 87)]
            public string Continue
            {
                set
                {
                    if (encodedText == null)
                    {
                        encodedText = new ContinueableText(value, Reporting);
                    }
                    else
                    {
                        encodedText.Continue = value;
                    }

                    //binaryData must be recalculated
                    binaryData = null;
                }
            }

            [Tag("CONT", typeof(AdditionalLine))]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneUnbounded)]
            [Length(1, 87)]
            public List<AdditionalLine> BlobLines
            {
                get
                {
                    if (encodedText == null)
                    {
                        createEncodedText();
                    }

                    string tmp = encodedText.Text;

                    return encodedText.AdditionalLines;
                }
            }

            private void createEncodedText()
            {
                string str = "";
                encodedText = new ContinueableText(Reporting);

                for (int n = 0; n < binaryData.Length; n += 3)
                {
                    byte b1 = binaryData[n];

                    byte? b2;
                    byte? b3;

                    if (binaryData.Length > n + 1)
                    {
                        b2 = binaryData[n + 1];
                        
                        if (binaryData.Length > n + 2)
                        {
                            b3 = binaryData[n + 2];
                        }
                        else
                        {
                            b3 = null;
                        }
                    }
                    else
                    {
                        b2 = null;
                        b3 = null;
                    }


                    // now we have three bytes (24 bit), which we separate in 4 6-bit values

                    //    b1    |   b2    |   b3
                    // xxxxxx xx|xxxx xxxx|xx xxxxxx
                    // 111111 00|0000 0000|00 000000 p1
                    // 000000 11|1111 0000|00 000000 p2
                    // 000000 00|0000 1111|11 000000 p3
                    // 000000 00|0000 0000|00 111111 p4

                    byte  p1 = (byte)((b1 & 0xFC) >> 2);
                    byte? p2 = (byte)((b1 & 0x3) << 4);
                    byte? p3 = null;
                    byte? p4 = null;

                    if (b2.HasValue)
                    {
                        p2 = (byte)(((b1 & 0x3) << 4) | ((b2 & 0xF0) >> 4));

                        if (b3.HasValue)
                        {
                            p3 = (byte)(((b2 & 0xF) << 2) | ((b3 & 0xC0) >> 6));
                            p4 = (byte)(b3 & 0x3F);
                        }
                    }

                    string tmp = encodingTable[p1].ToString();

                    tmp += encodingTable[p2.Value].ToString();

                    if (p3.HasValue)
                    {
                        tmp += encodingTable[p3.Value].ToString();

                        if (p4.HasValue)
                        {
                            tmp += encodingTable[p4.Value].ToString();
                        }
                    }

                    str += tmp;
                    if (str.Length >= 87 - 4)
                    // start a new line
                    {
                        encodedText.Continue = str;
                        str = "";
                    }
                }

                if (str.Length > 0)
                // add the last line
                {
                    encodedText.Continue = str;
                }
            }

            private void createBinaryData()
            {
                List<byte> bytes = new List<byte>();

                foreach (string line in encodedText.AllLines)
                {
                    for (int n = 0; n < line.Length; n += 4)
                    {
                        string part;
                        byte b1;
                        byte b2;
                        byte? b3;
                        byte? b4;

                        if (line.Length >= n + 4)
                        {
                            part = line.Substring(n, 4);
                        }
                        else
                        {
                            part = line.Substring(n);
                        }

                        b1 = decodingTable[part[0]];

                        if (part.Length > 1)
                        {
                            b2 = decodingTable[part[1]];
                            
                            if (part.Length > 2)
                            {
                                b3 = decodingTable[part[2]];

                                if (part.Length > 3)
                                {
                                    b4 = decodingTable[part[3]];
                                }
                                else
                                {
                                    b4 = 0;
                                }
                            }
                            else
                            {
                                b3 = null;
                                b4 = null;
                            }
                        }
                        else
                        {
                            b2 = 0;
                            b3 = null;
                            b4 = null;
                        }


                        //we now have 4 bytes, of which the two most significant bits are not set
                        //this reduces the real data to 4*6 = 24 bits, which we group into 3 full bytes

                        //    b1    |   b2    |   b3    |   b4
                        // xxxx xxxx|xxxx xxxx|xxxx xxxx|xxxx xxxx
                        // __11 1111|__11 0000|0000 0000|0000 0000 p1
                        // 0000 0000|0000 1111|__11 1100|0000 0000 p2
                        // 0000 0000|0000 0000|0000 0011|__11 1111 p3

                        byte? p2;
                        byte? p3;

                        byte p1 = (byte)(((b1 & 0x3F) << 2) | ((b2 & 0x30) >> 4));

                        if (b3.HasValue)
                        {
                            p2 = (byte)(((b2 & 0xF) << 4) | ((b3 & 0x3C) >> 2));

                            if (b4.HasValue)
                            {
                                p3 = (byte)(((b3 & 0x3) << 6) | (b4 & 0x3F));
                            }
                            else
                            {
                                p3 = null;
                            }
                        }
                        else
                        {
                            p2 = null;
                            p3 = null;
                        }

                        bytes.Add(p1);

                        if (p2.HasValue)
                        {
                            bytes.Add(p2.Value);

                            if (p3.HasValue)
                            {
                                bytes.Add(p3.Value);
                            }
                        }
                    }
                }

                binaryData = bytes.ToArray();
            }

            public byte[] BinaryData
            {
                get
                {
                    if (binaryData == null)
                    {
                        createBinaryData();
                    }

                    return binaryData;
                }
                set
                {
                    binaryData = value;

                    //encodedText must be recalculated
                    encodedText = null;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                {
                    throw new ArgumentNullException();
                }

                Blob_ blob = obj as Blob_;
                if (blob == null)
                {
                    return false;
                }

                if (!base.Equals(obj))
                {
                    return false;
                }

                if (!BinaryData.Equals(blob.BinaryData))
                {
                    return false;
                }

                return true;
            }
        }
    }
}
