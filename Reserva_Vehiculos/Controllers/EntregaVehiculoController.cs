using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace FrontEnd.Controllers
{
    public class EntregaVehiculoController : Controller
    {
        Vehiculo_DAO vehiculo_DAO;
        Ubicacion_DAO _ubi_dao;
        Danio_vehiculos_DAO _danio_DAO;
        List<Ubicacion> _list_ubi;
        List<Danios_vehiculos> _list_danio;
        public IActionResult EntregaVehiculo()
        {
            _ubi_dao = new Ubicacion_DAO();
            _danio_DAO = new Danio_vehiculos_DAO();

            _list_ubi = _ubi_dao.listar_ubicacion();
            _list_danio = _danio_DAO.ListarDanio();

            var view_model = new Obj_View_Vehiculo()
            {
                ListatipoUbicacion = _list_ubi,
                Lista_danio_Vehiculos = _list_danio
            };

            return View(view_model);
        }
        [HttpPost]
        public IActionResult EntregaVehiculo(decimal valorTotal,String ubicacion, String horaActual2, string fechaActual2, List<string> hiddenSelectedDanios) // horaActual2 no es String
        {

            foreach (var item in hiddenSelectedDanios)
            {
                Console.WriteLine("id_danio "+item.ToString());
            }
            Console.WriteLine(" ubicacion id " + ubicacion);
            Console.WriteLine("hora " + horaActual2);
            Console.WriteLine("fecha " + fechaActual2);
            Console.WriteLine("valor " + valorTotal);
            return RedirectToAction("EntregaVehiculo", "EntregaVehiculo");
        }

        [HttpGet]
        public JsonResult BuscarVehiculo(string placa)
        {
            vehiculo_DAO = new Vehiculo_DAO();
            Vehiculo vehiculo = null;
            if (placa != null)
            {
                vehiculo = vehiculo_DAO.BUscarVehiculo(placa);
            }

            if (vehiculo != null)
            {
                return Json(new { success = true, data = vehiculo });
            }
            else
            {
                return Json(new { success = false, message = "Vehículo no encontrado." });
            }
        }

    }
}
