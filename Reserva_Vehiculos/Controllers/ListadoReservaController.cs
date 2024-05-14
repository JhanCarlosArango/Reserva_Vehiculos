using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models.DAO;
using iTextSharp.text.pdf;
using System.IO;
using Paragraph = iTextSharp.text.Paragraph;
using Document = iTextSharp.text.Document;
using PdfWriter = iTextSharp.text.pdf.PdfWriter;
using PageSize = iTextSharp.text.PageSize;

namespace Reserva_Vehiculos.Controllers
{
    [Authorize]
    public class ListadoReservaController : Controller
    {
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        String usuario_session;
        public ListadoReservaController(IHttpContextAccessor httpContextAccessor)
        {
            _IHttpContextAccessor = httpContextAccessor;
            usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");
        }
        Reserva_DAO reserva_DAO = new Reserva_DAO();
        public IActionResult ListadoReserva()
        {
            var viewModel = reserva_DAO.Listar_Reservas_para_admin();
            return View(viewModel);
        }


        public IActionResult MisReservas()
        {

            var viewModel = reserva_DAO.Listar_Reservas(usuario_session);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cancelar(String cancela)
        {
            reserva_DAO.CANCELAR_RESERVA(cancela);
            return RedirectToAction("ListadoReserva", "ListadoReserva");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost]
        public IActionResult Pdf_Listado_Reservas(String cancela)
        {

            var reservas = reserva_DAO.Listar_Reservas(usuario_session);

            // Crear un nuevo documento PDF
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);
            writer.CloseStream = false;

            document.Open();

            // Agregar un título
            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
            var title = new Paragraph("Reporte de Reservas", fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Chunk("\n")); // Agregar espacio

            // Crear la tabla con los datos de las reservas
            PdfPTable table = new PdfPTable(1); // El número de columnas
            table.WidthPercentage = 100; // Ancho relativo a la página

            // Agregar los nombres de las columnas
            table.AddCell("ID Reserva");


            // Llenar la tabla con los datos de las reservas
            foreach (var reserva in reservas._list_reserva)
            {
                table.AddCell(reserva.acep_fecha.ToString());

            }

            document.Add(table);

            // Agregar un pie de página
            var fontFooter = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12);
            var footer = new iTextSharp.text.Paragraph("Reporte generado el: " + System.DateTime.Now.ToString(), fontFooter);
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);

            document.Close();

            byte[] bytes = output.ToArray();
            output.Close();

            return File(bytes, "application/pdf", "ReporteDeReservas.pdf");
        }
    }
}


