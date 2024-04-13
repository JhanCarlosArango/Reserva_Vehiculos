using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Resgistro_Vehi
{
    public class RV : PageModel
    {
        private readonly ILogger<RV> _logger;

        public RV(ILogger<RV> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}