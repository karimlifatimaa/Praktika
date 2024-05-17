using DoorangApp.Business.Services.Abstracts;
using DoorangApp.Core.Models;
using DoorangApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DoorangApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExplorerServices _explorerServices;
        public HomeController(ILogger<HomeController> logger, IExplorerServices explorerServices)
        {
            _logger = logger;
            _explorerServices = explorerServices;
        }

        public IActionResult Index()
        {
            List<Explorer> explorers = _explorerServices.GetAllExplorer();
            return View(explorers);
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
