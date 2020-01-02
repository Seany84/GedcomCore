using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using GedcomCore.Framework.Entities;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework.Parser
{
    /// <summary>
    /// class for parsing Gedcom files
    /// </summary>
    public class GedcomParser
    {
        /// <summary>
        /// objects of currently processed objects. when processing of an objects starts, 
        /// it is pushed. when the processing is finished, it's popped
        /// </summary>
        private Stack<object> objects;

        /// <summary>
        /// tags of the currently processed objects. The function getCurrentPath() returns
        /// the path to the current object which is found on top of the objects-Stack.
        /// </summary>
        private Stack<string> tags;

        /// <summary>
        /// level of the last line that was processed. The object that represents the whole
        /// Gedcom file (the LineageLinkedGedcom-object) has the level -1
        /// </summary>
        private int lastLevel;

        /// <summary>
        /// Contains the types that are used for representing custom tags.
        /// key: path to the part of the file where the custom tag may be placed (eg /INDI/_TEST)
        /// value: that property that the created object will be assigned to
        /// </summary>
        private readonly Dictionary<string, PropertyInfo> customTagProperties;

        private readonly Reporting reporting;

        /// <summary>
        /// creates a new GedcomParser
        /// </summary>
        public GedcomParser()
        {
            customTagProperties = new Dictionary<string, PropertyInfo>();
            reporting = new Reporting();
        }

        /// <summary>
        /// Parses the Gedcom file that is read from the stream
        /// </summary>
        /// <param name="stream">stream that the Gedcom is read from</param>
        /// <returns>a LineageLinkedGedcom object, that contains the read information</returns>
        public LineageLinkedGedcom Parse(Stream stream)
        {
            var lineNumber = 0;
            objects = new Stack<object>();
            tags = new Stack<string>();

            var gedcom = new LineageLinkedGedcom(reporting);

            objects.Push(gedcom);
            lastLevel = -1;

            using (var streamReader = new StreamReader(stream))
            {
                string str;
                while ((str = streamReader.ReadLine()) != null)
                    try
                    {
                        ProcessLine(str, ++lineNumber);
                    }
                    catch (NullReferenceException)
                    {
                        throw;
                    }
                    catch (TargetInvocationException)
                    {
                    }
                    catch (Exception e)
                    {
                        reporting.Error(e.Message);
                    }
            }

            return gedcom;
        }

        /// <summary>
        /// processes a Gedcom line
        /// </summary>
        /// <param name="line">line to process</param>
        /// <param name="lineNumber"></param>
        private void ProcessLine(string line, int lineNumber)
        {
            object nextLine = null;
            PropertyInfo property = null;
            Type propType = null;

            var lineObj = new Line(line);

            if (lineObj.TagName.StartsWith("_"))
            {
                reporting.Warn("custom tags currently not supported");
                return;
            }

            var popCount = lastLevel - lineObj.Level;
            for (var n = 0; n <= popCount; n++)
            {
                objects.Pop();
            }

            var currentObject = objects.Peek();

            if (currentObject == null)
            //dummy-object pushed in the last call of processLine(); we can't proceed
            {
                return;
            }

            try
            {
                property = GetPropertyInfo(lineObj, currentObject);

                propType = property.PropertyType;

                nextLine = CreateNextObject(lineObj, property);

                if (nextLine == null)
                {
                    throw new InternalException("nextLine mustn't be null");
                }
            }
            catch (Exception e)
            {
                reporting.Error($"Error while parsing line {lineNumber}: \"{line}\": {e.Message}");

                nextLine = null; //just a dummy; we pop it when processLine() is called next time
            }
            finally
            {
                objects.Push(nextLine);
                tags.Push(lineObj.TagName);

                lastLevel = lineObj.Level;

                if ((property != null) && (propType != null))
                {
                    if ((QuantityUtil.GetMaximum(property) > 1) && ImplementsIList(propType))
                    {
                        if (nextLine != null)
                        {
                            var col = property.GetValue(currentObject, null) as IList;
                            col?.Add(nextLine);
                        }
                    }
                    else
                    {
                        property.SetValue(currentObject, nextLine, null);
                    }
                }
            }
        }

        /// <summary>
        /// returns the property of the currentObject that the given line belongs to.
        /// throws a GeneaGedcom.MemberNotFoundException if no suitable property is found.
        /// </summary>
        /// <param name="line">line that is a subline of the current object</param>
        /// <param name="currentObject">object that is currently processed</param>
        /// <returns>the property info that belongs to the given arguments</returns>
        private PropertyInfo GetPropertyInfo(Line line, object currentObject)
        {
            try
            //try to get the property the regular way
            {
                return TagUtil.GetMember(currentObject, line.TagName, false, true);
            }
            catch (MemberNotFoundException memberException)
            //try to find a custom tag or throw a exception if none is found
            {
                var customTagPath = GetCurrentPath() + "/" + memberException.Tag;
                if (!customTagProperties.ContainsKey(customTagPath))
                {
                    throw;
                }

                return customTagProperties[customTagPath];
            }
        }

        /// <summary>
        /// creates the object for the given line
        /// </summary>
        /// <param name="line">line to create a object for</param>
        /// <param name="property">property which the line belongs to, eg the context of the line</param>
        /// <returns>a GedcomLine-object that represents the line in the given context</returns>
        private object CreateNextObject(Line line, PropertyInfo property)
        {
            CheckLength(line, property);

            var type = TagUtil.GetLineType(property, line.TagName);

            object nextObject;

            if (type == typeof(string))
            {
                nextObject = line.LineValue;
            }
            else if (type == typeof(int))
            {
                nextObject = int.Parse(line.LineValue);
            }
            else if (type == typeof(long))
            {
                nextObject = long.Parse(line.LineValue);
            }
            else if (type.IsEnum)
            {
                nextObject = EnumTagUtil.SelectMember(type, line.LineValue, null);
            }
            else
            {
                nextObject = CreateNextGedcomLine(line, property, type, reporting);
            }

            return nextObject;
        }

        private GedcomLine CreateNextGedcomLine(Line line, PropertyInfo property, Type type, Reporting report)
        {
            var ctor = type.GetConstructor(GetCtorArgTypes(line, report));
            GedcomLine nextObject;

            if (ctor == null)
            {
                var t = GetCtorArgTypes(line, this.reporting);
                throw new InternalException($"no appropriate ctor found for type {type}. {t.Length} parameters");
            }

            try
            {
                nextObject = ctor.Invoke(GetCtorArgs(line, report)) as GedcomLine;
            }
            catch (Exception e)
            {
                throw new InternalException("error while creating the next object", e);
            }

            if (nextObject != null)
            {
                nextObject.Tag = line.TagName;
            }

            if (!string.IsNullOrEmpty(line.LineValue))
            {
                try
                {
                    var prop = TagUtil.GetMember(nextObject, "", false, true);
                    prop.SetValue(nextObject, line.LineValue, null);
                }
                catch (MemberNotFoundException)
                {
                    this.reporting.Error(string.Format("line had a line-value, but the corresponding doesn't have a corresponding tag"));
                }
            }

            if (nextObject == null)
            {
                throw new InternalException(
                    $"error while creating next next object: class {type} doesn't inherit from GeneaGedcom.GedcomLine");
            }

            return nextObject;
        }

        /// <summary>
        /// Returns the ctor-arguments to create the next object
        /// </summary>
        /// <param name="line">the Gedcom line</param>
        /// <returns>an array with the ctor-args</returns>
        private object[] GetCtorArgs(Line line, Reporting report)
        {
            var args = new List<object>();

            if (line != null && line.XRefId != "")
            {
                args.Add(line.XRefId);
            }

            args.Add(report);

            return args.ToArray();
        }

        /// <summary>
        /// Returns the types of the ctor-args used to create the next object
        /// </summary>
        /// <param name="line">the Gedcom line</param>
        /// <param name="report"></param>
        /// <returns>an array with the types of the ctor-args</returns>
        private static Type[] GetCtorArgTypes(Line line, Reporting report)
        {
            var types = new List<Type>();

            if (line != null && line.XRefId != "")
            {
                types.Add(line.XRefId.GetType());
            }

            types.Add(report.GetType());

            return types.ToArray();
        }

        /// <summary>
        /// checks if the given Type implements IList
        /// </summary>
        /// <param name="type">type that shall be checked</param>
        /// <returns>true, if the Type implements IList, otherwise false</returns>
        private static bool ImplementsIList(Type type)
        {
            return type.GetInterfaces().Any(face => face == typeof(IList));
        }

        /// <summary>
        /// checks if the length of the line value matches the length assigned 
        /// with the GeneaGedcom.Meta.LengthAttribute
        /// </summary>
        /// <param name="line">actual line value found in the file</param>
        /// <param name="property">property which the value will be assigned to</param>
        private void CheckLength(Line line, PropertyInfo property)
        {
            var length = line.LineValue.Length;
            var minLength = LengthUtil.GetMinimumLength(property);
            var maxLength = LengthUtil.GetMaximumLength(property);

            if (length < minLength)
            {
                reporting.Warn($"line \"{line.LineValue}\" too short. should be min {minLength} characters");
            }

            if (length > maxLength)
            {
                reporting.Warn($"line \"{line.LineValue}\" too long. should be max {maxLength} characters");
            }
        }

        /// <summary>
        /// Registers a new custom tag
        /// </summary>
        /// <param name="pathToTag">Path to the tag in the Format /INDI/_MYTAG </param>
        /// <param name="customTagProperty">the property which the instantiated object will be assigned to</param>
        public void RegisterCustomTag(string pathToTag, PropertyInfo customTagProperty)
        {
            if (customTagProperty.ReflectedType != null && !customTagProperty.ReflectedType.IsSubclassOf(typeof(GedcomLine)))
            {
                throw new ArgumentException("CustomTagType must inherit from GeneaGedcom.GedcomLine");
            }

            var regex = new Regex(@"/\w+(/\w+/)");
            var match = regex.Match(pathToTag);
            if (!match.Success)
            {
                throw new FormatException("PathToTag " + pathToTag + " didn't match expected format");
            }

            customTagProperties.Add(pathToTag, customTagProperty);
        }

        /// <summary>
        /// returns the path for the currently processed object
        /// </summary>
        /// <returns>returns the path for the currently processed object</returns>
        private string GetCurrentPath()
        {
            return tags.Aggregate("", (current, tag) => current + ("/" + tag));
        }
    }
}
