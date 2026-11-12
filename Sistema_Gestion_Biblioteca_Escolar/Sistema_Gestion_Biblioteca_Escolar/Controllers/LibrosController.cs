// Codigo Guiado y sugerido por IA (Claude, Anthropic) -----------------------------------
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.Models;
using Sistema_Gestion_Biblioteca_Escolar.Services;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibroService servicio = new LibroService();

        // GET: Libros
        public ActionResult Index() // Acción para listar todos los libros activos, obteniendo los datos del servicio y convirtiéndolos a ViewModel para la vista
        {
            var libros = servicio.Listar().Select(ConvertirAViewModel).ToList();
            return View(libros);
        }

        // GET: Libros/Create
        public ActionResult Create() // Acción para mostrar el formulario de creación de un nuevo libro, pasando un ViewModel vacío a la vista
        {
            return View(new LibroViewModel());
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibroViewModel modelo) // Acción para procesar el formulario de creación de un nuevo libro, validando el modelo y llamando al servicio para crear el libro
        {
            if (!ModelState.IsValid) return View(modelo);

            string error;
            if (servicio.Crear(ConvertirAEntidad(modelo), out error))
            {
                TempData["Mensaje"] = "Libro registrado correctamente.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", error);
            return View(modelo);
        }

        // GET: Libros/Edit/5
        public ActionResult Edit(int? id) // Acción para mostrar el formulario de edición de un libro existente, obteniendo el libro por su ID y convirtiéndolo a ViewModel para la vista
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var libro = servicio.Obtener(id.Value);
            if (libro == null) return HttpNotFound();

            return View(ConvertirAViewModel(libro));
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LibroViewModel modelo) // Acción para procesar el formulario de edición de un libro existente, validando el modelo y llamando al servicio para actualizar el libro
        {
            if (!ModelState.IsValid) return View(modelo);

            string error;
            if (servicio.Editar(ConvertirAEntidad(modelo), out error))
            {
                TempData["Mensaje"] = "Libro actualizado correctamente.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", error);
            return View(modelo);
        }

        // GET: Libros/Delete/5
        public ActionResult Delete(int? id) // Acción para mostrar la confirmación de eliminación de un libro existente, obteniendo el libro por su ID y convirtiéndolo a ViewModel para la vista
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var libro = servicio.Obtener(id.Value);
            if (libro == null) return HttpNotFound();

            return View(ConvertirAViewModel(libro));
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) // Acción para procesar la eliminación de un libro existente, llamando al servicio para desactivar el libro y manejando errores
        {
            string error;
            if (servicio.Eliminar(id, out error))
                TempData["Mensaje"] = "Libro eliminado correctamente.";
            else
                TempData["Error"] = error;

            return RedirectToAction("Index");
        }

        // Conversión entre entidad y ViewModel (la vista nunca recibe la entidad directa)
        private static LibroViewModel ConvertirAViewModel(Libro l) // Método para convertir un objeto de tipo Libro (entidad) a un objeto de tipo LibroViewModel (para la vista), mapeando las propiedades correspondientes
        {
            return new LibroViewModel
            {
                LibroId = l.LibroId,
                Titulo = l.Titulo,
                Autor = l.Autor,
                ISBN = l.ISBN,
                Categoria = l.Categoria,
                CantidadDisponible = l.CantidadDisponible
            };
        }

        private static Libro ConvertirAEntidad(LibroViewModel m) // Método para convertir un objeto de tipo LibroViewModel (de la vista) a un objeto de tipo Libro (entidad), mapeando las propiedades correspondientes y dejando Activo en true por el constructor de Libro
        {
            return new Libro
            {
                LibroId = m.LibroId,
                Titulo = m.Titulo,
                Autor = m.Autor,
                ISBN = m.ISBN,
                Categoria = m.Categoria,
                CantidadDisponible = m.CantidadDisponible
                // Activo queda en true por el constructor de Libro
            };
        }
    }
}