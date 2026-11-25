using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class PrestamoViewModel // Formulario de registrar préstamo
    {
        [Required(ErrorMessage = "Seleccione un usuario")]
        [Display(Name = "Usuario")]
        public int? UsuarioId { get; set; }

        [Required(ErrorMessage = "Seleccione un libro")]
        [Display(Name = "Libro")]
        public int? LibroId { get; set; }

        [Required(ErrorMessage = "La fecha de préstamo es obligatoria")]
        [Display(Name = "Fecha de préstamo")]
        public DateTime? FechaPrestamo { get; set; }

        [Required(ErrorMessage = "La devolución esperada es obligatoria")]
        [Display(Name = "Devolución esperada")]
        public DateTime? FechaDevolucionEsperada { get; set; }

        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<SelectListItem> Libros { get; set; }
    }
}