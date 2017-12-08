using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Models.DataAccess;
using TaskManager.DataAccess.Repositories.Abstration;

namespace TaskManager.DataAccess.Repositories
{
    public class TaskRepository : BaseRepository<TaskDbModel, ApplicationContext>, ITaskRepository
    {
        public TaskRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
