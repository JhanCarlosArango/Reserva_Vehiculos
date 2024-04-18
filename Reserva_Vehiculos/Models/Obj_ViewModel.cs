using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Obj_ViewModel
    {
        public List<Categoria> ListaCategorias { get; set; }
        public Pet_reserva _Pet_Reserva { get; set; }

      

        public Obj_ViewModel()
        {

        }
    }
}