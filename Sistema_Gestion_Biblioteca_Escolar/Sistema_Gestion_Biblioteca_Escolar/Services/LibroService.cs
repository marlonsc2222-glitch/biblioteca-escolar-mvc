using System;
using System.Collections.Generic;
using Sistema_Gestion_Biblioteca_Escolar.Data;
using Sistema_Gestion_Biblioteca_Escolar.Models;

// Codigo guíado y sugerido por IA (Claude, Anthropic) -----------------------------------

namespace Sistema_Gestion_Biblioteca_Escolar.Services
{
    public class LibroService // Servicio para manejar la lógica de negocio relacionada con los libros
    {
        private readonly ILibroRepository repo = new LibroRepository(); // Repositorio de libros para interactuar con la base de datos (readyonly debido a que no se debe modificar)

        public IEnumerable<Libro> Listar() => repo.ObtenerActivos(); // Método para listar todos los libros activos, delegando la llamada al repositorio

        public Libro Obtener(int id) => repo.ObtenerPorId(id); // Método para obtener un libro por su ID, delegando la llamada al repositorio

        public bool Crear(Libro libro, out string error) // Método para crear un nuevo libro, verificando si el ISBN ya existe y manejando errores
        {
            error = null; // Inicializa la variable de error como null (si no hay errores, se mantiene null y se retorna true)
            try
            {
                if (repo.ExisteIsbn(libro.ISBN)) // Verifica si el ISBN del libro ya existe en la base de datos (sin excluir ningún ID, ya que es un libro nuevo)
                {
                    error = "Ya existe un libro con ese ISBN.";
                    return false;
                }
                repo.Agregar(libro);
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo guardar el libro. Intente de nuevo.";
                return false;
            }
        }

        public bool Editar(Libro libro, out string error) // Método para editar un libro existente, verificando si el ISBN ya existe (excluyendo el ID del libro actual) y manejando errores
        {
            error = null;
            try
            {
                if (repo.ExisteIsbn(libro.ISBN, libro.LibroId))
                {
                    error = "Ya existe otro libro con ese ISBN.";
                    return false;
                }
                repo.Actualizar(libro);
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo actualizar el libro. Intente de nuevo.";
                return false;
            }
        }

        public bool Eliminar(int id, out string error) // Método para eliminar un libro por su ID, manejando errores
        {
            error = null;
            try
            {
                repo.Desactivar(id);
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo eliminar el libro. Intente de nuevo.";
                return false;
            }
        }
    }
}