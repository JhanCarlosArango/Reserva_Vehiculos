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

    
    public class CategoriaController : Controller
    {
        Categoria_DAO categoria_ = new Categoria_DAO();
        public IActionResult categoria_view(Pet_reserva _Pet_Reserva)
        {
            var l_cate = categoria_.ListarCategoria();
            var viewModel = new Obj_ViewModel
            {
                ListaCategorias = l_cate,
                _Pet_Reserva = _Pet_Reserva,   // _ViewModel_pet_Reserva pasa info a _Pet_Reserva

            };
            return View(viewModel);
        }
       
        [HttpPost]
        [Authorize]
        public IActionResult categoria_view(String id_cate)
        {
            Console.WriteLine(" Categoria ID:--------> "+id_cate );
            return RedirectToAction("peticion", "Peticion");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}