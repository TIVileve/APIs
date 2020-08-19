using System;
using VilevePay.Domain.Core.Events;
using Type = VilevePay.Domain.Enums.Type;

namespace VilevePay.Domain.Events.Property
{
    public class PropertyUpdatedEvent : Event
    {
        public PropertyUpdatedEvent(Guid id, string name, Type type, bool isRequired)
        {
            Id = id;
            Name = name;
            Type = type;
            IsRequired = isRequired;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public string Name { get; }
        public Type Type { get; }
        public bool IsRequired { get; }
    }
}