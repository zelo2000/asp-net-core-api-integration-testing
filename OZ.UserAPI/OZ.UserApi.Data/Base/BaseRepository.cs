using Microsoft.EntityFrameworkCore;

namespace OZ.UserApi.Data.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public UserApiDbContext Context { get; }

        public BaseRepository(UserApiDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await Context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
