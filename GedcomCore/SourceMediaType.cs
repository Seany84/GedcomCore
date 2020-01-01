using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework
{
    /* 
     * SOURCE_MEDIA_TYPE: = {Size=1:15}
     * [ audio | book | card | electronic | fiche | film | magazine |
     * manuscript | map | newspaper | photo | tombstone | video ]
     * A code, selected from one of the media classifications choices above, that indicates the type of material in which the referenced source is stored. 
     * 
     */
 
    /// <summary>
    /// A code, selected before one of the media classifications choices, that indicates the type
    /// of material in which the referenced source is stored.
    /// </summary>
    public enum SourceMediaType
    {
        [EnumTag("audio")]
        Audio,

        [EnumTag("book")]
        Book,

        [EnumTag("card")]
        Card,

        [EnumTag("electronic")]
        Electronic,

        [EnumTag("fiche")]
        Fiche,

        [EnumTag("film")]
        Film,

        [EnumTag("magazine")]
        Magazine,

        [EnumTag("manuscript")]
        Manuscript,

        [EnumTag("map")]
        Map,

        [EnumTag("newspaper")]
        Newspaper,

        [EnumTag("photo")]
        Photo,

        [EnumTag("tombstone")]
        Tombstone,

        [EnumTag("video")]
        Video,

        [UnknownEnum]
        Unknown
    }
}
