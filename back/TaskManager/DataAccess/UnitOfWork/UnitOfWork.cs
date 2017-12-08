using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Factory.Abstraction;
using TaskManager.DataAccess.Repositories.Abstration;
using TaskManager.DataAccess.UnitOfWork.Abstraction;

namespace TaskManager.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly IRepositoryFactory _repositoryFactory;
        private ITaskRepository _taskRepository;
        private bool _disposed;

        public UnitOfWork(ApplicationContext context, IRepositoryFactory factory)
        {
            _context = context;
            _repositoryFactory = factory;
        }


        public ITaskRepository TaskRepository
            => _taskRepository ?? (_taskRepository = _repositoryFactory.CreateTaskRepository());

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _context?.Dispose();
                _repositoryFactory?.Dispose();
                _taskRepository?.Dispose();
                GC.SuppressFinalize(this);
            }

            _disposed = true;
        }
    }
}
