using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models.DTO
{
    public class UserDto
    {
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
