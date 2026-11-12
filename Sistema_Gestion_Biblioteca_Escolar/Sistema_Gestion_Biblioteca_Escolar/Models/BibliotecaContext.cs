// Código guíado y consultado mediante IA (Claude Anthropic) ------------------------------------------------------
using System.Data.Entity;

namespace Sistema_Gestion_Biblioteca_Escolar.Models
{
    public class BibliotecaContext : DbContext // se hereda de DbContext para manejar la conexión a la base de datos y las entidades
    {
        // "BibliotecaContext" debe coincidir con el name de la cadena en Web.config
        public BibliotecaContext() : base("name=BibliotecaContext")
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<UsuarioBiblioteca> Usuarios { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<CuentaPersonal> Cuentas { get; set; }
    }
}