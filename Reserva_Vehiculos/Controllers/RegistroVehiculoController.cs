using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;
namespace FrontEnd.Controllers
{
    [Authorize]
    public class RegistroVehiculoController : Controller
    {
        Categoria_DAO _cate_DAO;
        Marca_DAO _marca_DAO;
        List<Categoria> l_cate_list;
        List<Marca> l_marca_list;
        public IActionResult RegistroVehiculo()
        {
            _cate_DAO = new Categoria_DAO();
            l_cate_list = _cate_DAO.ListarCategoria();

            _marca_DAO = new Marca_DAO();
            l_marca_list = _marca_DAO.ListarMarca();


            var view_vehiculo = new Obj_View_Vehiculo
            {
                ListaCategorias = l_cate_list,
                ListaMarca = l_marca_list,
            };

            return View(view_vehiculo);
        }
    }
}
