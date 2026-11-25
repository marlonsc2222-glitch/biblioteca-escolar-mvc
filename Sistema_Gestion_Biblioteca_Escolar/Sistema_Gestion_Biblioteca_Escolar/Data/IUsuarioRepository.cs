using Sistema_Gestion_Biblioteca_Escolar.Models;
using System.Collections.Generic;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public interface IUsuarioRepository // Interfaz para el repositorio de usuarios
    {
        IEnumerable<UsuarioBiblioteca> ObtenerActivos(); // método para obtener 
        UsuarioBiblioteca ObtenerPorId(int id);
        bool ExisteCedula(string cedula, int? excluirId = null);
        void Agregar(UsuarioBiblioteca usuario);
        void Actualizar(UsuarioBiblioteca usuario);
        void Desactivar(int id);
        bool TienePrestamosPendientes(int id);
        bool ExisteCorreo(string correo, int? excluirId = null);
    }
}