using System.ComponentModel;

namespace Advanced_Web_Application.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public int? Pace { get; set; }

        public int? Mileage { get; set; }

        public string? ProfileImageURL { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public IFormFile Image { get; set; }
    }
}
