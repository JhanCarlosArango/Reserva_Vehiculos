using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Reserva_Vehiculos.Views.Categoria
{
    public class Categoria : PageModel
    {
        private readonly ILogger<Categoria> _logger;

        public Categoria(ILogger<Categoria> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}