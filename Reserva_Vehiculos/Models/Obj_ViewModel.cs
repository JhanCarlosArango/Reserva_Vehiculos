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
        public List<Pet_reserva> _lis_Pet_Reserva { get; set; }

        public Categoria categoria{ get; set; }
        public Persona _persona{ get; set; }
        public Usuarios _user_{ get; set; }
        public Vehiculo _vehiculo{ get; set; }
        public List<Vehiculo> _list_vehiculos{ get; set; }

        public Obj_ViewModel()
        {

        }
    }
}