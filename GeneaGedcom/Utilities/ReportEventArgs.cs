using System;

namespace GedcomCore.Framework.Utilities
{
    public class ReportEventArgs : EventArgs
    {
        public ReportEventArgs(string Message, ReportSeverity Severity)
        {
            this.Message = Message;
            this.Severity = Severity;
        }

        public string Message { get; }

        public ReportSeverity Severity { get; }
    }
}
