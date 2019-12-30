using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom
{
    class MemberNotFoundException : InternalException
    {
        private readonly string tag;

        public MemberNotFoundException(string Message, string Tag)
            : base(Message)
        {
            this.tag = Tag;
        }

        public string Tag => tag;
    }
}
