using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    // Código guíado y consultado mediante IA (Claude Anthropic) ------------------------------------------------------
    public class Libro // Clase que representa la entidad Libro en el sistema de gestión de biblioteca escolar
    {
        [Key]
        public int LibroId { get; set; } // Identificador único del libro

        [Required(ErrorMessage = "El título del libro es obligatorio")]
        [StringLength(200, ErrorMessage = "El título del libro no puede exceder los 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } // Título del libro

        [Required(ErrorMessage = "El autor del libro es obligatorio")]
        [StringLength(150, ErrorMessage = "El autor del libro no puede exceder los 150 caracteres")]
        public string Autor { get; set; } // Autor del libro

        [Required(ErrorMessage = "El ISBN del libro es obligatorio")]
        [StringLength(13, ErrorMessage = "El ISBN del libro no puede exceder los 13 caracteres")]
        [Index(IsUnique = true)]
        public string ISBN { get; set; } // ISBN del libro

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [StringLength(80, ErrorMessage = "La categoría del libro no puede exceder los 80 caracteres")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; } // Categoría del libro

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad de copias disponibles no puede ser negativa")]
        [Display(Name = "Copias Disponibles")]
        public int CopiasDisponibles { get; set; } // Cantidad de copias disponibles del libro

        public bool Activo { get; set; } // Indica si el libro está activo

        public virtual ICollection<Prestamo> Prestamos { get; set; } // Relación con la entidad Prestamo

        public Libro()
        {
            Prestamos = new HashSet<Prestamo>(); // Inicializa la colección de préstamos
            Activo = true; // Por defecto, el libro está activo
        }
    }
}