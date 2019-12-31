using System;
using System.Text.RegularExpressions;

namespace GedcomCore.Framework.Parser
{
    /// <summary>
    /// Represents one line in a Gedcom File
    /// </summary>
    class Line
    {
        /// <summary>
        /// XRef-Id of the line. Linked XRef-Ids are part of the LineValue
        /// </summary>
        private string xrefId;

        /// <summary>
        /// Tag-Name
        /// </summary>
        private string tagName;

        /// <summary>
        /// Value of the line, eg the part after the tag name
        /// </summary>
        private string lineValue;

        /// <summary>
        /// constructs an empty Line
        /// </summary>
        public Line()
        {
        }

        /// <summary>
        /// Parses the given string that represents a Gedcom line as string.
        /// </summary>
        /// <param name="Line">a line from the Gedcom file</param>
        public Line(string Line)
        {
            Line = Line.Trim();

            var regex = new Regex(@"^(\d+)\s+(@(\w+)@\s+)?(\w+)(\s+.+)?$", RegexOptions.None);

            var match = regex.Match(Line);

            if (!match.Success)
            {
                throw new FormatException("line didn't match expected format: " + Line);
            }

            Level = int.Parse(match.Groups[1].Value);
            XRefId = match.Groups[3].Value;
            TagName = match.Groups[4].Value;
            LineValue = match.Groups[5].Value;
        }

        /// <summary>
        /// The level of the line
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// the XRef-Id of the line.
        /// XRef-Ids used for linking are part of the LineValue
        /// </summary>
        public string XRefId
        {
            get => xrefId;
            set => xrefId = value.Trim();
        }

        /// <summary>
        /// Tag-Name
        /// </summary>
        public string TagName
        {
            get => tagName;
            set => tagName = value.Trim();
        }

        /// <summary>
        /// The value of the line; eg the part after the Tag-Name
        /// </summary>
        public string LineValue
        {
            get => lineValue;
            set => lineValue = value.Trim();
        }
    }
}
