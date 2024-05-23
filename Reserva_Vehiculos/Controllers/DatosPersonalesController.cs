using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;

namespace FrontEnd.Controllers
{
    public class DatosPersonalesController : Controller
    {

        Usuario_DAO _user_DAO;
        Persona_DAO persona_DAO;
        public IActionResult DatosPersonales()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Enviar_DatosPersonales(int num_documento, String primer_nombre, String segundo_nombre, String primer_apellido, String segundo_apellido, String num_telefonico, int fk_id_tipo_doc, String correo, String usuario, String contrasenia)
        {

            persona_DAO = new Persona_DAO();
            persona_DAO.Guardar_Persona(num_documento, primer_nombre, segundo_nombre, primer_apellido, segundo_apellido, num_telefonico, fk_id_tipo_doc, correo);

            _user_DAO = new Usuario_DAO();
            _user_DAO.Guardar_usuario(num_documento, usuario, contrasenia);
            int id_usuario = _user_DAO.Get_id_USUARIO();
            _user_DAO.Guardar_rol_USuario(id_usuario);

            return RedirectToAction("Login", "User");
        }
    }
}
