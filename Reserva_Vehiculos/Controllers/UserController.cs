using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models.DAO;
using Reserva_Vehiculos.Models;


namespace Reserva_Vehiculos.Controllers
{
    public class UserController : Controller
    {
        Usuario_DAO UDAO = new Usuario_DAO();
        Usuarios user; 
        public IActionResult Listar()
        {
            return View();
        }
        public IActionResult Login()
        {
            //view
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuarios us)
        {
            user = UDAO.Search_user(us.usuario, us.contrasenia);
            Console.WriteLine("Roles obtenidos:");
            foreach (var rol in user.rols)
            {
                Console.WriteLine(rol);
            }
            return View();
        }
    }
}
