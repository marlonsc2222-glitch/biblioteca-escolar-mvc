using System;
using System.Linq;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Models;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    public class PrestamosController : Controller // Controlador para manejar las operaciones relacionadas con los préstamos de libros, incluyendo la creación de nuevos préstamos y la visualización del historial de préstamos
    {
        private readonly PrestamoService servicio = new PrestamoService();

        public ActionResult Index() => RedirectToAction("Create");

        // GET: Prestamos/Create
        public ActionResult Create()
        {
            var vm = new PrestamoViewModel
            {
                FechaPrestamo = DateTime.Today,
                FechaDevolucionEsperada = DateTime.Today.AddDays(14)
            };
            CargarListas(vm);
            return View(vm);
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrestamoViewModel modelo)// Maneja la solicitud POST para crear un nuevo préstamo, validando el modelo y registrando el préstamo a través del servicio, devolviendo la vista con errores si algo falla
        {
            if (ModelState.IsValid)
            {
                var prestamo = new Prestamo
                {
                    UsuarioId = modelo.UsuarioId.Value,
                    LibroId = modelo.LibroId.Value,
                    FechaPrestamo = modelo.FechaPrestamo.Value,
                    FechaDevolucionEsperada = modelo.FechaDevolucionEsperada.Value
                };

                string error;
                if (servicio.Registrar(prestamo, out error))
                {
                    TempData["Mensaje"] = "Préstamo registrado correctamente.";
                    return RedirectToAction("Index", "Devoluciones");
                }
                ModelState.AddModelError("", error);
            }

            CargarListas(modelo); // Hay que volver a llenar los dropdowns al devolver la vista
            return View(modelo);
        }

        private void CargarListas(PrestamoViewModel vm)
        {
            vm.Usuarios = servicio.UsuariosActivos()
                .Select(u => new SelectListItem { Value = u.UsuarioId.ToString(), Text = u.Nombre + " (" + u.TipoUsuario + ")" })
                .ToList();
            vm.Libros = servicio.LibrosActivos()
                .Select(l => new SelectListItem { Value = l.LibroId.ToString(), Text = l.Titulo })
                .ToList();
        }
    }
}