using System;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class PrestamoItemViewModel // ViewModel para mostrar la información de un préstamo específico, incluyendo el estado del préstamo (Activo o Devuelto)
    {
        public int PrestamoId { get; set; }
        public string Usuario { get; set; }
        public string Libro { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionEsperada { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public string Estado => FechaDevolucionReal == null ? "Activo" : "Devuelto";

        public static PrestamoItemViewModel Desde(Prestamo p)// Método estático que convierte un objeto Prestamo en un PrestamoItemViewModel, mapeando las propiedades relevantes
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