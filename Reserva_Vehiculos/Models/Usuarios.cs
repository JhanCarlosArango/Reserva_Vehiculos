using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models{
    public class Usuarios{
         int id_user {get;set;}
        public String usuario {get;set;}  
        public String contrasenia {get;set;}
        public Usuarios(){

        }

        public String Get_Usuario(){
            return this.usuario;
        }
        public String Get_Contrasenia(){
            return this.contrasenia;
        }

        public void Set_Usuario(String usuario){
            this.usuario = usuario;
        }
        public void Set_Contrasenia(String contrasenia){
            this.usuario = contrasenia;
        }
    }
}