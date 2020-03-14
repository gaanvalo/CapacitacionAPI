using System.Collections.Generic;

namespace PersonasDAL
{
    public interface IGenericRepository<TEntity>
    {
        List<TEntity> GetAllAsync();
        TEntity GetAsync(int id);
        void InsertAsync(TEntity entity, out object responso);
        void DeleteAsync(int id, out object response);
        void UpdateAsync(int id, TEntity entityToUpdate, out object response);
    }
}
