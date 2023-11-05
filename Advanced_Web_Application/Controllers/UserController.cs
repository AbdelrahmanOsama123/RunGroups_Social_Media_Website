using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_Web_Application.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AppUser> users = await userRepository.GetAppUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            MapResultUser(users, result);

            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            IEnumerable<AppUser> users = await userRepository.GetAppUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            MapResultUser(users, result);
            AppUser user = await userRepository.GetUserById(id);
            UserDetailViewModel userDetailVm = new UserDetailViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                Pace = user.Pace,
                Mileage = user.Mileage,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ProfileImageURl = user.ProfileImageURL,
            };
            return View(userDetailVm);
        }

        private void MapResultUser(IEnumerable<AppUser> users, List<UserViewModel> result)
        {
            foreach (var user in users)
            {
                UserViewModel userVM = new UserViewModel()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Id = user.Id,
                    Pace = user.Pace,
                    Mileage = user.Mileage,
                    ProfileImageURL = user.ProfileImageURL
                };
                result.Add(userVM);
            }
        }
    }
}
