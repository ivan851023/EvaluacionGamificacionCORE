using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.DAL.Entity
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Documento { get; set; }

        public string Nombre_Completo { get; set; }

        public string Email { get; set; }

        public string Direccion { get; set; }

        public DateTime Fecha_Nacimiento { get; set; }

        public string Telefono { get; set; }

        public string Password { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
