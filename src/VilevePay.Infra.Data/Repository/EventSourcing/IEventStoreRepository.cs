using System;
using System.Collections.Generic;
using VilevePay.Domain.Core.Events;

namespace VilevePay.Infra.Data.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}