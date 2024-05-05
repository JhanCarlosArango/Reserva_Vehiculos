using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Tipo_Direccion_DAO
    {
        private readonly Conexion conn;

        public Tipo_Direccion_DAO()
        {
            conn = new Conexion();
        }
    }
}