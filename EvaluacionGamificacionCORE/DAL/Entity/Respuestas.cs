using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Respuestas
    {
        public int Id { get; set; }

        public string Respuesta { get; set; }

        public int Id_Pregunta { get; set; }

        public bool Correcta { get; set; }

        public bool Incorrecta { get; set; }

        public virtual Preguntas Preguntas { get; set; }

    }
}
