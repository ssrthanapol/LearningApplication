using LearningWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWeb
{
    public interface IUnitOfWork : IDisposable
    {
        Entities Context { get; }
        void SaveChanges();
        IRepository<T> GetRepository<T>() where T : class;
    }
}
