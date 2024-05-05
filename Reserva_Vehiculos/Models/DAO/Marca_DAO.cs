using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models.DAO
{
    public class Marca_DAO
    {

        private readonly Conexion conn;

        public Marca_DAO()
        {
            conn = new Conexion();
        }
    }
}