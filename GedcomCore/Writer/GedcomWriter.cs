using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GedcomCore.Framework.Entities;
using GedcomCore.Framework.Parser;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Writer
{
    /// <summary>
    /// class to write a gedcom object to a stream
    /// </summary>
    public class GedcomWriter
    {
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
        /// <param name="gedcom">the lineage linked gedcom object that shoulde be written</param>
        /// <param name="stream">the stream that the gedcom object should be written to</param>
        public void Write(LineageLinkedGedcom gedcom, Stream stream)
        {
            var streamWriter = new StreamWriter(stream) {AutoFlush = true};

            WriteObject(gedcom, "", streamWriter, -1, "");
        }

        private void WriteLine(Line line, StreamWriter streamWriter)
        {
            if (line.Level >= 0)
            {
                if (string.IsNullOrEmpty(line.TagName))
                {
                    throw new ArgumentException("gedcomLine.tagStr mustn't be empty");
                }

                streamWriter.Flush();

                streamWriter.WriteLine(!string.IsNullOrEmpty(line.XRefId)
                    ? $"{LineNumber(line.Level)} @{line.XRefId}@ {line.TagName} {line.LineValue}"
                    : $"{LineNumber(line.Level)} {line.TagName} {line.LineValue}");
            }
        }

        /// <summary>
        /// writes the given object to the StreamWriter
        /// 
        /// throws
        /// - ArgumentException if the gedcomLine is a GedcomLine and no tagStr (gedcomLine.tagStr) is given
        /// </summary>
        /// <param name="gedcomLine">the gedcom line that should be written</param>
        /// <param name="tagStr"></param>
        /// <param name="streamWriter">the stream writer that the line should be written to</param>
        /// <param name="level">the level of the line</param>
        /// <param name="path">the path for the current object</param>
        private void WriteObject(object gedcomLine, string tagStr, StreamWriter streamWriter, int level, string path)
        {
            var lineV = new Line {Level = level, TagName = tagStr};

            if (gedcomLine is GedcomLine)
            {
                try
                {
                    var lineValueProperty = TagUtil.GetMember(gedcomLine, "", true, false);

                    var obj = lineValueProperty.GetValue(gedcomLine, null);

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

                if (gedcomLine is Record record)
                {
                    lineV.XRefId = record.XRef;
                }
            }
            else if (gedcomLine.GetType().IsEnum)
            {
                lineV.LineValue = EnumTagUtil.GetFirstTagName(gedcomLine as ValueType);
            }
            else
            {
                lineV.LineValue = gedcomLine.ToString();
            }

            WriteLine(lineV, streamWriter);

            foreach (var type in TagUtil.GetTags(gedcomLine.GetType()))
            {
                var prop = type.Key;
                var defaultTag = TagUtil.GetTagName(prop);

                if (!prop.CanRead)
                {
                    continue;
                }

                var minOccur = QuantityUtil.GetMinimum(prop);
                var maxOccur = QuantityUtil.GetMaximum(prop);

                var obj = prop.GetValue(gedcomLine, null);

                if (TagUtil.IsDefaultValue(prop, obj))
                {
                    if (minOccur > 0)
                    {
                        reporting.Warn(
                            $"object {prop.Name} in {path} must occur at least {minOccur} time(s), but was not set");
                    }

                    continue;
                }

                // use a loop, event its just a single object to avoid redundant code
                var tmpList = ImplementsIList(prop.PropertyType) ? obj as IList : new[] { obj };
                var occurrences = 0;

                foreach (var o in tmpList)
                {
                    occurrences++;

                    string tag;
                    if (o is GedcomLine)
                    {
                        var line = o as GedcomLine;
                        line.Tag = GetTag(type.Value, line);

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
                        WriteObject(o, tag, streamWriter, level + 1, path + "/" + tag);
                    }
                    catch(Exception)
                    {
                        continue;
                    }
                }

                if (occurrences < minOccur)
                {
                    reporting.Warn(
                        $"object {prop.Name} in {path} must occur at least {minOccur} time(s), but occured only {occurrences} time(s)");
                }

                if (occurrences > maxOccur)
                {
                    reporting.Warn(
                        $"object {prop.Name} in {path} can only occur {maxOccur} time(s), but occured {occurrences} time(s)");
                }
            }
        }

        /// <summary>
        /// returns the tag of the given gedcom line
        /// </summary>
        /// <param name="tags">the types with their tags of the object</param>
        /// <param name="obj">an gedcom line</param>
        /// <returns>the tag for the given gedcom line</returns>
        private static string GetTag(IDictionary<Type, string> tags, GedcomLine obj)
        {
            /* one property might have multiple types, each with multiple tags.
             * now we search for the correct type */
            foreach (var (key, value) in tags)
            {
                if (key == obj.GetType())
                {
                    /* t.Value == null indicates, that there are multiple tag with the same type
                     * so we take the correct tagStr from the object's tagStr-property */
                    var tag = value ?? obj.Tag;

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
        /// <param name="number">the line number</param>
        /// <returns>the line number including indentation, if its turned on</returns>
        private string LineNumber(int number)
        {
            var indent = "";
            if (IndentLines)
            {
                for (var n = 0; n < number; n++)
                {
                    indent += " ";
                }
            }

            return indent + number.ToString();
        }

        /// <summary>
        /// indicates if lines should be indented
        /// </summary>
        public bool IndentLines { get; set; }

        /// <summary>
        /// checks if the given Type implements IList
        /// </summary>
        /// <param name="Type">type that shall be checked</param>
        /// <returns>true, if the Type implements IList, otherwise false</returns>
        private static bool ImplementsIList(Type Type)
        {
            return Type.GetInterfaces().Any(iface => iface == typeof(IList));
        }
    }
}
