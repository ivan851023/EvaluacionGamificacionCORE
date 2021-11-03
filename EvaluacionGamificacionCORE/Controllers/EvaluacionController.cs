using EvaluacionGamificacionCORE.DAL.Context;
using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.Resources;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static EvaluacionGamificacionCORE.Models.EvaluacionModel;


namespace EvaluacionGamificacionCORE.Controllers
{
    public class EvaluacionController : Controller
    {
        private IConfiguration _configuration;
        private EvaluacionContext Context { get; }


        public EvaluacionController(IConfiguration configuration, IPerfil iperfil, ITipoMascota itipomascota, IPuntuacion ipuntuacion, IVwPuntuacion ivwpuntuacion, IUsuario iusuario, IPreguntas ipreguntas, IRespuestas irespuestas)
        {
            _perfil = iperfil;
            _tipomascota = itipomascota;
            _puntuacion = ipuntuacion;
            _vwpuntuacion = ivwpuntuacion;
            _configuration = configuration;
            _usuario = iusuario;
            _preguntas = ipreguntas;
            _respuestas = irespuestas;
        }

        private readonly IPerfil _perfil;

        private readonly ITipoMascota _tipomascota;

        private readonly IPuntuacion _puntuacion;

        private readonly IVwPuntuacion _vwpuntuacion;

        private readonly IUsuario _usuario;

        private readonly IPreguntas _preguntas;

        private readonly IRespuestas _respuestas;

        [HttpGet]
        public IActionResult Index()
        {

            var idUsuario = int.Parse(HttpContext.User.Claims.Select(x => x.Value).FirstOrDefault());


            var puntuacion = _puntuacion.GetPuntuaciones().Where(x => x.IdUsuario == idUsuario).Count();

            ViewBag.Puntaje = (puntuacion == 0 ? true : false);
            
            EvaluacionModel model = new EvaluacionModel();
            model.lstPerfiles = GetPerfiles();
            model.lstTipoMascota = GetTipoMascotas();
            return View(model);
        }


        public IEnumerable<SelectListItem> GetPerfiles()
        {
            var perfiles = _perfil.GetPerfiles().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombre

            });

            return new SelectList(perfiles, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetTipoMascotas()
        {
            var tipoMascota = _tipomascota.GetTipoMascotas().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Nombre

            });

