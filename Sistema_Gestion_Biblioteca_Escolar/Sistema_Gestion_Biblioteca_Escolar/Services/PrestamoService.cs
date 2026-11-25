using System;
using System.Collections.Generic;
using Sistema_Gestion_Biblioteca_Escolar.Data;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Services
{
    public class PrestamoService // Lógica de negocio de préstamos, devoluciones e historial
    {
        private readonly IPrestamoRepository repo = new PrestamoRepository();
        private readonly IUsuarioRepository usuarios = new UsuarioRepository();
        private readonly ILibroRepository libros = new LibroRepository();

        public IEnumerable<UsuarioBiblioteca> UsuariosActivos() => usuarios.ObtenerActivos(); // Devuelve todos los usuarios activos
        public IEnumerable<Libro> LibrosActivos() => libros.ObtenerActivos();// Devuelve todos los libros activos
        public IEnumerable<Prestamo> Pendientes() => repo.ObtenerPendientes();// Devuelve todos los préstamos pendientes (sin fecha de devolución real)
        public IEnumerable<LibroRanking> LibrosMasPrestados(int cantidad = 10) => repo.ObtenerMasPrestados(cantidad);
        public IEnumerable<Prestamo> HistorialDeUsuario(int usuarioId) => repo.ObtenerPorUsuario(usuarioId);// Devuelve el historial completo de préstamos de un usuario específico

        public bool Registrar(Prestamo prestamo, out string error)// Registra un nuevo préstamo, verificando la existencia y disponibilidad del libro y del usuario, y devuelve un mensaje de error si algo falla
        {
            error = null;
            try
            {
                if (prestamo.FechaDevolucionEsperada.Date < prestamo.FechaPrestamo.Date)
                {
                    error = "La devolución esperada no puede ser anterior a la fecha de préstamo.";
                    return false;
                }
                return repo.Registrar(prestamo, out error);
            }
            catch (Exception)
            {
                error = "No se pudo registrar el préstamo. Intente de nuevo.";
                return false;
            }
        }

        public bool MarcarDevuelto(int prestamoId, out string error)// Marca un préstamo como devuelto, actualizando la fecha de devolución real y aumentando la cantidad disponible del libro, devolviendo un mensaje de error si algo falla
        {
            error = null;
            try
            {
                return repo.MarcarDevuelto(prestamoId, DateTime.Today, out error);
            }
            catch (Exception)
            {
                error = "No se pudo registrar la devolución. Intente de nuevo.";
                return false;
            }
        }
    }
}