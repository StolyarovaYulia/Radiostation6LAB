using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Radiostation.DAL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly RadiostationContext Context;

        protected BaseRepository(RadiostationContext context)
        {
            Context = context;
        }

        public async Task Create(TEntity entity)
        {
            Context.Add(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetEntityById(id);

            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetEntities()
        {
            var query = IncludeChildrens(Context.Set<TEntity>())
                .AsNoTracking();

            return query;
        }

        public async Task<TEntity> GetEntityById(int entityId)
        {
            var item = await GetEntities()
                .FirstOrDefaultAsync(GetByIdExpression(entityId));

            return item;
        }

        protected abstract IQueryable<TEntity> IncludeChildrens(IQueryable<TEntity> query);

        protected abstract Expression<Func<TEntity, bool>> GetByIdExpression(int id);
    }
}