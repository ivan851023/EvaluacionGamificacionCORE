using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo documento es obligatorio")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo password es obligatorio")]
        public string Password { get; set; }

    }
}
