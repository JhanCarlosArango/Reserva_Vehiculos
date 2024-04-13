using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
namespace Reserva_Vehiculos.Models
{
    public class Vehiculo_doc
    {
        public IFormFile doc_legal { set; get;} // agregar sintaxi asp.net
    }
}