using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;
namespace Reserva_Vehiculos.Controllers
{
    [Authorize]
    public class RegistroVehiculoController : Controller
    {
        Vehiculo_DAO _vehi_DAO;
        Categoria_DAO _cate_DAO;
        Marca_DAO _marca_DAO;
        List<Categoria> l_cate_list;
        List<Marca> l_marca_list;

        Caja_Cambio_DAO caja_Cambio_DAO;
        List<Caja_Cambio> l_caja_list;

        List<Tipo_Direccion> l_tipo_direccion;
        Tipo_Direccion_DAO tipo_direccion_DAO;

        Tipo_Direccion _tipo;
        Marca _marca;
        Caja_Cambio _caja;
        Categoria _cate;

        
        public IActionResult RegistroVehiculo()
        {
            _cate_DAO = new Categoria_DAO();
            l_cate_list = _cate_DAO.ListarCategoria();

            _marca_DAO = new Marca_DAO();
            l_marca_list = _marca_DAO.ListarMarca();

            caja_Cambio_DAO = new Caja_Cambio_DAO();
            l_caja_list = caja_Cambio_DAO.ListarCajas();


            tipo_direccion_DAO = new Tipo_Direccion_DAO();
            l_tipo_direccion = tipo_direccion_DAO.ListarCTipo_Direccion();

            var view_vehiculo = new Obj_View_Vehiculo
            {
                ListaCategorias = l_cate_list,
                ListaMarca = l_marca_list,
                ListaCaja = l_caja_list,
                ListatipoDerrecion = l_tipo_direccion,
            };

            return View(view_vehiculo);
        }
        [HttpPost]
        public IActionResult RegistroVehiculo(String placa, String id_categoria, String id_marca, String modelo, String chasis, String motor, String cilindraje, String carga, String pasajero, String id_tp_combus, String color, String caja, String direccion)
        {
            Console.WriteLine("id_categoria" + id_categoria);
            Console.WriteLine("id_marca" + id_marca);
            Console.WriteLine("modelo" + modelo);
            Console.WriteLine("chasis" + chasis);
            Console.WriteLine("motor" + motor);
            Console.WriteLine("cilindraje" + cilindraje);
            Console.WriteLine("carga" + carga);
            Console.WriteLine("pasajero" + pasajero);
            Console.WriteLine("id_tp_combus" + id_tp_combus);
            Console.WriteLine("color" + color);
            Console.WriteLine("caja" + caja);
            Console.WriteLine("id_direccion" + direccion);
            Console.WriteLine("placa " + placa);

            _vehi_DAO = new Vehiculo_DAO();

            _marca_DAO = new Marca_DAO();
            tipo_direccion_DAO = new Tipo_Direccion_DAO();
            caja_Cambio_DAO = new Caja_Cambio_DAO();


           _tipo = tipo_direccion_DAO.Listartipodireccion(direccion);
           _caja = caja_Cambio_DAO.ListarCajas(caja);
           _marca = _marca_DAO.ListarMarca(id_marca);

            _vehi_DAO.Guardar_Especificcacio_Vehiculos(modelo, color, chasis, motor, cilindraje, int.Parse(id_tp_combus));
            _vehi_DAO.Guardar_Vehiculos(placa, int.Parse(pasajero), carga, int.Parse(id_categoria),_tipo.id_tipo_direccion,_caja.id_caja_cambios,int.Parse(id_marca),chasis);

            return RedirectToAction("RegistroVehiculo", "RegistroVehiculo");
        }
    }
}
