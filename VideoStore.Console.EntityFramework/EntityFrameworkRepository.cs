using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VideoStore.Console.PersistanceModels;
using VideoStore.Console.Persistence;

namespace VideoStore.Console.EntityFramework
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IVideoStoreDbContext _videoStoreDbContext;

        public EntityFrameworkRepository(IVideoStoreDbContext videoStoreDbContext)
        {
            _videoStoreDbContext = videoStoreDbContext;
        }

        public virtual async Task<T> SaveOrUpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(SaveOrUpdateAsync)} entity must not be null");
            }
            try
            {
                _videoStoreDbContext.Update(entity);
                await _videoStoreDbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await _videoStoreDbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual async Task<T> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _videoStoreDbContext.Set<T>().SingleOrDefaultAsync(expression);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _videoStoreDbContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual async Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                return await _videoStoreDbContext.Set<T>().AsQueryable().Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException($"{nameof(Delete)} entity must not be null");
                }
                _videoStoreDbContext.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public virtual void DeleteById(Guid id)
        {
            try
            {
                var entity = _videoStoreDbContext.Set<T>().FirstOrDefault(e => e.Id == id);
                if (entity != null)
                {
                    Delete(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

    }

}
