using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Empresa
    {
        public int nit { get; set; }
        public String name_empresa { get; set; }
        public String direccion { get; set; }
        public String correo_em { get; set; }
        public String telefono { get; set; }
        public String logo { get; set; }
        public Empresa()
        {

        }
    }
}