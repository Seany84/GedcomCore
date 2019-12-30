using System;
using System.Collections.Generic;
using System.Text;
using GeneaGedcom.Meta;
using GeneaGedcom.Utilities;

namespace GeneaGedcom
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
                private string unfilteredEventRecorded;
                private DateValue date;
                private string sourceJurisdictionPlace;
                private List<EventAttributeType> eventsRecorded;

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
                        string[] typeStrings = new string[eventsRecorded.Count];
                        int n = 0;

                        foreach (EventAttributeType type in eventsRecorded)
                        {
                            typeStrings[n++] = type.ToString(); //TODO: really ToString() ?
                        }

                        return string.Join(", ", typeStrings);
                    }
                    set
                    {
                        eventsRecorded = new List<EventAttributeType>();

                        string[] parts = value.Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string str in parts)
                        {
                            try
                            {
                                EventAttributeType type = (EventAttributeType)Enum.Parse(typeof(EventAttributeType), str);
                                eventsRecorded.Add(type);
                            }
                            catch
                            {
                                Reporting.Error(string.Format("invalid value for EVENTS_RECORDED: {0}; value omitted", str));
                            }
                        }

                        unfilteredEventRecorded = value;
                    }
                }

                /// <summary>
                /// Contains all valid events that were set using EventRecordedString
                /// </summary>
                public List<EventAttributeType> EventsRecorded
                {
                    get
                    {
                        return eventsRecorded;
                    }
                    set
                    {
                        eventsRecorded = value;
                    }
                }

                /// <summary>
                /// Contains the exact string that was passed to EventRecordedString
                /// </summary>
                public string UnfilteredEventRecordedString
                {
                    get
                    {
                        return unfilteredEventRecorded;
                    }
                    set
                    {
                        unfilteredEventRecorded = value;
                    }
                }

                [Tag("DATE")]
                [Quantity(QuantityAttribute.PredefinedQuantities.OneOptional)]
                public string DateString
                {
                    get
                    {
                        return date.DateString;
                    }
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
                public string SourceJurisdiction
                {
                    get
                    {
                        return sourceJurisdictionPlace;
                    }
                    set
                    {
                        sourceJurisdictionPlace = value;
                    }
                }

                public override bool Equals(object obj)
                {
                    if (obj == null)
                    {
                        throw new ArgumentNullException();
                    }

                    Event e = obj as Event;
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
