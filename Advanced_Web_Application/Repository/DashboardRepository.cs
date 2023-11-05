using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Diagnostics;

namespace Advanced_Web_Application.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DashboardRepository(ApplicationDbContext db,IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Club>> GetAllUserClubs()
        {
            var currUserId = httpContextAccessor.HttpContext?.User.GetUserId();
            var userClubs = await db.Clubs.Where(r => r.AppUserId == currUserId).ToListAsync();
            return userClubs;
        }

        public async Task<List<Race>> GetAllUserRaces()
        {
            var currUserId = httpContextAccessor.HttpContext?.User.GetUserId();
            var userRaces = await db.Races.Where(r => r.AppUserId == currUserId).ToListAsync();
            return userRaces;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await db.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Id==id);
        }

        public bool Update(AppUser user)
        {
            db.AppUsers.Update(user);
            return Save();
        }


        public bool Save()
        {
            var Saved = db.SaveChanges();
            return Saved > 0;
        }
    }
}
