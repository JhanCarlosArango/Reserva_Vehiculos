using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class DatosPersonalesController : Controller
    {
        public IActionResult DatosPersonales()
        {
            return View();
        }
    }
}
