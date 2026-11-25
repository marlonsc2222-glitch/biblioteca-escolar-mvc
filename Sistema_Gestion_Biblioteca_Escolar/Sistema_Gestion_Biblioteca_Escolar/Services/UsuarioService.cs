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

        public bool Crear(UsuarioBiblioteca usuario, out string error) // método para manejar la lógica de negocio de la creación de usuarios
        {
            error = null;
            try
            {
                usuario.Cedula = usuario.Cedula?.Trim(); // "?" valida que cédula no sea null, para ejecutar .Trim() y seguir el flujo.
                usuario.Correo = usuario.Correo?.Trim(); // Valida el correo de la misma forma al crear

                if (!TiposValidos.Contains(usuario.TipoUsuario)) // Valida el tipo de usuario 
                {
                    error = "El tipo de usuario debe ser Estudiante o Profesor.";
                    return false;
                }
                if (repo.ExisteCedula(usuario.Cedula)) // Valida si existe la cédula digitada
                {
                    error = "Ya existe un usuario con esa cédula.";
                    return false;
                }
                if (repo.ExisteCorreo(usuario.Correo))
                {
                    error = "Ya existe un usuario con ese correo.";
                    return false;
                }

                repo.Agregar(usuario); // Una vez validado se delega al método Agregar() del repositorio para agregar el usuario a la base de datos
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo guardar el usuario. Intente de nuevo.";
                return false;
            }
        }

        public bool Editar(UsuarioBiblioteca usuario, out string error) // Método para manejar la lógica de negocio de la edición de usuarios
        {
            error = null;
            try
            {
                usuario.Cedula = usuario.Cedula?.Trim(); // "?" valida que cédula no sea null, para ejecutar .Trim() y seguir el flujo.
                usuario.Correo = usuario.Correo?.Trim(); // Valida el correo de la misma forma al editar

                if (!TiposValidos.Contains(usuario.TipoUsuario)) // Valida el tipo de usuario 
                {
                    error = "El tipo de usuario debe ser Estudiante o Profesor.";
                    return false;
                }
                if (repo.ExisteCedula(usuario.Cedula, usuario.UsuarioId)) // Valida si existe la cédula digitada
                {
                    error = "Ya existe otro usuario con esa cédula.";
                    return false;
                }
                if (repo.ExisteCorreo(usuario.Correo, usuario.UsuarioId))
                {
                    error = "Ya existe otro usuario con ese correo.";
                    return false;
                }

                repo.Actualizar(usuario); // Una vez validado se delega al método Actualizar() del repositorio para actualizar el usuario en la base de datos
                return true;
            }
            catch (Exception)
            {
                error = "No se pudo actualizar el usuario. Intente de nuevo.";
                return false;
            }
        }

        public bool Eliminar(int id, out string error) // Método para manejar la lógicad de negocio de la eliminación (Eliminado Lógico) de usuarios.
        {
            error = null;
            try
            {
                if (repo.ObtenerPorId(id) == null) // se valida que el usuario exista
                {
                    error = "El usuario no existe.";
                    return false;
                }
                if (repo.TienePrestamosPendientes(id)) // No se puede dar de baja a quien aún tiene libros sin devolver.
                {
                    error = "No se puede eliminar: el usuario tiene préstamos pendientes.";
                    return false;
                }

                repo.Desactivar(id); // Si las validaciones son correctas, se desactiva el usuario
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