using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Web_Application.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext db;

        public RaceRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool Add(Race race)
        {
            db.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            db.Remove(race);
            return Save();
        }


        public async Task<IEnumerable<Race>> GetRacesList()
        {
            IEnumerable<Race> Races = await db.Races.ToListAsync();
            return Races;
        }

        public async Task<IEnumerable<Race>> GetRacesByCity(string city)
        {
            IEnumerable<Race> Races = await db.Races.Include(a => a.Address).Where(c => c.Address.City.Contains(city)).ToListAsync();
            return Races;
        }

        public async Task<Race> GetRaceByID(int id)
        {
            Race race = await db.Races.Include(a => a.Address).SingleOrDefaultAsync(u => u.Id == id);
            return race;
        }
        public async Task<Race> GetRaceByIDNoTracking(int id)
        {
            Race race = await db.Races.Include(a => a.Address).AsNoTracking().SingleOrDefaultAsync(u => u.Id == id);
            return race;
        }
        public bool Save()
        {
            var saved = db.SaveChanges();
            return saved > 0;
        }

        public bool Update(Race race)
        {
            db.Update(race);
            return Save();
        }

        Task<Race> IRaceRepository.GetRacesByCity(string city)
        {
            throw new NotImplementedException();
        }
    }
}
