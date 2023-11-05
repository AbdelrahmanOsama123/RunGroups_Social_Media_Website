using Advanced_Web_Application.Data.Interfaces;
using Advanced_Web_Application.helpers;
using Advanced_Web_Application.Models;
using Advanced_Web_Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace Advanced_Web_Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClubRepository clubRepository;

        public HomeController(ILogger<HomeController> logger,IClubRepository ClubRepository)
        {
            _logger = logger;
            clubRepository = ClubRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IpInfo();
            var homeViewModel = new HomeViewModel();
            try
            {
                string url = "https://ipinfo.io?token=f1cb1ee37c1348";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRegion = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRegion.EnglishName;
                homeViewModel.City = ipInfo.Region;
                homeViewModel.State = ipInfo.Country;
                if (homeViewModel.City == null)
                {
                    homeViewModel.clubs = null;
                }
                else
                {
                    homeViewModel.clubs = await clubRepository.GetClubsByCity(homeViewModel.City);
                }
                return View(homeViewModel);
            }
            catch(Exception ex)
            {
                homeViewModel.clubs = null;
                return View(homeViewModel);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}