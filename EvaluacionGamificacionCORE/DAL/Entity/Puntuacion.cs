using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Puntuacion
    {
        public int Id { get; set; }

        public int IdPerfil { get; set; }

        public int IdTipoMascota { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime FechaCreacion { get; set; }

        public long Puntaje { get; set; }
    }
}
