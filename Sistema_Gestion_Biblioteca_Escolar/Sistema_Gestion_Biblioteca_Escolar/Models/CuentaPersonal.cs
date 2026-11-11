using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Código guíado y consultado mediante IA (Claude Anthropic) ------------------------------------------------------
namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    public class CuentaPersonal // Clase que representa la entidad CuentaPersonal en el sistema de gestión de biblioteca escolar    
    {
        [Key] // Indica que la propiedad CuentaPersonalId es la clave primaria de la entidad
        public int CuentaPersonalId { get; set; } // Identificador único de la cuenta personal

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres")]
        [Index(IsUnique = true)]
        [Display(Name = "Nombre de Usuario")]
        public string NombreUsuario { get; set; } // Nombre de usuario único para la cuenta personal

        // Nunca se debe almacenar contraseñas en texto plano. En un sistema real, se debería almacenar un hash de la contraseña.
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres")]
        public string ContraseñaHash { get; set; } // Hash de la contraseña para la cuenta personal
    }
}