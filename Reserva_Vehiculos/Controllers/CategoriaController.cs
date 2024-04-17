using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace Reserva_Vehiculos.Controllers
{


    public class CategoriaController : Controller
    {
        Categoria_DAO categoria_ = new Categoria_DAO();
        public IActionResult categoria_view()
        {
            // aqui debo lista todas la categorias
            var l_cate = categoria_.ListarCategoria();
            return View(l_cate);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}