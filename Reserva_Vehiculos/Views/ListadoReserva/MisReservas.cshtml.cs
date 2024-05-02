using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.ListadoReserva
{
    public class MisReservas : PageModel
    {
        private readonly ILogger<MisReservas> _logger;

        public MisReservas(ILogger<MisReservas> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}