using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models;
namespace Reserva_Vehiculos.Models{
    public class Usuarios{
        public int id_user {get;set;}
        public String usuario {get;set;}  
        public String contrasenia {get;set;}

        public List<Rol> rols {get;set;}
        public Usuarios(){

        }

    }
}