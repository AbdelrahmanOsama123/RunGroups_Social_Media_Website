using Advanced_Web_Application.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_Web_Application.ViewModels
{
    public class UserDetailViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }

        public string? ProfileImageURl {  get; set; }

        public int? Pace { get; set; }

        public int? Mileage { get; set; }

        //public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
