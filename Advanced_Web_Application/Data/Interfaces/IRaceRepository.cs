
using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.Data.Interfaces
{
    public interface IRaceRepository
    {
        Task<IEnumerable<Race>> GetRacesList();
        Task<Race> GetRaceByID(int id);
        Task<Race> GetRaceByIDNoTracking(int id);
        Task<Race> GetRacesByCity(string city);
        bool Add(Race race);
        bool Update(Race race);
        bool Delete(Race race);
        bool Save();
    }
}
