using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models.DAO;

namespace FrontEnd.Controllers
{
    public class EntregaVehiculoController : Controller
    {
        Vehiculo_DAO vehiculo_DAO;
        public IActionResult EntregaVehiculo()
        {
            vehiculo_DAO = new Vehiculo_DAO();
            return View(vehiculo_DAO);
        }
    }
}
