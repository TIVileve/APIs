using System;
using Vileve.Domain.Core.Events;
using Type = Vileve.Domain.Enums.Type;

namespace Vileve.Domain.Events.Property
{
    public class PropertyRegisteredEvent : Event
    {
        public PropertyRegisteredEvent(Guid id, string name, Type type, bool isRequired)
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