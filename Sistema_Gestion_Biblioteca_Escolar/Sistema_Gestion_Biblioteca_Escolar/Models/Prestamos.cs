using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

// Código guíado y consultado mediante IA (Claude Anthropic) ------------------------------------------------------
namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    public class Prestamos // Clase que representa la entidad Prestamos en el sistema de gestión de biblioteca escolar
    {
        [Key]
        public int PrestamoId { get; set; } // Identificador único del préstamo

        [Required]
        public int UsuarioId { get; set; } // Identificador del usuario que realiza el préstamo

        [Required]
        public int LibroId { get; set; } // Identificador del libro que se presta

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de préstamo")]
        public DateTime FechaPrestamo { get; set; } // Fecha en que se realiza el préstamo

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Devolución esperada")]
        public DateTime FechaDevolucionEsperada { get; set; } // Fecha en que se espera que el libro sea devuelto

        // NULL mientras el préstamo siga pendiente
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Devolución real")]
        public DateTime? FechaDevolucionReal { get; set; } // Fecha en que se devuelve el libro (puede ser nula si el libro aún no ha sido devuelto)

        [ForeignKey("UsuarioId")] // Indica que la propiedad UsuarioId es una clave foránea que hace referencia a la entidad UsuarioBiblioteca
        public virtual UsuarioBiblioteca Usuario { get; set; }

        [ForeignKey("LibroId")]
        public virtual Libro Libro { get; set; } // Relación con la entidad Libro
    }
}