using System.Linq.Expressions;

namespace VideoStore.Console.Persistence
{
    public interface IRepository<T>
    {
        Task<T> SaveOrUpdateAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetSingleByExpressionAsync(Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetByExpressionAsync(Expression<Func<T, bool>> expression);
        void Delete(T entity);
        void DeleteById(Guid id);
    }
}
