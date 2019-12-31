using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GedcomCore.Framework.Parser;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Writer
{
    /// <summary>
    /// class to write a gedcom object to a stream
    /// </summary>
    public class GedcomWriter
    {
        /// <summary>
        /// indicates, if lines should be indented
        /// </summary>
        private bool indentLines;

        private readonly Reporting reporting;

        /// <summary>
        /// creates a new GedcomWriter
        /// </summary>
        public GedcomWriter()
        {
            reporting = new Reporting();
        }

        /// <summary>
        /// write the gedcom object to the stream
        /// </summary>
        /// <param name="Gedcom">the lineage linked gedcom object that shoulde be written</param>
        /// <param name="Stream">the stream that the gedcom object should be written to</param>
        public void Write(LineageLinkedGedcom Gedcom, Stream Stream)
        {
            var streamWriter = new StreamWriter(Stream);
            streamWriter.AutoFlush = true;

            writeObject(Gedcom, "", streamWriter, -1, "");
        }

        private void writeLine(Line Line, StreamWriter StreamWriter)
        {
            if (Line.Level >= 0)
            {
                if (string.IsNullOrEmpty(Line.TagName))
                {
                    throw new ArgumentException("Line.Tag mustn't be empty");
                }

                StreamWriter.Flush();

                if (!string.IsNullOrEmpty(Line.XRefId))
                {
                    StreamWriter.WriteLine($"{lineNumber(Line.Level)} @{Line.XRefId}@ {Line.TagName} {Line.LineValue}");
                }
                else
                {
                    StreamWriter.WriteLine($"{lineNumber(Line.Level)} {Line.TagName} {Line.LineValue}");
                }
            }
        }

        /// <summary>
        /// writes the given object to the StreamWriter
        /// 
        /// throws
        /// - ArgumentException if the Line is a GedcomLine and no Tag (Line.Tag) is given
        /// </summary>
        /// <param name="Line">the gedcom line that should be written</param>
        /// <param name="Tag"></param>
        /// <param name="StreamWriter">the stream writer that the line should be written to</param>
        /// <param name="Level">the level of the line</param>
        /// <param name="Path">the path for the current object</param>
        private void writeObject(object Line, string Tag, StreamWriter StreamWriter, int Level, string Path)
        {
            var lineV = new Line();
            lineV.Level = Level;
            lineV.TagName = Tag;

            if (Line is GedcomLine)
            {
                try
                {
                    var lineValueProperty = TagUtil.GetMember(Line, "", true, false);

                    var obj = lineValueProperty.GetValue(Line, null);

                    if (obj == null)
                    {
                        lineV.LineValue = "";
                    }
                    else
                    {
                        lineV.LineValue = obj.ToString();
                    }

                    if (lineV.LineValue.Length > LengthUtil.GetMaximumLength(lineValueProperty))
                    {
                        reporting.Warn("line-value too long");
                    }

                    if (lineV.LineValue.Length < LengthUtil.GetMinimumLength(lineValueProperty))
                    {
                        reporting.Warn("line-value too short");
                    }

                }
                catch (MemberNotFoundException)
                {
                    lineV.LineValue = "";
                }

                if (Line is Record)
                {
                    lineV.XRefId = (Line as Record).XRef;
                }
            }
            else if (Line.GetType().IsEnum)
            {
                lineV.LineValue = EnumTagUtil.GetFirstTagName(Line as ValueType);
            }
            else
            {
                lineV.LineValue = Line.ToString();
            }

            writeLine(lineV, StreamWriter);

            foreach (var type in TagUtil.GetTags(Line.GetType()))
            {
                var prop = type.Key;
                var defaultTag = TagUtil.GetTagName(prop);

                if (!prop.CanRead)
                {
                    continue;
                }

                var minOccur = QuantityUtil.GetMinimum(prop);
                var maxOccur = QuantityUtil.GetMaximum(prop);

                var obj = prop.GetValue(Line, null);

                if (TagUtil.IsDefaultValue(prop, obj))
                {
                    if (minOccur > 0)
                    {
                        reporting.Warn(
                            $"object {prop.Name} in {Path} must occur at least {minOccur} time(s), but was not set");
                    }

                    continue;
                }

                // use a loop, event its just a single object to avoid redundant code
                var tmpList = implementsIList(prop.PropertyType) ? obj as IList : new[] { obj };
                var occurrences = 0;

                foreach (var o in tmpList)
                {
                    occurrences++;

                    string tag;
                    if (o is GedcomLine)
                    {
                        var line = o as GedcomLine;
                        line.Tag = getTag(type.Value, line);

                        tag = line.Tag;
                    }
                    else
                    {
                        tag = defaultTag;
                    }

                    if (tag == "")
                    {
                        continue;
                    }

                    try
                    {
                        writeObject(o, tag, StreamWriter, Level + 1, Path + "/" + tag);
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }

                if (occurrences < minOccur)
                {
                    reporting.Warn(
                        $"object {prop.Name} in {Path} must occur at least {minOccur} time(s), but occured only {occurrences} time(s)");
                }

                if (occurrences > maxOccur)
                {
                    reporting.Warn(
                        $"object {prop.Name} in {Path} can only occur {maxOccur} time(s), but occured {occurrences} time(s)");
                }
            }
        }

        /// <summary>
        /// returns the tag of the given gedcom line
        /// </summary>
        /// <param name="tags">the types with their tags of the object</param>
        /// <param name="obj">an gedcom line</param>
        /// <returns>the tag for the given gedcom line</returns>
        private string getTag(IDictionary<Type, string> tags, GedcomLine obj)
        {
            /* one property might have multiple types, each with multiple tags.
             * now we search for the correct type */
            foreach (var t in tags)
            {
                if (t.Key.Equals(obj.GetType()))
                {
                    /* t.Value == null indicates, that there are multiple tag with the same type
                     * so we take the correct Tag from the object's Tag-property */
                    var tag = t.Value == null ? obj.Tag : t.Value;

                    if (tag == "")
                    {
                        continue;
                    }

                    return tag;
                }
            }

            return "";
        }

        /// <summary>
        /// returns the line number including indentation, if its turned on
        /// </summary>
        /// <param name="Number">the line number</param>
        /// <returns>the line number including indentation, if its turned on</returns>
        private string lineNumber(int Number)
        {
            var indent = "";
            if (IndentLines)
            {
                for (var n = 0; n < Number; n++)
                {
                    indent += " ";
                }
            }

            return indent + Number.ToString();
        }

        /// <summary>
        /// indicates if lines should be indented
        /// </summary>
        public bool IndentLines
        {
            get => indentLines;
            set => indentLines = value;
        }

        /// <summary>
        /// checks if the given Type implements IList
        /// </summary>
        /// <param name="Type">type that shall be checked</param>
        /// <returns>true, if the Type implements IList, otherwise false</returns>
        private bool implementsIList(Type Type)
        {
            foreach (var iface in Type.GetInterfaces())
            {
                if (iface.Equals(typeof(IList)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
