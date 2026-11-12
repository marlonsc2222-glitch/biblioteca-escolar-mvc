// Codigo guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using System.ComponentModel.DataAnnotations;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class LibroViewModel // ViewModel para representar los datos de un libro en la vista, incluyendo validaciones y etiquetas de presentación
    {
        [Key]
        public int LibroId { get; set; } // Propiedad para almacenar el ID del libro (clave primaria en la base de datos)

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede superar 200 caracteres")]
        [Display(Name = "Título")]
        public string Titulo { get; set; } // Propiedad para almacenar el título del libro, con validación de requerimiento y longitud máxima, y etiqueta de presentación personalizada

        [Required(ErrorMessage = "El autor es obligatorio")]
        [StringLength(150, ErrorMessage = "El autor no puede superar 150 caracteres")]
        [Display(Name = "Autor")]
        public string Autor { get; set; } // Propiedad para almacenar el autor del libro, con validación de requerimiento y longitud máxima, y etiqueta de presentación personalizada

        [Required(ErrorMessage = "El ISBN es obligatorio")]
        [StringLength(13, ErrorMessage = "El ISBN no puede superar 13 caracteres")]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "El ISBN debe ser un número de 10 o 13 dígitos")]
        public string ISBN { get; set; } // Propiedad para almacenar el ISBN del libro, con validación de requerimiento y longitud máxima

        [Required(ErrorMessage = "La categoría es obligatoria")]
        [StringLength(80, ErrorMessage = "La categoría no puede superar 80 caracteres")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "La categoría solo puede contener letras y espacios")]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; } // Propiedad para almacenar la categoría del libro, con validación de requerimiento y longitud máxima, y etiqueta de presentación personalizada

        [Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa")]
        [Display(Name = "Copias disponibles")]
        public int CantidadDisponible { get; set; } // Propiedad para almacenar la cantidad disponible del libro, con validación de rango para asegurar que no sea negativa, y etiqueta de presentación personalizada
    }
}