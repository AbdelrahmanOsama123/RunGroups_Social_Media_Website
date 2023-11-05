using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.Repository;
using Advanced_Web_Application.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Web_Application.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository clubRepository;
        private readonly IPhotoService photoService;

        public readonly IHttpContextAccessor httpContextAccesss;

        public ClubController(IClubRepository clubRepository,IPhotoService photoService,IHttpContextAccessor httpContextAccesss)
        {
            this.clubRepository = clubRepository;
            this.photoService = photoService;
            this.httpContextAccesss = httpContextAccesss;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> Clubs = await clubRepository.GetClubsList();
            return View(Clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
                return NotFound();
            Club club = await clubRepository.GetClubByID(id);

            if (club == null)
                return NotFound();

            return View(club);
        }

        public async Task<IActionResult> Create()
        {
            var currUserId = httpContextAccesss.HttpContext?.User.GetUserId();
            CreateClubViewModel createClubViewModel = new CreateClubViewModel()
            {
                AppUserId = currUserId
            };
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhoteAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = clubVM.AppUserId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    },
                    ClubCatagory = clubVM.ClubCatagory
                };
                clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
                return NotFound();
            Club club = await clubRepository.GetClubByID(id);

            if (club == null)   
                return NotFound();

            EditClubViewModel clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressId = club.AddressId,
                Address = new Address
                {
                    Street = club.Address.Street,
                    City = club.Address.City,
                    State = club.Address.State,
                },
                URl = club.Image,
                ClubCatagory = club.ClubCatagory
            };
            return View(clubVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id,EditClubViewModel clubVM)
        {
            var clubByID = await clubRepository.GetClubByIDNoTracking(id);
            if (clubByID != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(clubByID.Image);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Image", "Could not deleted");
                    return View(clubVM);
                }
            }

            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhoteAsync(clubVM.Image);
            
                var club = new Club
                {
                    Id = id,
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    AddressId = clubVM.AddressId,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    },
                    ClubCatagory = clubVM.ClubCatagory
                };
                clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(clubVM);
            }
        }
    }
}
