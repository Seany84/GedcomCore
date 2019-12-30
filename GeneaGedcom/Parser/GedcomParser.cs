using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;
using GeneaGedcom.Utilities;

namespace GeneaGedcom.Parser
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
        private Dictionary<string, PropertyInfo> customTagProperties;

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
        /// <param name="Stream">stream that the Gedcom is read from</param>
        /// <returns>a LineageLinkedGedcom object, that contains the read information</returns>
        public LineageLinkedGedcom Parse(Stream Stream)
        {
            var lineNumber = 0;
            objects = new Stack<object>();
            tags = new Stack<string>();
            var streamReader = new StreamReader(Stream);

            var gedcom = new LineageLinkedGedcom(reporting);

            objects.Push(gedcom);
            lastLevel = -1;

            string str;
            while ((str = streamReader.ReadLine()) != null)
            {
                try
                {
                    processLine(str, ++lineNumber);
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
        /// <param name="Line">line to process</param>
        private void processLine(string Line, int LineNumber)
        {
            object nextLine = null;
            PropertyInfo property = null;
            Type propType = null;

            var line = new Line(Line);

            if (line.TagName.StartsWith("_"))
            {
                reporting.Warn("custom tags currently not supported");
                return;
            }

            var popCount = lastLevel - line.Level;
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
                property = getPropertyInfo(line, currentObject);

                propType = property.PropertyType;

                nextLine = createNextObject(line, property);

                if (nextLine == null)
                {
                    throw new InternalException("nextLine mustn't be null");
                }
            }
            catch(Exception e)
            {
                reporting.Error($"Error while parsing line {LineNumber}: \"{Line}\": {e.Message}");

                nextLine = null; //just a dummy; we pop it when processLine() is called next time
            }
            finally
            {
                objects.Push(nextLine);
                tags.Push(line.TagName);

                lastLevel = line.Level;

                if ((property != null) && (propType != null))
                {
                    if ((QuantityUtil.GetMaximum(property) > 1) && implementsIList(propType))
                    {
                        if (nextLine != null)
                        {
                            var col = property.GetValue(currentObject, null) as IList;
                            col.Add(nextLine);
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
        private PropertyInfo getPropertyInfo(Line line, object currentObject)
        {
            try
                //try to get the property the regular way
            {
                return TagUtil.GetMember(currentObject, line.TagName, false, true);
            }
            catch (MemberNotFoundException memberException)
                //try to find a custom tag or throw a exception if none is found
            {
                var customTagPath = getCurrentPath() + "/" + memberException.Tag;
                if (!customTagProperties.ContainsKey(customTagPath))
                {
                    throw memberException;
                }

                return customTagProperties[customTagPath];
            }
        }

        /// <summary>
        /// creates the object for the given line
        /// </summary>
        /// <param name="Line">line to create a object for</param>
        /// <param name="Property">property which the line belongs to, eg the context of the line</param>
        /// <returns>a GedcomLine-object that represents the line in the given context</returns>
        private object createNextObject(Line Line, PropertyInfo Property)
        {
            checkLength(Line, Property);

            var type = TagUtil.GetLineType(Property, Line.TagName);

            object nextObject;

            if (type == typeof(string))
            {
                nextObject = Line.LineValue;
            }
            else if (type == typeof(int))
            {
                nextObject = int.Parse(Line.LineValue);
            }
            else if (type == typeof(long))
            {
                nextObject = long.Parse(Line.LineValue);
            }
            else if (type.IsEnum)
            {
                nextObject = EnumTagUtil.SelectMember(type, Line.LineValue, null);
            }
            else
            {
                nextObject = createNextGedcomLine(Line, Property, type, reporting);
            }
            
            return nextObject;
        }

        private GedcomLine createNextGedcomLine(Line Line, PropertyInfo Property, Type Type, Reporting Reporting)
        {
            var ctor = Type.GetConstructor(getCtorArgTypes(Line, Reporting));
            GedcomLine nextObject;

            if (ctor == null)
            {
                var t = getCtorArgTypes(Line, reporting);
                throw new InternalException($"no appropriate ctor found for type {Type}. {t.Length} parameters");
            }

            try
            {
                nextObject = ctor.Invoke(getCtorArgs(Line, Reporting)) as GedcomLine;
            }
            catch (Exception e)
            {
                throw new InternalException("error while creating the next object", e);
            }

            if (nextObject != null)
            {
                nextObject.Tag = Line.TagName;
            }

            if (!string.IsNullOrEmpty(Line.LineValue))
            {
                try
                {
                    var prop = TagUtil.GetMember(nextObject, "", false, true);
                    prop.SetValue(nextObject, Line.LineValue, null);
                }
                catch (MemberNotFoundException)
                {
                    reporting.Error(string.Format("line had a line-value, but the corresponding doesn't have a corresponding tag"));
                }
            }

            if (nextObject == null)
            {
                throw new InternalException(
                    $"error while creating next next object: class {Type} doesn't inherit from GeneaGedcom.GedcomLine");
            }

            return nextObject;
        }

        /// <summary>
        /// Returns the ctor-arguments to create the next object
        /// </summary>
        /// <param name="Line">the Gedcom line</param>
        /// <returns>an array with the ctor-args</returns>
        private object[] getCtorArgs(Line Line, Reporting Reporting)
        {
            var args = new List<object>();

            if (Line.XRefId != "")
            {
                args.Add(Line.XRefId);
            }

            args.Add(Reporting);

            return args.ToArray();
        }

        /// <summary>
        /// Returns the types of the ctor-args used to create the next object
        /// </summary>
        /// <param name="Line">the Gedcom line</param>
        /// <returns>an array with the types of the ctor-args</returns>
        private Type[] getCtorArgTypes(Line Line, Reporting Reporting)
        {
            var types = new List<Type>();

            if (Line.XRefId != "")
            {
                types.Add(Line.XRefId.GetType());
            }

            types.Add(Reporting.GetType());
            
            return types.ToArray();
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

        /// <summary>
        /// checks if the length of the line value matches the length assigned 
        /// with the GeneaGedcom.Meta.LengthAttribute
        /// </summary>
        /// <param name="Line">actual line value found in the file</param>
        /// <param name="Property">property which the value will be assigned to</param>
        private void checkLength(Line Line, PropertyInfo Property)
        {
            var length = Line.LineValue.Length;
            var minLength = LengthUtil.GetMinimumLength(Property);
            var maxLength = LengthUtil.GetMaximumLength(Property);

            if (length < minLength)
            {
                reporting.Warn($"line \"{Line.LineValue}\" too short. should be min {minLength} characters");
            }

            if (length > maxLength)
            {
                reporting.Warn($"line \"{Line.LineValue}\" too long. should be max {maxLength} characters");
            }
        }

        /// <summary>
        /// Registers a new custom tag
        /// </summary>
        /// <param name="PathToTag">Path to the tag in the Format /INDI/_MYTAG </param>
        /// <param name="CustomTagType">the property which the instantiated object will be assigned to</param>
        public void RegisterCustomTag(string PathToTag, PropertyInfo CustomTagProperty)
        {
            if (!CustomTagProperty.ReflectedType.IsSubclassOf(typeof(GedcomLine)))
            {
                throw new ArgumentException("CustomTagType must inherit from GeneaGedcom.GedcomLine");
            }

            var regex = new Regex(@"/\w+(/\w+/)");
            var match = regex.Match(PathToTag);
            if (!match.Success)
            {
                throw new FormatException("PathToTag " + PathToTag + " didn't match expected format");
            }

            customTagProperties.Add(PathToTag, CustomTagProperty);
        }

        /// <summary>
        /// returns the path for the currently processed object
        /// </summary>
        /// <returns>returns the path for the currently processed object</returns>
        private string getCurrentPath()
        {
            var path = "";
            foreach (var tag in tags)
            {
                path += "/" + tag;
            }
            return path;
        }
    }
}
