using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
       
        //public ICollection<Puntuacion> Puntuaciones { get; set; }
    }
}
