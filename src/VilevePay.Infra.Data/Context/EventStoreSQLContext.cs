using Microsoft.EntityFrameworkCore;
using VilevePay.Domain.Core.Events;
using VilevePay.Infra.Data.Mappings;

namespace VilevePay.Infra.Data.Context
{
    public class EventStoreSqlContext : DbContext
    {
        public EventStoreSqlContext(DbContextOptions<EventStoreSqlContext> options)
            : base(options)
        {
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}