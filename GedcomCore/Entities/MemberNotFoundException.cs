namespace GedcomCore.Framework.Entities
{
    internal class MemberNotFoundException : InternalException
    {
        public MemberNotFoundException(string message, string tag)
            : base(message)
        {
            Tag = tag;
        }

        public string Tag { get; }
    }
}
