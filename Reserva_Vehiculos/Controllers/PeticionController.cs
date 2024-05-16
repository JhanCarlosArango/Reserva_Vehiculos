using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;
namespace Reserva_Vehiculos.Controllers
{
    public class Peticion : Controller
    {
        Pet_reserva _Reserva;
        Persona_DAO persona_DAO;
        Pet_reserva_DAO _Reserva_DAO;
        Usuario_DAO _Usuario_DAO;
        Ubicacion_DAO _ubi_DAO;
        List<Ubicacion> _ubi;
        Persona per;
        DateTime fecha_i;
        DateTime fecha_f;
        String[] columnas = { "Nombre", "Fecha Recojida", "Fecha Devolucion", "Hora Recojida", "Hora Devolucion", "Barrio Recojida", "Barrio Devolucion", "Categoria", "Total" };
        private readonly IHttpContextAccessor _IHttpContextAccessor; 
        public Peticion(IHttpContextAccessor httpContextAccessor)
        {
            _IHttpContextAccessor = httpContextAccessor;
        }
        public IActionResult peticion()
        {
            _ubi_DAO = new Ubicacion_DAO();
            _ubi = _ubi_DAO.listar_ubicacion();
            ////aqui llamo ubicacin
            return View(_ubi);
        }
        // Paso 1 esta en la vista peticion.cshtml
        [HttpPost]

        public IActionResult peticion(DateOnly fechaini, String hora_ini, DateOnly fechafin, String hora_fin, int id_ubicacion, int id_ubicacion_fin)// Paso 2
        {
            if (!id_ubicacion.Equals("") && !id_ubicacion_fin.Equals(""))
            {

                Console.WriteLine("Recojida " + id_ubicacion);
                Console.WriteLine("DEvolucion " + id_ubicacion_fin);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Debe Selecionar la ubiacion");
                return View();
            }
            _Reserva = new Pet_reserva();
            string fechainiStr = fechaini.ToString();

            _Reserva.separa_feha(fechainiStr);

            string fechafinStr = fechafin.ToString();
            _Reserva.separa_feha2(fechafinStr);

            fecha_i = new DateTime(int.Parse(_Reserva.dia), int.Parse(_Reserva.mes), int.Parse(_Reserva.año));
            fecha_f = new DateTime(int.Parse(_Reserva.dia2), int.Parse(_Reserva.mes2), int.Parse(_Reserva.año2));

            TimeSpan diferencia = fecha_f - fecha_i;
            _Reserva.temp_costo = diferencia.Days;

            _Reserva.fecha_ini = new DateOnly(fecha_i.Year, fecha_i.Month, fecha_i.Day);
            _Reserva.fecha_fin = new DateOnly(fecha_f.Year, fecha_f.Month, fecha_f.Day);

            _Reserva.hora_ini = hora_ini;
            _Reserva.hora_fin = hora_fin;

            _Reserva.fk_id_ubicacion_inicial = id_ubicacion;
            _Reserva.fk_id_ubicacion_final = id_ubicacion_fin;
            Console.WriteLine("Dias de arquiler :" + _Reserva.temp_costo);
            return RedirectToAction("categoria_view", "Categoria", _Reserva); // Paso 3 esta en la vista categoria_view
        }

        [Authorize]
        [HttpPost]
        public IActionResult Datos_Reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, String id_cate, int fk_id_ubicacion_inicial, int fk_id_ubicacion_final)
        {
            persona_DAO = new Persona_DAO();
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");

            per = persona_DAO.Search_persona(usuario_session);

            var viewModel_1 = new Obj_ViewModel
            {

                _Pet_Reserva = new Pet_reserva(fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_ubicacion_inicial, fk_id_ubicacion_final), // este contexto
                categoria = new Categoria(int.Parse(id_cate)),
                _persona = per
            };

            return View(viewModel_1);
        }

        public IActionResult Enviar_Datos_Reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, int id_categoria, int fk_id_ubicacion_inicial, int fk_id_ubicacion_final)
        {
            _Usuario_DAO = new Usuario_DAO();
            _Reserva_DAO = new Pet_reserva_DAO();
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");
            int id_usuario = _Usuario_DAO.Obtener_ID_usuario(usuario_session);
            _Reserva_DAO.Guardar_Pet_reserva(fecha_ini, hora_ini, fecha_fin, hora_fin, fk_id_ubicacion_inicial, fk_id_ubicacion_final, id_categoria, id_usuario);

            //aqui debo poner la descarga del pdf 

            var model_PDF_pet = _Reserva_DAO.Listar_PDF_Pet_Reservas(usuario_session);
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            var output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);
            writer.CloseStream = false;

            document.Open();

            // Agregar un título
            var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            var title = new Paragraph("Reporte de Reservas", fontTitle);
            title.Alignment = Element.ALIGN_CENTER;
            document.Add(title);
            document.Add(new Chunk("\n")); // Agregar espacio

            // Crear la tabla con los datos de las reservas
            PdfPTable table = new PdfPTable(columnas.Length); // El número de columnas
            table.WidthPercentage = 100; // Ancho relativo a la página

            // Agregar los nombres de las columnas
            foreach (var item in columnas)
            {
                table.AddCell(item);
            }


            // Llenar la tabla con los datos de las reservas

            table.AddCell(model_PDF_pet._persona.f_name);
            table.AddCell(model_PDF_pet._Pet_Reserva.fecha_ini.ToString());
            table.AddCell(model_PDF_pet._Pet_Reserva.fecha_fin.ToString());
            table.AddCell(model_PDF_pet._Pet_Reserva.hora_ini.ToString());
            table.AddCell(model_PDF_pet._Pet_Reserva.hora_fin.ToString());
            table.AddCell(model_PDF_pet._ubi_.Ubicacion_ini.ToString());
            table.AddCell(model_PDF_pet._ubi_.Ubicacion_ini.ToString());
            table.AddCell(model_PDF_pet.categoria.tipo_vehiculo.ToString());
            table.AddCell("120");


            document.Add(table);

            // Agregar un pie de página
            var fontFooter = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12);
            var footer = new iTextSharp.text.Paragraph("Reporte generado el: " + System.DateTime.Now.ToString(), fontFooter);
            footer.Alignment = Element.ALIGN_CENTER;
            document.Add(footer);

            document.Close();

            byte[] bytes = output.ToArray();
            output.Close();

            return File(bytes, "application/pdf", "ReporteDeReservas.pdf", true);
            //return RedirectToAction("peticion", "Peticion");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}