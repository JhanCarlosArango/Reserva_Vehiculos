using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
     public class Pet_reserva
    {
        public int id_pet_reserva { set; get; }
        public DateOnly fecha_ini { set; get; }
        public DateOnly fecha_fin { set; get; }
        public String hora_ini { set; get; }
        public String hora_fin { set; get; }
        public String estado { set; get; }
        public Double temp_costo { set; get; }

        public String año { set; get; }
        public String mes { set; get; }
        public String dia { set; get; }

        public String año2 { set; get; }
        public String mes2 { set; get; }
        public String dia2 { set; get; }
        public String fecha { set; get; }
        public String fecha1 { set; get; }
        public int fk_id_categoria { set; get; }
        public int fk_id_usuario { set; get; }
        public int fk_id_ubicacion_inicial { set; get; }
        public int fk_id_ubicacion_final { set; get; }
        public String ubicacion_inicial { set; get; }
        public String ubicacion_final { set; get; }
        public decimal costo { set; get; }
        public Pet_reserva()
        {

        }
        public Pet_reserva(DateOnly fecha_ini, DateOnly fecha_fin, String hora_ini, String hora_fin,int fk_id_ubicacion_inicial, int fk_id_ubicacion_final,decimal costo)
        {
            this.fecha_ini = fecha_ini;
            this.fecha_fin = fecha_fin;
            this.hora_ini = hora_ini;
            this.hora_fin = hora_fin;
            this.fk_id_ubicacion_inicial = fk_id_ubicacion_inicial;
            this.fk_id_ubicacion_final = fk_id_ubicacion_final;
            this.costo = costo;
        }


        public void separa_feha2(String Fecha)
        {

            string[] partesFecha = Fecha.Split('/');
            año2 = partesFecha[0];
            mes2 = partesFecha[1];
            dia2 = partesFecha[2];
        }

        public void separa_feha(string Fecha)
        {

            string[] partesFecha = Fecha.Split('/');

            año = partesFecha[0];
            mes = partesFecha[1];
            dia = partesFecha[2];

        }
        public  string ObtenerPrimeraParteSeparadaPorEspacio(string input)
        { 
            string[] partes = input.Split(' ');
            return partes[0]; // Devolver la primera parte
        }
        public Double calcular_costo(Double a, Double b)
        {
            Double c = a * b;
            return c;
        }

    }

}