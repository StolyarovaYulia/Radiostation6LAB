using System.Linq;
using System.Threading.Tasks;

namespace Radiostation.DAL.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        IQueryable<TEntity> GetEntities();
        Task<TEntity> GetEntityById(int entityId);
    }
}