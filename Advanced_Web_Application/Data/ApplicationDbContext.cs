using Advanced_Web_Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Web_Application.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options)
        {
        }

        public DbSet<Race> Races {  get; set; }
        public DbSet<Club> Clubs {  get; set; }
        public DbSet<Address> Addresses {  get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
