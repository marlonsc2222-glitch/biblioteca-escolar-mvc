using Sistema_Gestion_Biblioteca_Escolar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public interface IPrestamoRepository
    {
        IEnumerable<Prestamo> ObtenerPendientes();
        IEnumerable<Prestamo> ObtenerPorUsuario(int usuarioId);
        bool Registrar(Prestamo prestamo, out string error);
        bool MarcarDevuelto(int prestamoId, DateTime fechaReal, out string error);
    }
}