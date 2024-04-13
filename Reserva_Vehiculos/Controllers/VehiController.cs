using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Reserva_Vehiculos.Models;

namespace Reserva_Vehiculos.Controllers
{
    public class VehiController : Controller
    {
        private readonly IWebHostEnvironment _environment; // Obtener ruta de mi proyecto


        public VehiController(IWebHostEnvironment _environment)
        {
            this._environment = _environment;   
        }

        public IActionResult RV()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Upload(Vehiculo_doc upload){

            var filename = System.IO.Path.Combine(_environment.ContentRootPath,"PDF",upload.doc_legal.FileName);
            using(var fs = new System.IO.FileStream(filename, System.IO.FileMode.Create)){
                await upload.doc_legal.CopyToAsync(fs);
            }
            
            Console.WriteLine("Ruta :",filename);
            return View();
        }  
    }
}