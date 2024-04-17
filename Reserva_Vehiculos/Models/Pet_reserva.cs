using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Pet_reserva
    {
        int id_pet_reserva;
        public DateOnly fecha_ini { set; get; }
        public DateOnly fecha_fin { set; get; }
        public String hora_ini { set; get; }
        public String hora_fin { set; get; }
        public String estado { set; get; }
        public Double costo { set; get; }

        public String año { set; get; }
        public String mes { set; get; }
        public String dia { set; get; }

        public String año2 { set; get; }
        public String mes2 { set; get; }
        public String dia2 { set; get; }
        public String fecha { set; get; }
        public String fecha1 { set; get; }
        public Pet_reserva()
        {

        }


        public void separa_feha2(String Fecha)
        {

            string[] partesFecha = Fecha.Split('-');
            año2 = partesFecha[0];
            mes2 = partesFecha[1];
            dia2 = partesFecha[2];
        }

        public void separa_feha(string Fecha)
        {

            string[] partesFecha = Fecha.Split('-');

            año = partesFecha[0];
            mes = partesFecha[1];
            dia = partesFecha[2];
            
        }
    }

}