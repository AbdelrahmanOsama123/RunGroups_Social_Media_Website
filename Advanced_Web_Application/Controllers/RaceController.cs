using Advanced_Web_Application.Data;
using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.Repository;
using Advanced_Web_Application.Services;
using Advanced_Web_Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Advanced_Web_Application.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository raceRepository;
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RaceController(IRaceRepository raceRepository,IPhotoService photoService,IHttpContextAccessor httpContextAccessor)
        {
            this.raceRepository = raceRepository;
            this.photoService = photoService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> Races = await raceRepository.GetRacesList();
            return View(Races);
        }

        public async Task<IActionResult> Detail (int id)
        {
            Race race = await raceRepository.GetRaceByID(id);
            return View(race);
        }

        public IActionResult Create()
        {
            var currUserId = httpContextAccessor.HttpContext?.User.GetUserId();
            CreateRaceViewModel createRaceViewModel = new CreateRaceViewModel()
            {
                AppUserId = currUserId
            };
            return View(createRaceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhoteAsync(raceVM.Image);

                var race = new Race
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    AppUserId = raceVM.AppUserId,
                    Address = raceVM.Address,
                    RaceCatagory = raceVM.RaceCatagory
                };
                raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Image", "Photo Upload fail");
                return View(raceVM);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var race = await raceRepository.GetRaceByID(id);
            if (race == null)
                return NotFound();
            EditRaceViewModel raceVM = new()
            {
                Title = race.Title,
                Description = race.Description,
                AddressId = race.AddressId,
                Address = race.Address,
                RaceCatagory = race.RaceCatagory
            };
            return View(raceVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id,EditRaceViewModel raceVM) { 
            var Race = await raceRepository.GetRaceByIDNoTracking(id);
            if (Race != null)
            {
                try
                {
                    await photoService.DeletePhotoAsync(Race.Image);
                }
                catch (Exception ex)
                {
                    throw new Exception("error " + ex);
                }
            }

            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhoteAsync(raceVM.Image);
                var race = new Race
                {
                    Id = id,
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    AddressId = raceVM.AddressId,
                    Address = new Address
                    {
                        Street = raceVM.Address.Street,
                        City = raceVM.Address.City,
                        State = raceVM.Address.State
                    },
                    RaceCatagory = raceVM.RaceCatagory
                };
                raceRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
                return View(raceVM);
        }
    }
}
