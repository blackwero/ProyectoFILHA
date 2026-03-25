using Microsoft.AspNetCore.Mvc;

namespace ProyectoFILHA.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
