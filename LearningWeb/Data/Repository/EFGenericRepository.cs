using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace LearningWeb
{
    public class EFGenericRepository<T> : IRepository<T> where T : class
    {
        internal IDbContext context { get; set; }
        protected DbSet<T> dbSet { get; set; }

        /// Constructor
        public EFGenericRepository(IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("DbContext");
            }
            else
            {
                this.context = context;
                this.dbSet = context.Set<T>();
            }
        }

        #region Implementation of the IRepository<T> interface

        /// <summary>
        /// How to use this method
        /// You need to use .ToList() because the LINQ will not execute until to call .ToList() or .Count()
        /// For example, " List<ValueList> valueLists = unitofWork.ValueListRepository.GetAll.ToList(); "
        /// </summary>
        public IQueryable<T> GetQueryable
        {
            get
            {
                return dbSet;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public IEnumerable<T> Get(string where, object[] whereParameters)
        {
            return Enumerable.Empty<T>();
        }

        public IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }


        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }


        #endregion Implementation of the IRepository<T> interface
    }
}
