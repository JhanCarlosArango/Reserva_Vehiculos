using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Peticion
{
    public class Peticion : PageModel
    {
        private readonly ILogger<Peticion> _logger;

        public Peticion(ILogger<Peticion> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}