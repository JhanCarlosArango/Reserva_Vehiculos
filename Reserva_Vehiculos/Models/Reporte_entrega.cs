using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Reporte_entrega
    {
        // voy a usar esta clase para daño vehiculo y reporte-daño
        int id_reporte { get; set; }
        int fk_id_ubicacion { get; set; }
        DateOnly fecha_entrega { get; set; }
        string hora_entrega { get; set; }
        string fk_num_placa { get; set; }

        public Reporte_entrega()
        {

        }

    }
}