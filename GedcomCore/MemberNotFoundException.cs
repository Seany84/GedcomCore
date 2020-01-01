namespace GedcomCore.Framework
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
