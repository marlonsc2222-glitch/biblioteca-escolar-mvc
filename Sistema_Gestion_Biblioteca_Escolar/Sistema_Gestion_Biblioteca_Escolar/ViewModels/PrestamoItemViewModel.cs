using System;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class PrestamoItemViewModel // Fila de tabla: la usan Devoluciones e Historial
    {
        public int PrestamoId { get; set; }
        public string Usuario { get; set; }
        public string Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionEsperada { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string Estado => FechaDevolucionReal == null ? "Activo" : "Devuelto";

        public static PrestamoItemViewModel Desde(Prestamo p)
        {
            return new PrestamoItemViewModel
            {
                PrestamoId = p.PrestamoId,
                Usuario = p.Usuario.Nombre,
                Libro = p.Libro.Titulo,
                FechaPrestamo = p.FechaPrestamo,
                FechaDevolucionEsperada = p.FechaDevolucionEsperada,
                FechaDevolucionReal = p.FechaDevolucionReal
            };
        }
    }
}