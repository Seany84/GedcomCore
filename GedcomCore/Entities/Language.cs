using GedcomCore.Framework.Meta;

namespace GedcomCore.Framework.Entities
{
    /* 
     * LANGUAGE_ID: = {Size=1:15}
     * A table of valid latin language identification codes.
     * [Afrikaans | Albanian | Anglo-Saxon | Catalan | Catalan_Spn | Czech | Danish | Dutch | English | Esperanto | Estonian | Faroese | Finnish | French | German | Hawaiian | Hungarian | Icelandic | Indonesian | Italian | Latvian | Lithuanian | Navaho | Norwegian | Polish | Portuguese | Romanian | Serbo_Croa | Slovak | Slovene | Spanish | Swedish | Turkish | Wendic ]
     * 
     * Other languages not supported until UNICODE
     * [Amharic | Arabic | Armenian | Assamese | Belorusian | Bengali | Braj | Bulgarian | Burmese | Cantonese | Church-Slavic | Dogri | Georgian | Greek | Gujarati | Hebrew | Hindi | Japanese | Kannada | Khmer | Konkani | Korean | Lahnda | Lao | Macedonian | Maithili | Malayalam | Mandrin | Manipuri | Marathi | Mewari | Nepali | Oriya | Pahari | Pali | Panjabi | Persian | Prakrit | Pusto | Rajasthani | Russian | Sanskrit | Serb | Tagalog | Tamil | Telugu | Thai | Tibetan | Ukrainian | Urdu | Vietnamese | Yiddish ] 
     * 
     */

    public enum Language
    {
        [UnknownEnum]
        Unknown,

        [EnumTag("Afrikaans")]
        Afrikaans,

        [EnumTag("Albanian")]
        Albanian,

        [EnumTag("Anglo-Saxon")]
        AngloSaxon,

        [EnumTag("Catalan")]
        Catalan,

        [EnumTag("Catalan_Spn")]
        Catalan_Spn,

        [EnumTag("Czech")]
        Czech,

        [EnumTag("Danish")]
        Danish,

        [EnumTag("Dutch")]
        Dutch,

        [EnumTag("English")]
        English,

        [EnumTag("Esperanto")]
        Esperanto,

        [EnumTag("Estonian")]
        Estonian,

        [EnumTag("Faroese")]
        Faroese,

        [EnumTag("Finnish")]
        Finnish,

        [EnumTag("French")]
        French,

        [EnumTag("German")]
        German,

        [EnumTag("Hawaiian")]
        Hawaiian,

        [EnumTag("Hungarian")]
        Hungarian,

        [EnumTag("Icelandic")]
        Icelandic,

        [EnumTag("Indonesian")]
        Indonesian,

        [EnumTag("Italian")]
        Italian,

        [EnumTag("Latvian")]
        Latvian,

        [EnumTag("Lithuanian")]
        Lithuanian,

        [EnumTag("Navaho")]
        Navaho,

        [EnumTag("Norwegian")]
        Norwegian,

        [EnumTag("Polish")]
        Polish,

        [EnumTag("Portuguese")]
        Portuguese,

        [EnumTag("Romanian")]
        Romanian,

        [EnumTag("Serbo_Croa")]
        SerboCroatian,

        [EnumTag("Slovak")]
        Slovak,

        [EnumTag("Slovene")]
        Slovene,

        [EnumTag("Spanish")]
        Spanish,

        [EnumTag("Swedish")]
        Swedish,

        [EnumTag("Turkish")]
        Turkish,

        [EnumTag("Wendic")]
        Wendic,

        [EnumTag("Amharic")]
        Amharic,

        [EnumTag("Arabic")]
        Arabic,

        [EnumTag("Armenian")]
        Armenian,

        [EnumTag("Assamese")]
        Assamese,

        [EnumTag("Belorusian")]
        Belorusian,

        [EnumTag("Bengali")]
        Bengali,

        [EnumTag("Braj")]
        Braj,

        [EnumTag("Bulgarian")]
        Bulgarian,

        [EnumTag("Burmese")]
        Burmese,

        [EnumTag("Cantonese")]
        Cantonese,

        [EnumTag("Church-Slavic")]
        ChurchSlavic,

        [EnumTag("Dogri")]
        Dogri,

        [EnumTag("Georgian")]
        Georgian,

        [EnumTag("Greek")]
        Greek,

        [EnumTag("Gujarati")]
        Gujarati,

        [EnumTag("Hebrew")]
        Hebrew,

        [EnumTag("Hindi")]
        Hindi,

        [EnumTag("Japanese")]
        Japanese,

        [EnumTag("Kannada")]
        Kannada,

        [EnumTag("Khmer")]
        Khmer,

        [EnumTag("Konkani")]
        Konkani,

        [EnumTag("Korean")]
        Korean,

        [EnumTag("Lahnda")]
        Lahnda,

        [EnumTag("Lao")]
        Lao,

        [EnumTag("Macedonian")]
        Macedonian,

        [EnumTag("Maithili")]
        Maithili,

        [EnumTag("Malayalam")]
        Malayalam,

        [EnumTag("Mandrin")]
        Mandrin,

        [EnumTag("Manipuri")]
        Manipuri,

        [EnumTag("Marathi")]
        Marathi,

        [EnumTag("Mewari")]
        Mewari,

        [EnumTag("Nepali")]
        Nepali,

        [EnumTag("Oriya")]
        Oriya,

        [EnumTag("Pahari")]
        Pahari,

        [EnumTag("Palio")]
        Palio,

        [EnumTag("Panjabi")]
        Panjabi,

        [EnumTag("Persian")]
        Persian,

        [EnumTag("Prakrit")]
        Prakrit,

        [EnumTag("Pusto")]
        Pusto,

        [EnumTag("Rajasthani")]
        Rajasthani,

        [EnumTag("Russian")]
        Russian,

        [EnumTag("Sanskrit")]
        Sanskrit,

        [EnumTag("Serb")]
        Serb,

        [EnumTag("Tagalog")]
        Tagalog,

        [EnumTag("Tamil")]
        Tamil,

        [EnumTag("Telugu")]
        Telugu,

        [EnumTag("Thai")]
        Thai,

        [EnumTag("Tibetan")]
        Tibetan,

        [EnumTag("Ukrainian")]
        Ukrainian,

        [EnumTag("Urdu")]
        Urdu,

        [EnumTag("Vietnamese")]
        Vietnamese,

        [EnumTag("Yiddish")]
        Yiddish
    }
}
