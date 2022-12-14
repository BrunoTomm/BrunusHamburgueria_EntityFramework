using Microsoft.AspNetCore.Mvc;

namespace BrunusBurguer.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
