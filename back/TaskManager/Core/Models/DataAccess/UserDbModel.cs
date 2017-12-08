using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Core.Models.DataAccess
{
    public class UserDbModel : IdentityUser
    {
        public ICollection<TaskDbModel> Tasks { get; set; }
    }
}
