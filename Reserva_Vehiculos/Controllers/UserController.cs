using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models.DAO;
using Reserva_Vehiculos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;



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
        public async Task<IActionResult> Login(Usuarios us)
        {
            user = UDAO.Search_user(us.usuario, us.contrasenia);

            if (user != null)
            {
                // Crear las claims del usuario
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.usuario),

            };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crear las propiedades de autenticación
                AuthenticationProperties authProperties = new ();
                
                    authProperties.AllowRefresh = true;
                    authProperties.IsPersistent = false;
                    authProperties.ExpiresUtc = DateTime.UtcNow.AddMinutes(10);

                // Iniciar sesión del usuario
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("peticion", "Peticion");
            }
            else
            {
                // Usuario no válido, manejar el error o devolver la vista de login nuevamente
                ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("peticion", "Peticion");
        }
    }
}
/*

        [HttpPost]
        public IActionResult Login(Usuarios us)
        {
            user = UDAO.Search_user(us.usuario, us.contrasenia);

            if (user == null)
            {
               // FormsAuthentication.setAthCookie(user.usuario);
            }
            return View();
        }
        */