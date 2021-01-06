using System;
using Vileve.Domain.Core.Events;

namespace Vileve.Domain.Events.Property
{
    public class PropertyRemovedEvent : Event
    {
        public PropertyRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}