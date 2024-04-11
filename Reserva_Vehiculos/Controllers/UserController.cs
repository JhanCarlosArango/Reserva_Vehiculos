using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models.DAO;
using Reserva_Vehiculos.Models;


namespace Reserva_Vehiculos.Controllers
{
    public class UserController : Controller
    {

        Usuario_DAO UDAO = new Usuario_DAO();
        public IActionResult Listar(){
            return View();
        }
        public IActionResult Login(){
            //view
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuarios us){
            Console.WriteLine(UDAO.validar_User(us.usuario,us.contrasenia));
            
            return View();
        }
    }
    }
