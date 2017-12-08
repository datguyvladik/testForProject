using System;
using TaskManager.DataAccess.Repositories.Abstration;

namespace TaskManager.Core.Factory.Abstraction
{
    public interface IRepositoryFactory : IDisposable
    {
        ITaskRepository CreateTaskRepository();
    }
}
