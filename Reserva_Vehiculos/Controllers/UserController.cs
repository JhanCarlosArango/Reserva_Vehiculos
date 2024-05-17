using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models.DAO;
using Reserva_Vehiculos.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;



namespace Reserva_Vehiculos.Controllers
{
    public class UserController : Controller
    {

        Usuario_DAO UDAO = new Usuario_DAO();
        Usuarios user;

        IHttpContextAccessor _httpContextAccessor { get; set; }

        public UserController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuarios us)
        {

            user = UDAO.Search_user(us.usuario, us.contrasenia);

            if (user != null)
            {
                _httpContextAccessor.HttpContext.Session.SetString("name_user", us.usuario); // REcordar registrar el controlador para traer la session
                // Crear las claims del usuario
                var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.usuario)
                };
                
                foreach (var rol in user.rols)
                {
                    String _rol =  rol.ToString();
                    claims.Add(new Claim(ClaimTypes.Role,  _rol));
                    Console.WriteLine(_rol);
                }

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Crear las propiedades de autenticación
                AuthenticationProperties authProperties = new()
                {
                    AllowRefresh = true,
                    IsPersistent = false,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                };

                // Iniciar sesión del usuario
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("peticion", "Peticion"); //Envio el usuario
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

            _httpContextAccessor.HttpContext.Session.Remove("name_user");
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("peticion", "Peticion");
        }
    }
}
