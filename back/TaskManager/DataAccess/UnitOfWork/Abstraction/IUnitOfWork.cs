using System;
using System.Threading.Tasks;
using TaskManager.DataAccess.Repositories.Abstration;

namespace TaskManager.DataAccess.UnitOfWork.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository TaskRepository { get; }
        Task SaveAsync();
    }
}