            return new SelectList(tipoMascota, "Value", "Text");
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] EvaluacionModel model)
        {
            try
            {
                Puntuacion table = new Puntuacion();
                table.IdPerfil = model.SelectValuePerfil;
                table.IdTipoMascota = model.SelectValueTipoMascota;
                table.IdUsuario = Convert.ToInt32(HttpContext.User.Claims.Select(x => x.Value).FirstOrDefault());
                table.FechaCreacion = DateTime.Now;
                table.Puntaje = model.Puntaje;
                _puntuacion.Insert(table);

                return View();
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpGet]
        public IActionResult ReportePuntaje()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<VwPuntuacion> LoadGridReportes()
        {
            IEnumerable<VwPuntuacion> result = _vwpuntuacion.GetDetallePuntuaciones().OrderByDescending(p => p.Id);
            var listGrid = (from x in result
                            select new VwPuntuacion
                            {

                                Id = x.Id,
                                Perfil = x.Perfil,
                                TipoMascota = x.TipoMascota,
                                Documento = x.Documento,
                                Nombre_Completo = x.Nombre_Completo,
                                Email = x.Email,
                                FechaCreacion = x.FechaCreacion,
                                Puntaje = x.Puntaje


                            }).AsEnumerable();

            return listGrid;
        }


        [HttpGet]
        public List<Puntuacion> GetAllPuntuaciones()
        {
            List<Puntuacion> result = _puntuacion.GetPuntuaciones().ToList();
            List<Perfil> perfil = _perfil.GetPerfiles().ToList();


            return result;
        }

        [HttpPost]
        public IActionResult ExportarExcel(string contentType, string base64, string filename)
        {
            var fileContents = Convert.FromBase64String(base64);
            return File(fileContents, contentType, filename);
        }

        [HttpGet]
        public IActionResult GeneraCertificado()
        {
            return View();
        }


        [HttpGet]
        public IEnumerable<VwPuntuacion> LoadGridCertificados()
        {
            IEnumerable<VwPuntuacion> result = _vwpuntuacion.GetDetallePuntuaciones().OrderByDescending(p => p.Id);
            var idUsuario = int.Parse(HttpContext.User.Claims.Select(x => x.Value).FirstOrDefault());
            var listGrid = (from x in result
                            select new VwPuntuacion
                            {

                                Id = x.Id,
                                Documento = x.Documento,
                                FechaCreacion = x.FechaCreacion,
                                Puntaje = x.Puntaje,
                                IdUsuario = x.IdUsuario


                            }).Where(x => x.Puntaje >= 80 && x.IdUsuario == idUsuario).AsEnumerable();

            return listGrid;
        }

        [HttpPost]
        public JsonResult DescargarPdf(string id)
        {
            Document doc = new Document(PageSize.LETTER);
            doc.SetMargins(75f, 75f, 40f, 20f);
            var ruta = _configuration["RutaPdf"].ToString();

            var rutaArchivo = Path.Combine(ruta, "Prueba.pdf");


            PdfWriter.GetInstance(doc, new FileStream(rutaArchivo, FileMode.Create));
            doc.Open();

            int idPuntaje = Convert.ToInt32(id);

            var puntuacion = _puntuacion.GetPuntuaciones().Where(x => x.Id == idPuntaje).FirstOrDefault();
            var usuario = _usuario.GetUsuarios().Where(x => x.Id == puntuacion.IdUsuario).FirstOrDefault();

            CreateCertificadoPDF(doc,usuario);

            doc.Close();
            MemoryStream workStream = new MemoryStream();
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;

            var base64string = ObtenerStringBase64Archivo(rutaArchivo);
            return Json(new { data = base64string, filename = "Certificado".Replace(".pdf", "") });
                        
        }

        public void CreateCertificadoPDF(Document doc, Usuario user)
        {
            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 12);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
           
            float marginLine = 1.5f;
            float[] headers = { 60, 60 }; //Header Widths  

            doc = agregarEncabezadoFalabella(doc, headers);
                       
            Paragraph phrase = new Paragraph
            {
                Alignment = Element.ALIGN_CENTER
            };
            phrase.Add(new Chunk("CERTIFICADO IDONEIDAD ADOPCION MASCOTAS", fuenteNegrita));
            insertarLineaEnBlanco(phrase, 3);
            doc.Add(phrase);

            phrase = new Paragraph
            {
                Alignment = Element.ALIGN_JUSTIFIED
            };
            phrase.Add(new Chunk("Entre los suscritos ", fuenteNormal));
            phrase.Add(new Chunk(user.Nombre_Completo, fuenteNegrita));
            phrase.Add(new Chunk(" identificado con cédula de ciudadanía " + user.Documento + "", fuenteNormal));
            phrase.Add(new Chunk(" FALABELLA DE COLOMBIA S.A.,", fuenteNegrita));
            phrase.Add(new Chunk(" quien se denomina", fuenteNormal));
            phrase.Add(new Chunk(" EL ADOPTANTE", fuenteNegrita));
            phrase.Add(new Chunk(", y de otra parte, ", fuenteNormal));
            phrase.Add(new Chunk("Lizeth Hurtado", fuenteNegrita));
            phrase.Add(new Chunk(", identificado(a) con cédula de ciudadanía número ", fuenteNormal));
            phrase.Add(new Chunk("1013000007", fuenteNegrita));
            phrase.Add(new Chunk(", quien en adelante se denomina,", fuenteNormal));
            phrase.Add(new Chunk(" Representante fundaciones", fuenteNegrita));
            phrase.Add(new Chunk(", ha convenido de manera libre y espontanea de dar la respectiva aprobacion para empezar el proceso de adopción.", fuenteNormal));
            phrase.SetLeading(marginLine, marginLine);
            insertarLineaEnBlanco(phrase, 2);
            doc.Add(phrase);


            
          

            PdfPTable tabla = new PdfPTable(2);
            tabla.SetWidths(headers);
            tabla.WidthPercentage = 100;

            tabla.AddCell(obtenerCeldaConTextoFirma("REPRESENTANTE DE FUNDACIONES", 12, Font.BOLD, 12, 12, Element.ALIGN_LEFT));
            tabla.AddCell(obtenerCeldaConTextoFirma("ADOPTANTE", 12, Font.BOLD, 12, 12, Element.ALIGN_LEFT));

            Image imagenFirma = AgregarImagenPdfParametrizada();
            imagenFirma.ScaleToFit(80f, 80f);
            tabla.AddCell(obtenerCeldaImagen(imagenFirma, 0, 0, 0, 0, 0, 0, 10, 0, Element.ALIGN_LEFT));

            tabla.AddCell(obtenerCeldaConTextoFirma("", 10, Font.NORMAL, 0, 0, Element.ALIGN_LEFT));

            tabla.AddCell(obtenerCeldaConTextoFirma("Lizeth Hurtado", 12, Font.NORMAL, 12, 12, Element.ALIGN_LEFT));
            tabla.AddCell(obtenerCeldaConTextoFirma(user.Nombre_Completo, 12, Font.NORMAL, 12, 12, Element.ALIGN_LEFT));
            tabla.AddCell(obtenerCeldaConTextoFirma("C.C. " + "1013000007", 12, Font.NORMAL, 0, 12, Element.ALIGN_LEFT));
            tabla.AddCell(obtenerCeldaConTextoFirma("C.C. " + user.Documento, 12, Font.NORMAL, 0, 12, Element.ALIGN_LEFT));
            
            doc.Add(tabla);

           
        }

        public Paragraph insertarLineaEnBlanco(Paragraph paragraph, int numeroSaltosBlanco)
        {
            for (int i = 0; i < numeroSaltosBlanco; i++)
            {
                paragraph.Add(Chunk.NEWLINE);
            }

            return paragraph;
        }

        private PdfPCell obtenerCeldaConTextoFirma(string texto, float tamanoFuente,
            int tipoFuente, float paddingTop, float paddingLeft, int horizontalAlignment)
        {
            var phrase = new Paragraph
            {
                new Chunk(texto, FontFactory.GetFont(FontFactory.HELVETICA, tamanoFuente, tipoFuente))
            };

            var celda = new PdfPCell(phrase)
            {
                PaddingTop = paddingTop,
                PaddingLeft = paddingLeft,
                BorderWidthLeft = 0,
                BorderWidthRight = 0,
                BorderWidthTop = 0,
                BorderWidthBottom = 0
            };

            return celda;
        }

        public Image AgregarImagenPdfParametrizada()
        {

            var rutaFirma = _configuration["RutaFirma"].ToString();


            var NuevaFirma = Image.GetInstance(rutaFirma);
            

            return NuevaFirma;

        }

        private PdfPCell obtenerCeldaImagen(Image imagenFirma,
            int colspan, int estilo,
            float borderWidthLeft, float borderWidthRight, float borderWidthTop, float BorderWidthBottom,
            float paddingTop, float paddingBottom, int alineacion
            )
        {
            var celda = new PdfPCell(imagenFirma)
            {
                Colspan = colspan,
                BorderWidthLeft = borderWidthLeft,
                BorderWidthRight = borderWidthRight,
                BorderWidthTop = borderWidthTop,
                BorderWidthBottom = BorderWidthBottom,
                HorizontalAlignment = alineacion,
                PaddingTop = paddingTop,
                PaddingBottom = paddingBottom
            };

            return celda;
        }

        public string ObtenerStringBase64Archivo(string ruta)
        {
            string base64 = "";
            byte[] stream = System.IO.File.ReadAllBytes(ruta);
            base64 = Convert.ToBase64String(stream);
            return base64;
        }

        private Document agregarEncabezadoFalabella(Document doc, float[] headers)
        {

            PdfPTable tabla = new PdfPTable(2);
            tabla.SetWidths(headers); //Set the pdf headers  
            tabla.WidthPercentage = 100; //Set the PDF File witdh percentage            

            Image imagenFallabella = AgregarImagenPdfParametrizadaEncabezado();
            imagenFallabella.ScaleToFit(447f, 420f);
            tabla.AddCell(obtenerCeldaImagen(imagenFallabella, 12, 0, 0, 0, 0, 0, 0, 0, Element.ALIGN_LEFT));
            doc.Add(tabla);

            return doc;
        }

        public Image AgregarImagenPdfParametrizadaEncabezado()
        {
            Image imagenEncabezado = Image.GetInstance(_configuration["RutaEncabezado"].ToString());

            return imagenEncabezado;
        }

        [HttpPost]
        public IActionResult GetRespuestasPreguntas([FromBody] EvaluacionModel model)
        {

            var preguntas = _preguntas.GetPreguntas().Where(x => x.IdPerfil == Convert.ToInt32(model.SelectValuePerfil)
            && x.IdTipoMascota == Convert.ToInt32(model.SelectValueTipoMascota));

            if (preguntas.Count() == 0)
            {
                return RedirectToAction("Index", "Evaluacion");
            }

            var respuestas = _respuestas.GetRespuestas().Where(y => preguntas.Contains(y.Preguntas));


            List<AllQuestions> lstQuestions = new List<AllQuestions>();


            foreach (var item in preguntas)
            {
                AllQuestions all = new AllQuestions();
                all.Choices = new Choices();
                all.Choices.Correct = new List<string>();
                all.Choices.Wrong = new List<string>();
                all.Question = item.Pregunta;

                foreach (var j in respuestas.Where(x => x.Id_Pregunta == item.id))
                {
                    if (j.Correcta)
                    {
                        all.Choices.Correct.Add(j.Respuesta);
                    }
                    else
                    {
                        all.Choices.Wrong.Add(j.Respuesta);
                    }

                    
                }
                lstQuestions.Add(all);
            }



            var json = JsonSerializer.Serialize(lstQuestions);

            
            return Json(lstQuestions);
        }
    }
}
