using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace Reserva_Vehiculos.Controllers
{

    [Authorize]
    public class CategoriaController : Controller
    {
        Categoria_DAO categoria_ = new Categoria_DAO();
        public IActionResult categoria_view(Pet_reserva pet_Reserva)
        {
            var l_cate = categoria_.ListarCategoria();
            var viewModel = new Obj_ViewModel
            {
                ListaCategorias = l_cate,
                _Pet_Reserva = pet_Reserva,   // disponible en este contexto
            };
            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}