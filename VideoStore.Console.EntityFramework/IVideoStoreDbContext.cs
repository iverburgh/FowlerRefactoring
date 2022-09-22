using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VideoStore.Console.PersistanceModels;

namespace VideoStore.Console.EntityFramework
{
    public interface IVideoStoreDbContext
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<Performance> Performances { get; set; }
        DbSet<Play> Plays { get; set; }
        DbSet<T> Set<T>() where T : class;
        EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}