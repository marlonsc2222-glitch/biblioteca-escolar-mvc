using System;
using System.Collections.Generic;
using System.Linq;
using Sistema_Gestion_Biblioteca_Escolar.Data;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Services
{
    public class UsuarioService // servicio con la lógica de negocio de los usauarios de la bilblioteca 
    {
        private readonly IUsuarioRepository repo = new UsuarioRepository(); // se usa la interfaz, el servicio no sabe cómo se guardan los datos, solo aplica lógica de negocio

        private static readonly string[] TiposValidos = { "Estudiante", "Profesor" }; // Arreglo con los tipos de Usuarios permitidos

        public IEnumerable<UsuarioBiblioteca> Listar() => repo.ObtenerActivos(); // se delega a la interfáz (IUsuarioRepository) para obtener los usuarios activos

        public UsuarioBiblioteca Obtener(int id) => repo.ObtenerPorId(id);
        
        public bool Crear(UsuarioBiblioteca usuario, out string error)
        {
            error = null; 
            try
            {
                usuario.Cedula = usuario.Cedula?.Trim();

                if (!TiposValidos.Contains(usuario.TipoUsuario))
                {
                    error = "El tipo de usuario debe ser Estudiante o Profesor.";
                    return false;
                }
                if (repo.ExisteCedula(usuario.Cedula, usuario.UsuarioId)) // Se excluye al propio usuario
                {
                    error = "Ya existe otro usuario con esa cédula.";
                    return false;
                }

                repo.Actualizar(usuario);
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo actualizar el usuario. Intente de nuevo.";
                return false;
            }
        }

        public bool Eliminar(int id, out string error)
        {
            error = null;
            try
            {
                if (repo.ObtenerPorId(id) == null)
                {
                    error = "El usuario no existe.";
                    return false;
                }
                if (repo.TienePrestamosPendientes(id))
                {
                    error = "No se puede eliminar: el usuario tiene préstamos pendientes.";
                    return false;
                }

                repo.Desactivar(id);
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo eliminar el usuario. Intente de nuevo.";
                return false;
            }
        }
    }
}