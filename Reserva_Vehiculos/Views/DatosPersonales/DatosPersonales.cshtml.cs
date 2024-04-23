using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.DatosPersonales
{
    public class DatosPersonales : PageModel
    {
        private readonly ILogger<DatosPersonales> _logger;

        public DatosPersonales(ILogger<DatosPersonales> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}