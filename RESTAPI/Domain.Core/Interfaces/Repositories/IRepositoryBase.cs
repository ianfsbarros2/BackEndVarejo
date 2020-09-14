using System.Collections.Generic;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        
        TEntity GetById(int id);
        
        void Add(IEnumerable<TEntity> obj);

        void Update(TEntity obj);

        void Remove(TEntity obj);

        void Dispose();
    }
}