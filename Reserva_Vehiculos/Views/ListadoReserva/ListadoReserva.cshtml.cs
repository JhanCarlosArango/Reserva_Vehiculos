using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.ListadoReserva
{
    public class ListadoReserva : PageModel
    {
        private readonly ILogger<ListadoReserva> _logger;

        public ListadoReserva(ILogger<ListadoReserva> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}