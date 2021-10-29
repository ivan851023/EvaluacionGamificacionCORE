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

        public string Documento { get; set; }

        public string Nombre_Completo { get; set; }

        public string Email { get; set; }

        public DateTime FechaCreacion { get; set; }

        public long Puntaje { get; set; }

        public int IdUsuario { get; set; }
    }
}
