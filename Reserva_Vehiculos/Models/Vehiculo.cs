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
        public int id_reserva_temp { get; set; }
        public String color { get; set; }
        public String modelo { get; set; }
        public String num_chasis { get; set; }
        public String modelo_motor { get; set; }
        public String cilindraje { get; set; }
        public String tipo_vehiculo { get; set; }
        public int fk_id_tipo_combustible { get; set; }

        //TEmporales
        public DateOnly temp_fecha_ini { get; set; }
        public DateOnly temp_fecha_fin { get; set; }
        public String temp_hora_ini { get; set; }
        public String temp_hora_fin { get; set; }
        public String temp_nombre_completo { get; set; }
        public String temp_num_documento { get; set; }
        public String temp_costo_reserva { get; set; }
        public String temp_tipo_vehiculo { get; set; }
        public Vehiculo()
        {

        }
    }
}