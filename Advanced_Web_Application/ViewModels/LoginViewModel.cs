using CloudinaryDotNet.Actions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Advanced_Web_Application.ViewModels
{
    public class LoginViewModel
    {
        [DisplayName("Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
