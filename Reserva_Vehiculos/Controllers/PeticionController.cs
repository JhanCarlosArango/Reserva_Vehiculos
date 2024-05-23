
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reserva_Vehiculos.Models;
using Reserva_Vehiculos.Models.DAO;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using System.Net.Mail;
using iText.Html2pdf;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Org.BouncyCastle.Asn1.Cmp;
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
        Categoria_DAO categoria_;
        Persona per;
        DateTime fecha_i;
        DateTime fecha_f;
        Empresa_DAO empresa_DAO;
        String[] columnas = { "Nombre", "Fecha Recojida", "Fecha Devolucion", "Hora Recojida", "Hora Devolucion", "Barrio Recojida", "Barrio Devolucion", "Categoria", "Total" };
        private readonly IHttpContextAccessor _IHttpContextAccessor;
        private byte[] _pdfBytes;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IActionContextAccessor _actionContextAccessor;
        public Peticion(IActionContextAccessor actionContextAccessor, IHttpContextAccessor httpContextAccessor, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider, IServiceProvider serviceProvider, IWebHostEnvironment hostingEnvironment)
        {
            _IHttpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _hostingEnvironment = hostingEnvironment;
            _pdfBytes = Array.Empty<byte>(); // Inicializar _pdfBytes con un arreglo vacío
        }
        public IActionResult peticion()
        {
            categoria_ = new Categoria_DAO();
            _ubi_DAO = new Ubicacion_DAO();
            _ubi = _ubi_DAO.listar_ubicacion();

            var l_cate = categoria_.ListarCategoria();
            var viewModel = new Obj_ViewModel
            {
                ListaCategorias = l_cate,
                _list_ubicaion = _ubi,   // _ViewModel_pet_Reserva pasa info a _Pet_Reserva

            };
            ////aqui llamo ubicacin
            return View(viewModel);
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
        public IActionResult Datos_Reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, String id_cate, int fk_id_ubicacion_inicial, int fk_id_ubicacion_final, decimal costo)
        {
            persona_DAO = new Persona_DAO();
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");

            per = persona_DAO.Search_persona(usuario_session);

            var viewModel_1 = new Obj_ViewModel
            {

                _Pet_Reserva = new Pet_reserva(fecha_ini, fecha_fin, hora_ini, hora_fin, fk_id_ubicacion_inicial, fk_id_ubicacion_final, costo), // este contexto
                categoria = new Categoria(int.Parse(id_cate)),
                _persona = per
            };

            return View(viewModel_1);
        }

        public IActionResult Enviar_Datos_Reserva(DateOnly fecha_ini, String hora_ini, DateOnly fecha_fin, String hora_fin, int id_categoria, int fk_id_ubicacion_inicial, int fk_id_ubicacion_final, decimal costo, String corre)
        {
            _Usuario_DAO = new Usuario_DAO();
            _Reserva_DAO = new Pet_reserva_DAO();
            String usuario_session = _IHttpContextAccessor.HttpContext.Session.GetString("name_user");
            int id_usuario = _Usuario_DAO.Obtener_ID_usuario(usuario_session);
            _Reserva_DAO.Guardar_Pet_reserva(fecha_ini, hora_ini, fecha_fin, hora_fin, fk_id_ubicacion_inicial, fk_id_ubicacion_final, id_categoria, id_usuario, costo);

            //aqui debo poner la descarga del pdf 
            var model_PDF_pet = _Reserva_DAO.Listar_PDF_Pet_Reservas(usuario_session);

            string destinatarioEmail = corre; // Dirección de correo predefinida 

            // Generar el PDF si aún no se ha generado
            _pdfBytes = GenerarPDF(model_PDF_pet);

            // Enviar el PDF por correo y obtener el resultado
            int envioExitoso = EnviarCorreoConPDF(destinatarioEmail, _pdfBytes);

            if (envioExitoso == 1)
            {
                TempData["Message"] = "Recibo Generado Correctamente, Revisa tu Correo Electronico";
                return RedirectToAction("peticion", "Peticion");
            }
            else
            {
                return View();
            }
        }

        private byte[] GenerarPDF(Obj_ViewModel PDF_pet)
        {
            empresa_DAO = new Empresa_DAO(); ///datos de la empresa
            var empresa = empresa_DAO.traer_empresa();
            // Obtener la ruta completa de la imagen
            string imagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", "logo.png");

            // Renderizar la vista Plantilla a una cadena HTML
            string html = RenderViewToString("Plantilla", new { ImageSrc = imagePath });

            // Reemplazar los marcadores de posición con los valores obtenidos desde el modelo
            html = html
                .Replace("{Nombre de la Empresa}", empresa.name_empresa)
                .Replace("{nit}", empresa.nit.ToString())
                .Replace("{1}", PDF_pet._Pet_Reserva.fecha_ini.ToString("dd/MM/yyyy"))
                .Replace("{2}", PDF_pet._Pet_Reserva.hora_ini)
                .Replace("{3}", PDF_pet._ubi_.Ubicacion_ini)
                .Replace("{4}", PDF_pet.categoria.tipo_vehiculo)
                .Replace("{5}", PDF_pet._Pet_Reserva.fecha_fin.ToString("dd/MM/yyyy"))
                .Replace("{6}", PDF_pet._Pet_Reserva.hora_fin)
                .Replace("{7}", PDF_pet._ubi_.ubicacion_fin)
                .Replace("{8}", PDF_pet._Pet_Reserva.costo.ToString())
                .Replace("{Dirección}", empresa.direccion)
                .Replace("{Teléfono}", empresa.telefono)
                .Replace("{Correo Electrónico}", empresa.correo_em)
                .Replace("{Fecha}", DateTime.Now.ToString("dd/MM/yyyy"));

            // Crear un MemoryStream para el PDF
            MemoryStream output = new MemoryStream();

            // Convertir HTML a PDF
            HtmlConverter.ConvertToPdf(html, output);

            return output.ToArray();
        }

        // Método para renderizar una vista a una cadena HTML
        private string RenderViewToString(string viewName, object model)
        {
            // Obtener el contexto de acción actual
            var actionContext = _actionContextAccessor.ActionContext;
            if (actionContext == null)
            {
                throw new InvalidOperationException("No se pudo obtener el contexto de la acción actual.");
            }

            // Definir la ruta completa de la vista
            string viewPath = Path.Combine("Views", "Peticion", $"{viewName}.cshtml");

            // Obtener la vista usando la ruta completa
            var viewResult = _viewEngine.GetView(executingFilePath: null, viewPath: viewPath, isMainPage: false);

            // Verificar si la vista existe
            if (!viewResult.Success)
            {
                Console.WriteLine($"No se encontró la vista en la ruta: {viewPath}");
                // Lanzar una excepción si la vista no existe
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            // Crear un diccionario de datos para la vista
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                // Asignar el modelo a la vista
                Model = model
            };

            // Crear un diccionario temporal de datos
            var tempData = new TempDataDictionary(actionContext.HttpContext, _tempDataProvider);

            // Crear un escritor para escribir el resultado de la vista
            using (var writer = new StringWriter())
            {
                // Crear un contexto de vista
                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewData,
                    tempData,
                    writer,
                    new HtmlHelperOptions()
                );

                // Renderizar la vista en el escritor
                viewResult.View.RenderAsync(viewContext).GetAwaiter().GetResult();

                // Retornar el resultado como una cadena
                return writer.ToString();
            }
        }

        // Método para enviar correo electrónico con el PDF adjunto
        private int EnviarCorreoConPDF(string destinatarioEmail, byte[] pdfBytes)
        {
            try
            {
                // Configurar el cliente SMTP para enviar el correo
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("arangousugajhancarlos@gmail.com", "kvdp laho ilev refz"),
                    EnableSsl = true
                };

                // Crear el correo electrónico
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("arangousugajhancarlos@gmail.com"),
                    Subject = "Reporte de Reservas",
                    Body = "Adjunto encontrarás el reporte de reservas en formato PDF."
                };

                // Adjuntar el PDF al correo electrónico
                MemoryStream pdfStream = new MemoryStream(pdfBytes);
                mailMessage.Attachments.Add(new Attachment(pdfStream, "ReporteDeReservas.pdf", "application/pdf"));

                // Agregar el destinatario del correo
                mailMessage.To.Add(new MailAddress(destinatarioEmail));

                try
                {
                    // Enviar el correo electrónico
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    // Manejar cualquier excepción que ocurra durante el envío del correo
                    Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
                    return 0; // Error al enviar el correo
                }

                // Cerrar y eliminar el MemoryStream después de enviar el correo
                pdfStream.Close();
                pdfStream.Dispose();

                return 1; // Éxito al enviar el correo
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que ocurra durante la configuración del correo
                Console.WriteLine("Error al configurar el correo electrónico: " + ex.Message);
                return 0; // Error al configurar el correo
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}