using System;
using TaskManager.Core.Factory.Abstraction;
using TaskManager.DataAccess;
using TaskManager.DataAccess.Repositories;
using TaskManager.DataAccess.Repositories.Abstration;

namespace TaskManager.Core.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly ApplicationContext _context;

        protected bool IsDisposed { get; set; }

        public RepositoryFactory(ApplicationContext context)
        {
            _context = context;
        }
        ~RepositoryFactory()
        {
            Dispose(false);
        }

        public ITaskRepository CreateTaskRepository()
        {
           return new TaskRepository(_context);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }

            IsDisposed = true;
        }
    }
}
