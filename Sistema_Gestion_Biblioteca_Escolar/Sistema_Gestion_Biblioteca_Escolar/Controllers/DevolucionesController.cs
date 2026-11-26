using System.Linq;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    // Solo el personal con sesión iniciada puede gestionar usuarios.
    [Authorize]
    public class DevolucionesController : Controller
    {
        private readonly PrestamoService servicio = new PrestamoService();

        // GET: Devoluciones
        public ActionResult Index()
        {
            var lista = servicio.Pendientes().Select(PrestamoItemViewModel.Desde).ToList();
            return View(lista);
        }

        // POST: Devoluciones/MarcarDevuelto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MarcarDevuelto(int id)
        {
            string error;
            if (servicio.MarcarDevuelto(id, out error))
                TempData["Mensaje"] = "Devolución registrada correctamente.";
            else
                TempData["Error"] = error;

            return RedirectToAction("Index");
        }
    }
}