using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
{
    /* 
     * ROLE_IN_EVENT: = {Size=1:15}
     * [ CHIL | HUSB | WIFE | MOTH | FATH | SPOU | ( <ROLE_DESCRIPTOR> ) ]
     * Indicates what role this person played in the event that is being cited in this context. For example, if you cite a child's birth record as the source of the mother's name, the value for this field is "MOTH." If you describe the groom of a marriage, the role is "HUSB." If the role is something different than one of the six relationship role tags listed above then enclose the role name within matching parentheses. 
     * 
     */ 

    public class RoleInEvent : GedcomLine
    {
        private string text;
        private PredefinedRoles predefinedRole;

        public RoleInEvent(Reporting Reporting)
            : base(Reporting)
        {
        }

        [Tag("")]
        [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
        [Length(1,15)]
        public string Text
        {
            get => text;
            set
            {
                text = value;

                predefinedRole = (PredefinedRoles)EnumTagUtil.SelectMember(typeof(PredefinedRoles), value, predefinedRole, PredefinedRoles.Other);
            }
        }

        public PredefinedRoles PredefinedRole
        {
            get => predefinedRole;
            set
            {
                if (predefinedRole != value)
                {
                    predefinedRole = value;

                    if (value != PredefinedRoles.Other)
                    {
                        text = EnumTagUtil.GetFirstTagName(value);
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            var role = obj as RoleInEvent;
            if (role == null)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            if (!CompareObjects(PredefinedRole, role.PredefinedRole))
            {
                return false;
            }

            if (!CompareObjects(Text, role.Text))
            {
                return false;
            }

            return true;
        }

        public enum PredefinedRoles
        {
            [EnumTag("CHIL")]
            Child,

            [EnumTag("HUSB")]
            Husband,

            [EnumTag("WIFE")]
            Wife,

            [EnumTag("MOTH")]
            Mother,

            [EnumTag("FATH")]
            Father,

            [EnumTag("SPOU")]
            Spouse,

            [UnknownEnum]
            Other
        }
    }
}
