using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;
namespace Reserva_Vehiculos.Controllers
{
    public class Peticion : Controller
    {
        Pet_reserva _Reserva;
        Persona_DAO persona_DAO;

        Persona per;
        DateTime fecha_i;
        DateTime fecha_f;
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        public Peticion(IHttpContextAccessor httpContextAccessor)
        {
            _IHttpContextAccessor = httpContextAccessor;
        }
        public IActionResult peticion(Usuarios _user) // recibo user
        {
            Console.WriteLine("aqui " + _user.id_user);
            return View(_user);
        }

        [HttpPost]
        public IActionResult peticion(DateOnly fechaini, String hora_ini, DateOnly fechafin, String hora_fin, String id_user)
        {
            _Reserva = new Pet_reserva();
            string fechainiStr = fechaini.ToString();

            _Reserva.separa_feha(fechainiStr);

            string fechafinStr = fechafin.ToString();
            _Reserva.separa_feha2(fechafinStr);

            fecha_i = new DateTime(int.Parse(_Reserva.dia), int.Parse(_Reserva.mes), int.Parse(_Reserva.año));
            fecha_f = new DateTime(int.Parse(_Reserva.dia2), int.Parse(_Reserva.mes2), int.Parse(_Reserva.año2));

            TimeSpan diferencia = fecha_f - fecha_i;
            _Reserva.temp_costo = diferencia.Days;

            _Reserva.fecha_ini = new DateOnly(fecha_i.Year, fecha_i.Month, fecha_i.Day);
            _Reserva.fecha_fin = new DateOnly(fecha_f.Year, fecha_f.Month, fecha_f.Day);

            Console.WriteLine("Dias de arquiler :" + _Reserva.temp_costo);

            int id_users = int.Parse(id_user);

            return RedirectToAction("categoria_view", "Categoria", _Reserva);
        }

        [Authorize]
        [HttpPost] // GET
        public IActionResult Datos_Reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, String id_cate, String id_user)
        {
            persona_DAO = new Persona_DAO();
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user"); 

            Console.WriteLine("Usuario . "+ usuario_session);

            per = persona_DAO.Search_persona(usuario_session);
            
            
            
            var viewModel_1 = new Obj_ViewModel
            {

                _Pet_Reserva = new Pet_reserva(fecha_ini, fecha_fin, hora_ini, hora_fin), // este contexto
                categoria = new Categoria(int.Parse(id_cate)),
            };
            return View(viewModel_1);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}