using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom
{
    class MemberNotFoundException : InternalException
    {
        public MemberNotFoundException(string Message, string Tag)
            : base(Message)
        {
            this.Tag = Tag;
        }

        public string Tag { get; }
    }
}
