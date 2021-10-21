using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class VwPuntuacion
    {

        public int Id { get; set; }

        public string Perfil { get; set; }

        public string TipoMascota { get; set; }

        public string NumeroDocumento { get; set; }

        public DateTime FechaCreacion { get; set; }

        public long Puntaje { get; set; }
    }
}
