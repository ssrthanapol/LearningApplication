using LearningWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace LearningWeb
{
    public class EFUnitOfWork : IUnitOfWork
    {
        protected Entities _context;
        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public Entities Context { get { return _context; } }

        public EFUnitOfWork()
        {
            _context = new Entities();
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.Keys.Contains(typeof(T)))
                _repositories.Add(typeof(T), new EFGenericRepository<T>(_context));
            return _repositories[typeof(T)] as IRepository<T>;
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    string msgerror = "";
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        msgerror = String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        Console.WriteLine(msgerror);
                        sb.AppendFormat(msgerror);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            msgerror = String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                            Console.WriteLine(msgerror);
                            sb.AppendFormat(msgerror);
                        }
                    }
                    throw new ValidationException(sb.ToString());
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}