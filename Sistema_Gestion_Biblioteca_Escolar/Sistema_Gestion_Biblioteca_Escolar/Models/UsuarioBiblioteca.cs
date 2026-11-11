using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    public class UsuarioBiblioteca
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage ="EL nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "EL nombre no puede superar 150 caracteres")]
        public string Nombre   { get; set; }

        [Required(ErrorMessage ="La cedula es obligatoria")]
        [StringLength(25, ErrorMessage ="La cedula no puede superar 25 caracteres")]
        [Index(IsUnique =true)]
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage ="El correo es obligatorio")]
        [StringLength(150, ErrorMessage = "El correo no tiene un formato valido")]
        [EmailAddress(ErrorMessage ="El correo no tiene un formato valido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [StringLength(25)]
        [Display(Name = "Tipo de Usuario")]
        public string TipoUsuario { get; set; }

        public virtual ICollection<Prestamo> Prestamos { get; set; }

        public UsuarioBiblioteca()
        {
            Prestamos = new HashSet<Prestamo>();
        }
    }
}