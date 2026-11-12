// Codigo guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public class LibroRepository : ILibroRepository // Implementación del repositorio de libros (Hereda de ILibroRepository debido a que implementa todos sus métodos)
    {
        private readonly BibliotecaContext db = new BibliotecaContext(); // Contexto de la base de datos para interactuar con la tabla de libros (readyonly debido a que no se debe modificar)

        public IEnumerable<Libro> ObtenerActivos() // Método para obtener todos los libros activos, ordenados por título
        {
            return db.Libros.Where(l => l.Activo).OrderBy(l => l.Titulo).ToList();
        }

        public Libro ObtenerPorId(int id) // Método para obtener un libro por su ID, solo si está activo
        {
            return db.Libros.FirstOrDefault(l => l.LibroId == id && l.Activo);
        }

        public bool ExisteIsbn(string isbn, int? excluirId = null) // Método para verificar si un ISBN ya existe, con opción de excluir un ID específico (útil al actualizar un libro)
        {
            return db.Libros.Any(l => l.ISBN == isbn && l.LibroId != excluirId);
        }

        public void Agregar(Libro libro) // Método para agregar un nuevo libro a la base de datos
        {
            db.Libros.Add(libro);
            db.SaveChanges();
        }

        public void Actualizar(Libro libro) // Método para actualizar un libro existente en la base de datos
        {
            db.Entry(libro).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Desactivar(int id) // Método para desactivar un libro por su ID (borrado lógico, no se elimina físicamente de la base de datos)
        {
            var libro = db.Libros.Find(id);
            if (libro != null)
            {
                libro.Activo = false;
                db.SaveChanges();
            }
        }
    }
}