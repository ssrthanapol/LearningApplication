using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LearningWeb
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetQueryable { get; }
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T GetByID(object id);
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
