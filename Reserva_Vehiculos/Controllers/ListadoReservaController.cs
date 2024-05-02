using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models.DAO;

namespace Reserva_Vehiculos.Controllers
{

    public class ListadoReservaController : Controller
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;

        public ListadoReservaController(IHttpContextAccessor httpContextAccessor)
        {
            _IHttpContextAccessor = httpContextAccessor;
        }
        Reserva_DAO reserva_DAO = new Reserva_DAO();
        public IActionResult ListadoReserva()
        {
            var viewModel = reserva_DAO.Listar_Reservas_para_admin();
            return View(viewModel);
        }


        public IActionResult MisReservas()
        {
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");
            var viewModel = reserva_DAO.Listar_Reservas(usuario_session);
            return View(viewModel);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}