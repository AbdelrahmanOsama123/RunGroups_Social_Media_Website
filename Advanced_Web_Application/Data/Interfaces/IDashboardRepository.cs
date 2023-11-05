using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.Data.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Club>> GetAllUserClubs();
        Task<List<Race>> GetAllUserRaces();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();

    }
}
