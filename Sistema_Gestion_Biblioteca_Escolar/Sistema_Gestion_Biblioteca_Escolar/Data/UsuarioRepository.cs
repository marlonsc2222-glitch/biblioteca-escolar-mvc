// Codigo guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using Sistema_Gestion_Biblioteca_Escolar.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BibliotecaContext db = new BibliotecaContext(); // Contexto de la base de datos para interactuar con la tabla de usuarios (readonly debido a que no se debe modificar)

        public IEnumerable<UsuarioBiblioteca> ObtenerActivos() // Método para obtener todos los Usuarios Activos
        {
            return db.Usuarios.Where(l => l.Activo).OrderBy(l => l.Nombre).ToList();
        }

        public UsuarioBiblioteca ObtenerPorId(int id) // Método para obtener un Usuario por su ID solo si está activo
        {
            return db.Usuarios.FirstOrDefault(l => l.UsuarioId == id && l.Activo);
        }

        public bool ExisteCedula(string cedula, int? excluirId = null) // Método para verificar si un Cedula ya existe / Si se pasa "excluirId" (al editar), se ignora el registro del mismo usuario para evitar que se duplique
        {
            return db.Usuarios.Any(l => l.Cedula == cedula && l.UsuarioId != excluirId);
        }
        public bool ExisteCorreo(string correo, int? excluirId = null)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            string correoNormalizado = correo.Trim().ToLower();

            return db.Usuarios.Any(u => u.Activo &&  u.Correo != null && u.Correo.Trim().ToLower() == correoNormalizado && (!excluirId.HasValue || u.UsuarioId != excluirId.Value));
        }

        public void Agregar(UsuarioBiblioteca usuario) // Método para agregar un Usuario nuevo a la base de datos.
        {
            usuario.Activo = true;
            db.Usuarios.Add(usuario);
            db.SaveChanges();
        }

        public void Actualizar(UsuarioBiblioteca usuario) // Método para actualizar un usuario ya existente en la base de datos
        {
            var existente = db.Usuarios.Find(usuario.UsuarioId);
            if (existente == null)
            {
                return;
            }

            existente.Nombre = usuario.Nombre;
            existente.Cedula = usuario.Cedula;
            existente.Correo = usuario.Correo;
            existente.TipoUsuario = usuario.TipoUsuario;

            db.Entry(existente).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Desactivar(int id) // Método para desactivar un Usuario por su ID (borrado lógico para no perder el registro en la bvase de datos)
        {
            var usuario = db.Usuarios.Find(id);
            if(usuario == null)
            { 
                return; 
            }

            usuario.Activo = false;
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool TienePrestamosPendientes(int id) // Un préstamo pendiente es el que aún no tiene fecha de devolución real
        {
            return db.Prestamos.Any(p => p.UsuarioId == id && p.FechaDevolucionReal == null);
        }
    }
}