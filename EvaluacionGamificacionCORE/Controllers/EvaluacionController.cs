using EvaluacionGamificacionCORE.DAL.Context;
using EvaluacionGamificacionCORE.DAL.Entity;
using EvaluacionGamificacionCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static EvaluacionGamificacionCORE.Models.EvaluacionModel;

namespace EvaluacionGamificacionCORE.Controllers
{
    public class EvaluacionController : Controller
    {
        private IConfiguration _configuration;
        private EvaluacionContext Context { get; }

        public EvaluacionController(IConfiguration configuration, IPerfil iperfil, ITipoMascota itipomascota, IPuntuacion ipuntuacion)
        {
            _perfil = iperfil;
            _tipomascota = itipomascota;
            _puntuacion = ipuntuacion;
            _configuration = configuration;

        }

        private readonly IPerfil _perfil;

        private readonly ITipoMascota _tipomascota;

        private readonly IPuntuacion _puntuacion;

        [HttpGet]
        public IActionResult Index()
        {
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

        public IActionResult Guardar([FromBody]EvaluacionModel model)
        {
            try
            {
                Puntuacion table = new Puntuacion();
                table.IdPerfil = model.SelectValuePerfil;
                table.IdTipoMascota = model.SelectValueTipoMascota;
                table.NumeroDocumento = model.NumeroDocumento;
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

    }
}
