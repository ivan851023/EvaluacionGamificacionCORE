﻿using EvaluacionGamificacionCORE.DAL.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.Models
{
    public class EvaluacionModel
    {
        public List<Respuestas> lstRespuestas { get; set; }


        public class Respuestas
        {
            public long id { get; set; }
            public bool respuesta { get; set; }

        }

        public long? SelectValuePerfil { get; set; }

        public IEnumerable<SelectListItem> lstPerfiles { get; set; }

        public long? SelectValueTipoMascota { get; set; }

        public IEnumerable<SelectListItem> lstTipoMascota { get; set; }

    }
}