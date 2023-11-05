using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.Data.Interfaces
{
    public interface IClubRepository
    {
        Task<IEnumerable<Club>> GetClubsList();
        Task<Club> GetClubByID(int id);
        Task<Club> GetClubByIDNoTracking(int id);
        Task<IEnumerable<Club>> GetClubsByCity(string city);
        bool Add(Club club);
        bool Update(Club club);
        bool Delete(Club club);
        bool Save();
    }
}
