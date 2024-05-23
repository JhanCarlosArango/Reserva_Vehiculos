using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Peticion
{
    public class _AlertModal : PageModel
    {
        private readonly ILogger<_AlertModal> _logger;

        public _AlertModal(ILogger<_AlertModal> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}