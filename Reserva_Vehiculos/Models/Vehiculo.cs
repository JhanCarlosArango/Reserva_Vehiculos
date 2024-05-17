using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reserva_Vehiculos.Models;

namespace Reserva_Vehiculos.Models
{
    public class Vehiculo
    {

        public String num_placa { get; set; }
        public String capacidad_pasajeros { get; set; }
        public String capacidad_carga { get; set; }
        public int fk_id_espec_vehiculo { get; set; }
        public int fk_id_categoria { get; set; }
        public int fk_id_marca { get; set; }
        public String color { get; set; }
        public String modelo { get; set; }
        public String num_chasis { get; set; }
        public String modelo_motor { get; set; }
        public String cilindraje { get; set; }
        public String tipo_vehiculo { get; set; }
        public int fk_id_tipo_combustible { get; set; }

        public Vehiculo()
        {

        }
    }
}