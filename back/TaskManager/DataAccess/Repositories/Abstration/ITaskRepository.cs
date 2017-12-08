using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Models.DataAccess;

namespace TaskManager.DataAccess.Repositories.Abstration
{
    public interface ITaskRepository : IRepository<TaskDbModel>
    {
    }
}
