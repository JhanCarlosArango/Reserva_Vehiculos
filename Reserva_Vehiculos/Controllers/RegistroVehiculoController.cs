using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class RegistroVehiculoController : Controller
    {
        public IActionResult RegistroVehiculo()
        {
            return View();
        }
    }
}
