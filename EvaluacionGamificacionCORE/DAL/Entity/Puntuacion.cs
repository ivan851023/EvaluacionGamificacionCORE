using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Puntuacion
    {
        public int Id { get; set; }

    
        public int IdPerfil { get; set; }
        //public Perfil Perfil { get; set; }

        public int IdTipoMascota { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaCreacion { get; set; }

        public long Puntaje { get; set; }
        
    }
}
