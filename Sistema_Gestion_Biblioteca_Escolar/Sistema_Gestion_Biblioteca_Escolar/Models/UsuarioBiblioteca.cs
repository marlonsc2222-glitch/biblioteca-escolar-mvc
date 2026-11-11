using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;          // Atributos de validación: [Key], [Required], [StringLength]...
using System.ComponentModel.DataAnnotations.Schema;   // Atributos de mapeo a la BD: [Index]
using System.Linq;
using System.Web;

namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    // Modelo (entidad) que representa a un usuario de la biblioteca.
    // Entity Framework lo convierte en la tabla "UsuarioBibliotecas".
    public class UsuarioBiblioteca
    {
        // Llave primaria de la tabla (se genera automáticamente)
        [Key]
        public int UsuarioId { get; set; }

        // Nombre completo: obligatorio y máximo 150 caracteres
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre no puede superar 150 caracteres")]
        public string Nombre { get; set; }

        // Cédula: obligatoria, máximo 25 caracteres y ÚNICA en la BD
        // (no pueden existir dos usuarios con la misma cédula)
        [Required(ErrorMessage = "La cedula es obligatoria")]
        [StringLength(25, ErrorMessage = "La cedula no puede superar 25 caracteres")]
        [Index(IsUnique = true)]
        [Display(Name = "Cedula")] // Texto que se muestra en las vistas
        public string Cedula { get; set; }

        // Correo: obligatorio, máximo 150 caracteres y con formato de email válido
        [Required(ErrorMessage = "El correo es obligatorio")]
        [StringLength(150, ErrorMessage = "El correo no tiene un formato valido")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato valido")]
        public string Correo { get; set; }

        // Tipo de usuario (por ejemplo: Estudiante, Docente), máximo 25 caracteres
        [Required(ErrorMessage = "El tipo de usuario es obligatorio")]
        [StringLength(25)]
        [Display(Name = "Tipo de Usuario")]
        public string TipoUsuario { get; set; }

        // Propiedad de navegación: un usuario puede tener MUCHOS préstamos (relación 1 a N).
        // "virtual" permite a Entity Framework cargar los datos relacionados cuando se necesitan (lazy loading)
        public virtual ICollection<Prestamo> Prestamos { get; set; }

        // Constructor: inicializa la colección vacía para evitar errores de
        // referencia nula al agregar préstamos
        public UsuarioBiblioteca()
        {
            Prestamos = new HashSet<Prestamo>();
        }
    }
}