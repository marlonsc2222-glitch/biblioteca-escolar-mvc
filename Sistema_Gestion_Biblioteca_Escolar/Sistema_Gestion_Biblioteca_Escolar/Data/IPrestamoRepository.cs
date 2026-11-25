using Sistema_Gestion_Biblioteca_Escolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public interface IPrestamoRepository  // Interfaz para el repositorio de préstamos, define los métodos que deben implementarse
    {
        IEnumerable<Prestamo> ObtenerPendientes();// Obtiene todos los préstamos que aún no han sido devueltos (sin fecha de devolución real)
        IEnumerable<Prestamo> ObtenerPorUsuario(int usuarioId);// Obtiene el historial completo de préstamos de un usuario específico, ordenados por fecha de préstamo descendente
        IEnumerable<LibroRanking> ObtenerMasPrestados(int cantidad);
        bool Registrar(Prestamo prestamo, out string error);    // Registra un nuevo préstamo, verificando la existencia y disponibilidad del libro y del usuario, y devuelve un mensaje de error si algo falla
        bool MarcarDevuelto(int prestamoId, DateTime fechaReal, out string error);// Marca un préstamo como devuelto, actualizando la fecha de devolución real y aumentando la cantidad disponible del libro, devolviendo un mensaje de error si algo falla
    }
}