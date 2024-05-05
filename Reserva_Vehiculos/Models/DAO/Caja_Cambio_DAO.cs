using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Caja_Cambio_DAO
    {
        private readonly Conexion conn;

        public Caja_Cambio_DAO()
        {
            conn = new Conexion();
        }
    }
}
