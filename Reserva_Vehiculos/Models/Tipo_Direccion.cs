using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models.DAO;

namespace Reserva_Vehiculos.Models
{
   
    public class Tipo_Direccion
    {

        public Tipo_Direccion(){
          
        }
        int id_tipo_direccion { get; set; }
        String tipo_direccion { get; set; }
    }
}