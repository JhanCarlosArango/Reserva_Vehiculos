using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Peticion
{
    public class Plantilla2 : PageModel
    {
        private readonly ILogger<Plantilla2> _logger;

        public Plantilla2(ILogger<Plantilla2> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}