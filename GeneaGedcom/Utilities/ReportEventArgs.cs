using System;
using System.Collections.Generic;
using System.Text;

namespace GeneaGedcom.Utilities
{
    public class ReportEventArgs : EventArgs
    {
        private readonly string message;

        private readonly ReportSeverity severity;

        public ReportEventArgs(string Message, ReportSeverity Severity)
        {
            message = Message;
            severity = Severity;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }

        public ReportSeverity Severity
        {
            get
            {
                return severity;
            }
        }
    }
}
