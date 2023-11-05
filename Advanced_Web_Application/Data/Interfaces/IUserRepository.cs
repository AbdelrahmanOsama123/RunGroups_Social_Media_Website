using Advanced_Web_Application.Models;

namespace Advanced_Web_Application.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAppUsers();
        Task<AppUser> GetUserById(string id);
        bool Add(AppUser user);
        bool Delete(AppUser user);
        bool Update(AppUser user);
        bool Save();
    }
}
