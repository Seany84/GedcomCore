using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Entities.Header
{
    public partial class Header
    {
        /* 
 *     +1 CHAR <CHARACTER_SET>  {1:1}
 *       +2 VERS <VERSION_NUMBER>  {0:1}
 * 
 */

        public partial class CharacterSet_ : GedcomLine
        {
            public CharacterSet_(Reporting Reporting)
                : base(Reporting)
            {
                Tag = "CHAR";
            }

            [Tag("")]
            //TODO: length
            public string LineValue
            {
                get => EnumTagUtil.GetFirstTagName(Set);
                set => Set = (Set_)EnumTagUtil.SelectMember(typeof(Set_), value, null);
            }

            [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
            public Set_ Set { get; set; }

            [Tag("VERS")]
            [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
            public string Version { get; set; }
        }
    }
}
