using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva_Vehiculos.Models
{
    public class Categoria
    {
        public int id_categoria { get; set; }
        public String tipo_vehiculo { get; set; }
        public Double costo { get; set; }
        public byte[] Imagen { get; set; } // La imagen de la categor�a
        public string RutaImagen { get; set; } // La ruta de la imagen

        public string nombre_imagen { get; set; } // Nombre de la imagen
        public string ruta_imagen { get; set; }
        public Categoria()
        {

        }
        public Categoria(int id_categoria)
        {
            this.id_categoria = id_categoria;
        }

    }
}