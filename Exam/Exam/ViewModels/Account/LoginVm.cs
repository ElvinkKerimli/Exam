using System.ComponentModel.DataAnnotations;

namespace Exam.ViewModels.Account
{
    public class LoginVm
    {
        [Required]
        [EmailAddress]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Reminder { get; set; }

    }
}
