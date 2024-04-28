using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace Reserva_Vehiculos.Controllers
{
    public class Reserva : Controller
    {
        Pet_reserva_DAO pet_Reserva_D;
        Vehiculo_DAO vehiculo_DAO;
        List<Pet_reserva> pet_Reserva_;
        List<Vehiculo> vehiculos;
        Vehiculo vehiculo;
        
        public IActionResult Reserva_()
        {
            vehiculo = new Vehiculo();
            pet_Reserva_D = new Pet_reserva_DAO();
            vehiculo_DAO = new Vehiculo_DAO();

            pet_Reserva_ = pet_Reserva_D.ListarPeticion();
            vehiculos = vehiculo_DAO.ListarVehiculo();


            Obj_ViewModel _ViewModel = new Obj_ViewModel()
            {
                _lis_Pet_Reserva = pet_Reserva_,
                _list_vehiculos = vehiculos,
                _vehiculo = vehiculo

            };
            return View(_ViewModel);
        }
        public List<Vehiculo> FiltrarVehiculosPorCategoria(int categoriaId)
        {
            vehiculo = new Vehiculo();
            vehiculo_DAO = new Vehiculo_DAO();
             vehiculos = vehiculo_DAO.ListarVehiculo();
            return vehiculo.Flitar_veiculos_categoria(vehiculos, categoriaId);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}