using System;

namespace GedcomCore.Framework.Utilities
{
    public class Reporting
    {
        public event EventHandler<ReportEventArgs> LogEntry = delegate { };

        public void Debug(string Message)
        {
            LogEntry(this, new ReportEventArgs(Message, ReportSeverity.Debug));
        }

        public void Warn(string Message)
        {
            LogEntry(this, new ReportEventArgs(Message, ReportSeverity.Warning));
        }

        public void Error(string Message)
        {
            LogEntry(this, new ReportEventArgs(Message, ReportSeverity.Error));
        }
    }
}
