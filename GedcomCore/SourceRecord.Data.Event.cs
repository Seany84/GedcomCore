using System;
using System.Collections.Generic;
using GedcomCore.Framework.Meta;
using GedcomCore.Framework.Utilities;

namespace GedcomCore.Framework
{
    public partial class SourceRecord
    {
        public partial class Data_
        {
            /* 
            *       +2 EVEN <EVENTS_RECORDED>  {0:M}
            *         +3 DATE <DATE_PERIOD>  {0:1}
            *         +3 PLAC <SOURCE_JURISDICTION_PLACE>  {0:1}
            * 
            */

            public class Event : GedcomLine
            {
                private DateValue date;

                public Event(Reporting Reporting)
                    : base(Reporting)
                {
                    Tag = "EVEN";
                }

                /// <summary>
                /// Gets or sets the EVENTS_RECORDED as a string of comma-seperated EventAttributeTypes.
                /// 
                /// The full string, including invalid events, can be retrieved via UnfilteredEventRecordedString.
                /// 
                /// All valid events can be retrieved as a list via EventsRecorded.
                /// 
                /// If invalid events where contained when setting this property, they will not be returned by it.
                /// </summary>
                [Tag("")]
                [Quantity(QuantityAttribute.PredefinedQuantities.OneRequired)]
                public string EventRecordedString
                {
                    get
                    {
                        var typeStrings = new string[EventsRecorded.Count];
                        var n = 0;

                        foreach (var type in EventsRecorded)
                        {
                            typeStrings[n++] = type.ToString(); //TODO: really ToString() ?
                        }

                        return string.Join(", ", typeStrings);
                    }
                    set
                    {
                        EventsRecorded = new List<EventAttributeType>();

                        var parts = value.Split(new[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var str in parts)
                        {
                            try
                            {
                                var type = (EventAttributeType)Enum.Parse(typeof(EventAttributeType), str);
                                EventsRecorded.Add(type);
                            }
                            catch
                            {
                                Reporting.Error($"invalid value for EVENTS_RECORDED: {str}; value omitted");
                            }
                        }

                        UnfilteredEventRecordedString = value;
                    }
                }

                /// <summary>
                /// Contains all valid events that were set using EventRecordedString
                /// </summary>
                public List<EventAttributeType> EventsRecorded { get; set; }

                /// <summary>
                /// Contains the exact string that was passed to EventRecordedString
                /// </summary>
                public string UnfilteredEventRecordedString { get; set; }

                [Tag("DATE")]
                [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
                public string DateString
                {
                    get => date.DateString;
                    set
                    {
                        date = DateValue.CreateDateValue(value, Reporting);

                        if (date.GetType() != typeof(DatePeriod))
                        {
                            throw new Exception("must be date period");
                        }
                    }
                }

                [Tag("PLAC")]
                [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
                public string SourceJurisdiction { get; set; }

                public override bool Equals(object obj)
                {
                    if (obj == null)
                    {
                        throw new ArgumentNullException();
                    }

                    var e = obj as Event;
                    if (e == null)
                    {
                        return false;
                    }

                    if (!base.Equals(obj))
                    {
                        return false;
                    }

                    if (!UnfilteredEventRecordedString.Equals(e.UnfilteredEventRecordedString))
                    {
                        return false;
                    }

                    if (!DateString.Equals(e.DateString))
                    {
                        return false;
                    }

                    if (!SourceJurisdiction.Equals(e.SourceJurisdiction))
                    {
                        return false;
                    }

                    if (!EventsRecorded.Count.Equals(e.EventsRecorded.Count))
                    {
                        return false;
                    }

                    return true;
                }
            }
        }
    }
}
