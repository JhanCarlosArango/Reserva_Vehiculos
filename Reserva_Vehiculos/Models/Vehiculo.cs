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
        public String aire_acondicionado { get; set; }
        public String capacidad_pasajeros { get; set; }
        public String capacidad_carga { get; set; }
        public int fk_id_categoria { get; set; }

        public Vehiculo()
        {

        }

// se listan todos los vehiculos
        //public List<Vehiculo> Flitar_veiculos_categoria(List<Vehiculo> lista, int idCategoria) //Filtar vehiculos 
        //{
        //    return lista.Where(elemento => elemento.fk_id_categoria == idCategoria).ToList();
        //}

    }
}