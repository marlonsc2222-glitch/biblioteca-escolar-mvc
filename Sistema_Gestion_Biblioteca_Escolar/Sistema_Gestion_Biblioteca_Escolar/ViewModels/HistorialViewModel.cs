using System.Collections.Generic;
using System.Web.Mvc;

namespace Sistema_Gestion_Biblioteca_Escolar.ViewModels
{
    public class HistorialViewModel
    {
        public int? UsuarioId { get; set; }
        public IEnumerable<SelectListItem> Usuarios { get; set; }
        public IEnumerable<PrestamoItemViewModel> Prestamos { get; set; } = new List<PrestamoItemViewModel>();
    }
}