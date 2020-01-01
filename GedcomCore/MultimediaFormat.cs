using System;
using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework
{
    /*
     * MULTIMEDIA_FORMAT: = {Size=3:4}
     * [ bmp | gif | jpeg | ole | pcx | tiff | wav ]
     * Indicates the format of the multimedia data associated with the specific GEDCOM context. This allows processors to determine whether they can process the data object. Any linked files should contain the data required, in the indicated format, to process the file data. Industry standards will emerge in this area and GEDCOM will then narrow its scope. 
     * 
     */

    [Length(3,4)]
    public enum MultimediaFormat
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("bmp")]
        BMP,

        [EnumTag("gif")]
        GIF,

        [EnumTag("jpeg")]
        JPEG,

        [EnumTag("ole")]
        OLE,

        [EnumTag("pcx")]
        PCX,

        [EnumTag("tiff")]
        TIFF,

        [EnumTag("wav")]
        WAV,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("URL")]
        URL,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("Text")]
        Text,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("W8BN")]
        W8BN,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("RTF")]
        RTF,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("PDF")]
        PDF,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("PICT")]
        PICT,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("PNTG")]
        PNTG,
        
        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("TPIC")]
        TPIC,
        
        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("AIFF")]
        AIFF,
        
        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("MOV")]
        MOV,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("8BPS")]
        PSD,

        [Obsolete("not in the standard, but found in the torture test files")]
        [EnumTag("MPEG")]
        MPEG
    }
}
