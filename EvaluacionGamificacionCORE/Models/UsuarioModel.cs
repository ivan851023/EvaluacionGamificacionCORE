using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EvaluacionGamificacionCORE.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Documento es obligatorio")]
        public string Documento { get; set; }
        [Required(ErrorMessage = "El campo Nombre Completo es obligatorio")]
        public string Nombre_Completo { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato es incorrecto")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Direccion es obligatorio")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "El campo Fecha Nacimiento es obligatorio")]
        public string Fecha_Nacimiento { get; set; }
        [Required(ErrorMessage = "El campo Telefono es obligatorio")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo Password es obligatorio")]
        [StringLength(100, ErrorMessage = "El password debe tener 8 caracteres", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "El password no coincide")]
        public string PasswordConfirm { get; set; }

        public DateTime FechaCreacion { get; set; }

    }
}
