using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly BibliotecaContext db = new BibliotecaContext();

        public IEnumerable<Prestamo> ObtenerPendientes() // Préstamos sin fecha de devolución real
        {
            return db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Where(p => p.FechaDevolucionReal == null)
                .OrderBy(p => p.FechaDevolucionEsperada)
                .ToList();
        }

        public IEnumerable<Prestamo> ObtenerPorUsuario(int usuarioId) // Historial completo de un usuario
        {
            return db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Where(p => p.UsuarioId == usuarioId)
                .OrderByDescending(p => p.FechaPrestamo)
                .ToList();
        }

        public bool Registrar(Prestamo prestamo, out string error) // Guarda el préstamo y resta 1 copia en UN solo SaveChanges
        {
            error = null;

            var usuario = db.Usuarios.FirstOrDefault(u => u.UsuarioId == prestamo.UsuarioId && u.Activo);
            if (usuario == null)
            {
                error = "El usuario seleccionado no existe o está inactivo.";
                return false;
            }

            var libro = db.Libros.FirstOrDefault(l => l.LibroId == prestamo.LibroId && l.Activo);
            if (libro == null)
            {
                error = "El libro seleccionado no existe o está inactivo.";
                return false;
            }

            if (libro.CantidadDisponible <= 0)
            {
                error = "No hay copias disponibles de este libro.";
                return false;
            }

            libro.CantidadDisponible -= 1;
            prestamo.FechaDevolucionReal = null;
            db.Prestamos.Add(prestamo);
            db.SaveChanges(); // Ambos cambios se guardan juntos (o ninguno)
            return true;
        }

        public bool MarcarDevuelto(int prestamoId, DateTime fechaReal, out string error) // Marca devuelto y suma 1 copia en UN solo SaveChanges
        {
            error = null;

            var prestamo = db.Prestamos.Include(p => p.Libro).FirstOrDefault(p => p.PrestamoId == prestamoId);
            if (prestamo == null)
            {
                error = "El préstamo no existe.";
                return false;
            }

            if (prestamo.FechaDevolucionReal != null)
            {
                error = "Este préstamo ya fue devuelto.";
                return false;
            }

            prestamo.FechaDevolucionReal = fechaReal.Date;
            prestamo.Libro.CantidadDisponible += 1;
            db.SaveChanges();
            return true;
        }
    }
}