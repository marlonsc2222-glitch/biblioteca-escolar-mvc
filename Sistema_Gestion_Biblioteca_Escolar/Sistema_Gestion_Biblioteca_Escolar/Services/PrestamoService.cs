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

        public IEnumerable<UsuarioBiblioteca> UsuariosActivos() => usuarios.ObtenerActivos();
        public IEnumerable<Libro> LibrosActivos() => libros.ObtenerActivos();
        public IEnumerable<Prestamo> Pendientes() => repo.ObtenerPendientes();
        public IEnumerable<Prestamo> HistorialDeUsuario(int usuarioId) => repo.ObtenerPorUsuario(usuarioId);

        public bool Registrar(Prestamo prestamo, out string error)
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

        public bool MarcarDevuelto(int prestamoId, out string error)
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