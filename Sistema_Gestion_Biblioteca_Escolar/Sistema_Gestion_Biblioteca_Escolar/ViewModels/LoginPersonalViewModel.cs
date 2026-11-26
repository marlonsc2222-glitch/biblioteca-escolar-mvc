using System.ComponentModel.DataAnnotations;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    // Validaciones del formulario sugeridas por ChatGPT y adaptadas
    // al nombre de usuario y al límite de 50 caracteres de CuentaPersonal.
    public class LoginPersonalViewModel
    {
        [Required(ErrorMessage = "Ingresá el nombre de usuario.")]
        [StringLength(50, ErrorMessage = "El nombre de usuario admite hasta 50 caracteres.")]
        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "Ingresá la contraseña.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }
    }
}