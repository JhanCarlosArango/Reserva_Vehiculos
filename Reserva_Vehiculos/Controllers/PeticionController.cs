using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models;
namespace Reserva_Vehiculos.Controllers
{
    public class Peticion : Controller
    {
        Pet_reserva _Reserva;
        public IActionResult peticion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult peticion(Pet_reserva pet_Reserva)
        {
            _Reserva = pet_Reserva;

            _Reserva.separa_feha(_Reserva.fecha);

            Console.WriteLine(pet_Reserva.año);
            //_Reserva.separa_feha2(_Reserva.fecha_2);
    
            //DateTime fecha_ini = new DateTime(int.Parse(_Reserva.año), int.Parse(_Reserva.mes), int.Parse(_Reserva.dia));
            //DateTime fecha_fin = new DateTime(int.Parse(_Reserva.año2), int.Parse(_Reserva.año2), int.Parse(_Reserva.año2));

            //TimeSpan diferencia = fecha_fin.Subtract(fecha_ini);
            //int diferenciaDias = diferencia.Days;
            
            //Console.WriteLine(fecha_ini);
           // Console.WriteLine(fecha_fin);
            //Console.WriteLine(diferenciaDias);
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}