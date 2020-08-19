using System;
using VilevePay.Domain.Core.Events;

namespace VilevePay.Domain.Events.Property
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