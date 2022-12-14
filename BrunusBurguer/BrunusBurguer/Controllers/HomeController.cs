using BrunusBurguer.Models;
using BrunusBurguer.Repositories.Interfaces;
using BrunusBurguer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrunusBurguer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos,
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}