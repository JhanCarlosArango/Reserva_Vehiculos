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
        DateTime fecha_i;
        DateTime fecha_f;
        public IActionResult peticion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult peticion(Pet_reserva pet_Reserva)
        {
            _Reserva = pet_Reserva;
            _Reserva.separa_feha(_Reserva.fecha);
            _Reserva.separa_feha2(_Reserva.fecha1);

            fecha_i = new DateTime(int.Parse(_Reserva.año), int.Parse(_Reserva.mes), int.Parse(_Reserva.dia));
            fecha_f = new DateTime(int.Parse(_Reserva.año2), int.Parse(_Reserva.mes2), int.Parse(_Reserva.dia2));

            TimeSpan diferencia = fecha_f - fecha_i;
            _Reserva.temp_costo = diferencia.Days;

            _Reserva.fecha_ini = new DateOnly(fecha_i.Year, fecha_i.Month, fecha_i.Day);
            _Reserva.fecha_fin = new DateOnly(fecha_f.Year, fecha_f.Month, fecha_f.Day);

            Console.WriteLine("Dias de arquiler :" + _Reserva.temp_costo);

            return RedirectToAction("categoria_view", "Categoria", _Reserva);
        }

        public IActionResult listar()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}