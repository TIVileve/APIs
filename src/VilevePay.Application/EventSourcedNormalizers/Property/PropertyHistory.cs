using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using VilevePay.Domain.Core.Events;

namespace VilevePay.Application.EventSourcedNormalizers.Property
{
    public class PropertyHistory
    {
        public static IList<PropertyHistoryData> HistoryData { get; set; }

        public static IList<PropertyHistoryData> ToJavaScriptPropertyHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<PropertyHistoryData>();
            PropertyHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(p => p.Timestamp);
            var list = new List<PropertyHistoryData>();
            var last = new PropertyHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new PropertyHistoryData
                {
                    Id = !change.Id.HasValue || change.Id == last.Id ? null : change.Id,
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name ? null : change.Name,
                    Type = !change.Type.HasValue || change.Type == last.Type ? null : change.Type,
                    IsRequired = !change.IsRequired.HasValue || change.IsRequired == last.IsRequired ? null : change.IsRequired,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? null : change.Action,
                    Timestamp = change.Timestamp,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void PropertyHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<PropertyHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "PropertyRegisteredEvent":
                        historyData.Action = "Registered";
                        historyData.Who = e.User;
                        break;
                    case "PropertyUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Who = e.User;
                        break;
                    case "PropertyRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Who = e.User;
                        break;
                }

                HistoryData.Add(historyData);
            }
        }
    }
}