using System;

namespace GedcomCore.Framework.Entities
{
    internal class InternalException : Exception
    {
        public InternalException(string message)
            : base(message)
        {
        }

        public InternalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
