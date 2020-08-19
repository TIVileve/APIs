using System;
using System.Linq;
using System.Linq.Expressions;
using VilevePay.Domain.Core.Models;

namespace VilevePay.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(Guid id);

        IQueryable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        void Add(TEntity obj);
        void Update(TEntity obj);
        void Remove(Guid id);
        int SaveChanges();
    }
}