using VilevePay.Domain.Interfaces;
using VilevePay.Infra.Data.Context;

namespace VilevePay.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VilevePayContext _context;

        public UnitOfWork(VilevePayContext context)
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