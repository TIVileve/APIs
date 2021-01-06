using System;
using System.Collections.Generic;
using Vileve.Domain.Core.Events;

namespace Vileve.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}