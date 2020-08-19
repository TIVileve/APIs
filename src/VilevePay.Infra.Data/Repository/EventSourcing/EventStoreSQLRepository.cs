using System;
using System.Collections.Generic;
using System.Linq;
using VilevePay.Domain.Core.Events;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.Repository.EventSourcing
{
    public class EventStoreSqlRepository : IEventStoreRepository
    {
        private readonly EventStoreSqlContext _context;

        public EventStoreSqlRepository(EventStoreSqlContext context)
        {
            _context = context;
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}