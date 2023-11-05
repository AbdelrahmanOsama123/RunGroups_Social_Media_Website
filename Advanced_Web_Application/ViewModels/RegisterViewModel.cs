using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Advanced_Web_Application.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Email Address")]
        [Required(ErrorMessage ="Email Address is Required")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        public string Username { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password",ErrorMessage ="Passwords are not Matched")]
        public string confirmPassword { get; set; }
    }
}
