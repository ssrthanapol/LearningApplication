using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace LearningWeb
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        DbEntityEntry Entry(object entity);
    }
}
