// Codigo guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using System.Collections.Generic;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public interface ILibroRepository // Interfaz para el repositorio de libros
    {
        IEnumerable<Libro> ObtenerActivos(); // Método para obtener todos los libros activos
        Libro ObtenerPorId(int id); // Método para obtener un libro por su ID
        bool ExisteIsbn(string isbn, int? excluirId = null); // Método para verificar si un ISBN ya existe, con opción de excluir un ID específico
        void Agregar(Libro libro); // Método para agregar un nuevo libro
        void Actualizar(Libro libro); // Método para actualizar un libro existente
        void Desactivar(int id); // Método para desactivar un libro por su ID
    }
}