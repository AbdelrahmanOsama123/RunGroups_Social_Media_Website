using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_Web_Application.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImageURL { get; set; }

        public int? Pace {  get; set; }

        public int? Mileage {  get; set; }

        public string Street  { get; set; }

        public string City  { get; set; }

        public string State { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        public ICollection<Club> Clubs { get; set; }

        public ICollection<Race> Races { get; set; }

    }
}
