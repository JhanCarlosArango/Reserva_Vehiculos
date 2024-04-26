using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Persona
    {
        public int num_documento { get; set; }
        public string f_name { get; set; }
        public string s_name { get; set; }
        public string f_lastname { get; set; }
        public string s_lastname { get; set; }
        public string num_telefonico { get; set; }

        public Persona()
        {

        }
    }
}