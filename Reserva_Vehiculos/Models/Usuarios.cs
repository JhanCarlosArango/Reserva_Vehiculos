using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models;

namespace Reserva_Vehiculos.Models
{
    public class Usuarios
    {
        public int id_user { get; set; }
        public String usuario { get; set; }
        public String contrasenia { get; set; }

        public List<Rol> rols { get; set; }
        public Usuarios()
        {

        }
        public Usuarios(int id_user)
        {
            this.id_user = id_user;
        }
        //private String DatosUsuarioSesion()
        //{
        //    var Identity = HttpContext.User.Identity as ClaimsIdentity;
        //    var DatosUsuarioSesion = Identity.FindFirst(ClaimTypes.UserData).Value;
        //    return DatosUsuarioSesion;
        //   
        //}
    }
}