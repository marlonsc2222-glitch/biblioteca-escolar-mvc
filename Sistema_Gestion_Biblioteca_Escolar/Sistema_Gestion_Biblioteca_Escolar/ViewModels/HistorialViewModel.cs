using System.Collections.Generic;
using System.Web.Mvc;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class HistorialViewModel // ViewModel para mostrar el historial de préstamos de un usuario específico, incluyendo la lista de usuarios activos y los préstamos asociados a ese usuario
    {
        public int? UsuarioId { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<PrestamoItemViewModel> Prestamos { get; set; } = new List<PrestamoItemViewModel>();
    }
}