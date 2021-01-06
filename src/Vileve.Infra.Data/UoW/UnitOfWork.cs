using Vileve.Domain.Interfaces;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VileveContext _context;

        public UnitOfWork(VileveContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}