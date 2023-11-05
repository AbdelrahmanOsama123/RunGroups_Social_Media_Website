using Advanced_Web_Application.Data.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Advanced_Web_Application.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Image {  get; set; }

        public string Description { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address {  get; set; }
        [DisplayName("Club Catagory")]
        public ClubCatagory ClubCatagory {  get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId {  get; set; }
        public AppUser? AppUser {  get; set; }  
    }
}
