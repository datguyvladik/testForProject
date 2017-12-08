using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Core.Models.DataAccess
{
    public class TaskDbModel : DbModel
    {
        public UserDbModel User { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}
