using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Obj_View_Vehiculo
    {
        public List<Categoria> ListaCategorias { get; set; }
        public List<Marca> ListaMarca { get; set; }
        public List<Caja_Cambio> ListaCaja { get; set; }
        public List<Tipo_Direccion> ListatipoDerrecion { get; set; }
        public Obj_View_Vehiculo()
        {

        }
    }
}