// Código guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using System.Linq;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    // Solo el personal con sesión iniciada puede gestionar usuarios.
    [Authorize]
    public class ConsultasController : Controller
    {
        private readonly PrestamoService servicio = new PrestamoService();

        // GET: Consultas/Historial?usuarioId=3
        public ActionResult Historial(int? usuarioId) // Historial de préstamos del usuario elegido en el dropdown
        {
            var vm = new HistorialViewModel
            {
                UsuarioId = usuarioId,
                Usuarios = servicio.UsuariosActivos()
                    .Select(u => new SelectListItem { Value = u.UsuarioId.ToString(), Text = u.Nombre })
                    .ToList()
            };

            if (usuarioId.HasValue) // Solo busca préstamos si ya se eligió un usuario
                vm.Prestamos = servicio.HistorialDeUsuario(usuarioId.Value)
                    .Select(PrestamoItemViewModel.Desde).ToList();

            return View(vm);
        }

        // GET: Consultas/MasPrestados
        public ActionResult MasPrestados() // Ranking de los 10 libros más prestados
        {
            var ranking = servicio.LibrosMasPrestados(10).ToList();
            return View(ranking);
        }
    }
}