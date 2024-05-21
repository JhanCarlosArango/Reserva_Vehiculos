using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace FrontEnd.Controllers
{
    public class EntregaVehiculoController : Controller
    {
        Vehiculo_DAO vehiculo_DAO;
        Reserva_DAO _reserva_DAO;
        Ubicacion_DAO _ubi_dao;
        Danio_vehiculos_DAO _danio_DAO;
        List<Ubicacion> _list_ubi;
        List<Danios_vehiculos> _list_danio;

        Reporte_entrega_DAO _repo;
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
        public IActionResult EntregaVehiculo(String fk_num_placa, decimal valorTotal, String ubicacion, String horaActual2, DateOnly fechaActual2, List<string> hiddenSelectedDanios, int id_reserva_temp) // horaActual2 no es String
        {
            _repo = new Reporte_entrega_DAO();
            _repo.Guardar_Reporte_entrega(fechaActual2, horaActual2, int.Parse(ubicacion), fk_num_placa);

            _reserva_DAO = new Reserva_DAO();

            int fk_id_reporte = _repo.Get_id_reporte_entrega();
            Console.WriteLine("id_reserva_temp" + id_reserva_temp);
            foreach (var item in hiddenSelectedDanios)
            {
                int fk_id_danio = int.Parse(item);
                _repo.Guardar_Itermedia_reporte_danio(fk_id_danio, fk_id_reporte);
            }
            _reserva_DAO.FINALIZAR_RESERVA(id_reserva_temp);
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
