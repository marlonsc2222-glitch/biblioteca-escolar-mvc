using System.Linq;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly PrestamoService servicio = new PrestamoService();

        // GET: Consultas/Historial?usuarioId=3
        public ActionResult Historial(int? usuarioId)
        {
            var vm = new HistorialViewModel
            {
                UsuarioId = usuarioId,
                Usuarios = servicio.UsuariosActivos()
                    .Select(u => new SelectListItem { Value = u.UsuarioId.ToString(), Text = u.Nombre })
                    .ToList()
            };

            if (usuarioId.HasValue)
                vm.Prestamos = servicio.HistorialDeUsuario(usuarioId.Value)
                    .Select(PrestamoItemViewModel.Desde).ToList();

            return View(vm);
        }
    }
}