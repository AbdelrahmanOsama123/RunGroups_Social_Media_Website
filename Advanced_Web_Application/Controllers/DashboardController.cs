using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.Services;
using Advanced_Web_Application.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace Advanced_Web_Application.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository dashboardRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPhotoService photoService;

        public DashboardController(IDashboardRepository dashboardRepository
            ,IHttpContextAccessor httpContextAccessor,IPhotoService photoService)

        {
            this.dashboardRepository = dashboardRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.photoService = photoService;
        }

        public async Task<IActionResult> Index()
        {
            var userClubs = await dashboardRepository.GetAllUserClubs();
            var userRaces = await dashboardRepository.GetAllUserRaces();
            DashboardViewModel dashboardViewModel = new ()
            {
                Clubs = userClubs,
                Races = userRaces
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currUserId = httpContextAccessor.HttpContext.User?.GetUserId();
            AppUser user = await dashboardRepository.GetUserById(currUserId);
            if (user == null)
            {
                return View("Error");
            }
            EditUserViewModel editVM = new ()
            {
                Id = currUserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                Street = user.Street,
                City = user.City,
                State = user.State,
                ProfileImageURL = user.ProfileImageURL
            };
            return View(editVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserViewModel editUserVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editUserVM);
            }
            AppUser user = await dashboardRepository.GetUserByIdNoTracking(editUserVM.Id);
            if(user.ProfileImageURL=="" || user.ProfileImageURL == null)
            {
                var photoResult = await photoService.AddPhoteAsync(editUserVM.Image);
                MapUserEdit(user, editUserVM, photoResult);
                dashboardRepository.Update(user);
                return RedirectToAction("Index","User");
            }
            else
            {
                try
                {
                    await photoService.DeletePhotoAsync(user.ProfileImageURL);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("ProfileImageURL", ex+"Couldn't Delete photo");
                    return View(editUserVM);
                }
                var photoResult = await photoService.AddPhoteAsync(editUserVM.Image);
                MapUserEdit(user, editUserVM, photoResult);
                dashboardRepository.Update(user);
                return RedirectToAction("Index", "User");
            }

        }

        private void MapUserEdit(AppUser user, EditUserViewModel editUserVM, ImageUploadResult photoResult)
        {
            user.Id = editUserVM.Id;
            user.FirstName = editUserVM.FirstName;
            user.LastName = editUserVM.LastName;
            user.ProfileImageURL = photoResult.Url.ToString();
            user.Mileage = editUserVM.Mileage;
            user.Pace = editUserVM.Pace;
            user.City = editUserVM.City;
            user.State = editUserVM.State;
            user.Street = editUserVM.Street;
        }
    }
}
