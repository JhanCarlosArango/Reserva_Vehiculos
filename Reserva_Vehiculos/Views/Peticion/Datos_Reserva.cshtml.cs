using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Datos_Reserva
{
    public class Datos_Reserva : PageModel
    {
        private readonly ILogger<Datos_Reserva> _logger;

        public Datos_Reserva(ILogger<Datos_Reserva> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}