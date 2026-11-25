using System.ComponentModel.DataAnnotations;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class UsuarioViewModel // ViewModel para representar los datos de un Usuario en la vista, incluyendo validaciones y etiquetas de presentación (Display)
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar 150 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La cedula es obligatoria")]
        [StringLength(25, ErrorMessage = "La cedula no puede superar 25 caracteres")]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(150, ErrorMessage = "El correo no puede superar los 150 caracteres")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [StringLength(25)]
        [Display(Name = "Tipo de Usuario")]
        public string TipoUsuario { get; set; }
    }
}