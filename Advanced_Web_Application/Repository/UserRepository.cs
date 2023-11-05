using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Web_Application.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext db;

        public UserRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool Add(AppUser user)
        {
            db.Users.Add(user);
            return Save();
        }

        public bool Delete(AppUser user)
        {
            db.Users.Remove(user);
            return Save();
        }

        public async Task<IEnumerable<AppUser>> GetAppUsers()
        {
            return await db.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await db.AppUsers.FindAsync(id);
        }

        public bool Save()
        {
            var Saved = db.SaveChanges();
            return Saved > 0;
        }

        public bool Update(AppUser user)
        {
            db.Users.Update(user);
            return Save();
        }
    }
}
