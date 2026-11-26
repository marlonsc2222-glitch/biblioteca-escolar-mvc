using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Models;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    // Solo el personal con sesión iniciada puede gestionar usuarios.
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly UsuarioService servicio = new UsuarioService();

        private void CargarTiposUsuario(string seleccionado = null)
        {
            var tipos = new List<SelectListItem>
            {
                new SelectListItem { Text = "Estudiante", Value = "Estudiante" },
                new SelectListItem { Text = "Profesor", Value = "Profesor" }
            };

            ViewBag.TiposUsuario = new SelectList(tipos, "Value", "Text", seleccionado);
        }

        // GET: Usuarios
        public ActionResult Index() // Acción para listar todos los usuarios, obteniendo los datos del servicio y convirtiéndolos a ViewModel para la vista
        {
            var usuarios = servicio.Listar().Select(ConvertirAViewModel).ToList();
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create() // Acción para mostrar el formulario de creación de un nuevo usuario, pasando un ViewModel vacío a la vista
        {
            CargarTiposUsuario();
            return View(new UsuarioViewModel());
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel modelo) // Acción para procesar el formulario de creación de un nuevo usuario, validando el modelo y llamando al servicio
        {
            if (!ModelState.IsValid)
            {
                CargarTiposUsuario(modelo.TipoUsuario);
                return View(modelo);
            }

            string error;
            if (servicio.Crear(ConvertirAEntidad(modelo), out error))
            {
                TempData["Mensaje"] = "Usuario registrado correctamente.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", error);
            CargarTiposUsuario(modelo.TipoUsuario);
            return View(modelo);
        }

        // GET: Usuarios/Edit/
        public ActionResult Edit(int? id) // Acción para mostrar el formulario de edición de un usuario existente, obteniendo el usuario por su ID y convirtiéndolo a ViewModel
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = servicio.Obtener(id.Value);
            if (usuario == null) return HttpNotFound();

            CargarTiposUsuario(usuario.TipoUsuario);
            return View(ConvertirAViewModel(usuario));
        }

        // POST: Usuarios/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsuarioViewModel modelo) // Acción para procesar el formulario de edición de un usuario existente, validando el modelo y llamando al servicio
        {
            if (!ModelState.IsValid)
            {
                CargarTiposUsuario(modelo.TipoUsuario);
                return View(modelo);
            }

            string error;
            if (servicio.Editar(ConvertirAEntidad(modelo), out error))
            {
                TempData["Mensaje"] = "Usuario actualizado correctamente.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", error);
            CargarTiposUsuario(modelo.TipoUsuario);
            return View(modelo);
        }

        // GET: Usuarios/Delete/
        public ActionResult Delete(int? id) // Acción para mostrar la confirmación de eliminación de un usuario existente, obteniendo el usuario por su ID y convirtiéndolo a ViewModel
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var usuario = servicio.Obtener(id.Value);
            if (usuario == null) return HttpNotFound();

            return View(ConvertirAViewModel(usuario));
        }

        // POST: Usuarios/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) // Acción para procesar la eliminación de un usuario existente, llamando al servicio y manejando errores
        {
            string error;
            if (servicio.Eliminar(id, out error))
                TempData["Mensaje"] = "Usuario eliminado correctamente.";
            else
                TempData["Error"] = error;

            return RedirectToAction("Index");
        }

        // Conversión entre entidad y ViewModel (la vista nunca recibe la entidad directa)
        private static UsuarioViewModel ConvertirAViewModel(UsuarioBiblioteca u) // Método para convertir un objeto de tipo UsuarioBiblioteca (entidad) a UsuarioViewModel (para la vista)
        {
            return new UsuarioViewModel
            {
                UsuarioId = u.UsuarioId,
                Nombre = u.Nombre,
                Cedula = u.Cedula,
                Correo = u.Correo,
                TipoUsuario = u.TipoUsuario
            };
        }

        private static UsuarioBiblioteca ConvertirAEntidad(UsuarioViewModel m) // Método para convertir un objeto de tipo UsuarioViewModel (de la vista) a UsuarioBiblioteca (entidad)
        {
            return new UsuarioBiblioteca
            {
                UsuarioId = m.UsuarioId,
                Nombre = m.Nombre,
                Cedula = m.Cedula,
                Correo = m.Correo,
                TipoUsuario = m.TipoUsuario
            };
        }
    }
}