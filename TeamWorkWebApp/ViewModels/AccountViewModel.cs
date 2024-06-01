using System.ComponentModel.DataAnnotations;

namespace TeamWorkWebApp.ViewModels
{
    public class AccountViewModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
        public string Name { get; set; }
    }
}

