using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Preguntas
    {
        public Preguntas()
        {

        }
        
        public int id { get; set; }

        public string Pregunta { get; set; }

        public int IdPerfil { get; set; }

        public int IdTipoMascota { get; set; }

        public ICollection<Respuestas> _respuestas;
        
    }
}
