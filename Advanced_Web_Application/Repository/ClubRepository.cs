using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Advanced_Web_Application.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext db;

        public ClubRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool Add(Club club)
        {
            db.Add(club);
            return Save();
        }

        public bool Delete(Club club)
        {
            db.Remove(club);
            return Save();
        }

        public async Task<IEnumerable<Club>> GetClubsList()
        {
            IEnumerable<Club> clubs = await db.Clubs.ToListAsync();
            return clubs;
        }

        public async Task<IEnumerable<Club>> GetClubsByCity(string city)
        {
            return  await db.Clubs.Include(a=>a.Address).Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Club> GetClubByID(int id)
        {
            Club club = await db.Clubs.Include(a=>a.Address).SingleOrDefaultAsync(u => u.Id == id);
            return club;
        }

        public async Task<Club> GetClubByIDNoTracking(int id)
        {
            Club club = await db.Clubs.Include(a => a.Address).AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            return club;
        }

        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(Club club)
        {
            db.Update(club);
            return Save();
        }

    }
}
