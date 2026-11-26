using System.Web.Mvc;
using Sistema_Gestion_Biblioteca_Escolar.ViewModels;
using System.Web.Security;
using Sistema_Gestion_Biblioteca_Escolar.Data;
using Sistema_Gestion_Biblioteca_Escolar.Services;

namespace Sistema_Gestion_Biblioteca_Escolar.Controllers
{
    public class CuentaController : Controller
    {
        // Acción GET sugerida por ChatGPT y adaptada al ViewModel del proyecto.
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View(new LoginPersonalViewModel());
        }

        // Acción POST sugerida por ChatGPT y adaptada para validar el formulario
        // del personal antes de conectar la verificación de credenciales.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginPersonalViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var repositorio = new CuentaPersonalRepository();
            var cuenta = repositorio.BuscarPorNombreUsuario(modelo.NombreUsuario.Trim());

            // Usa el mismo mensaje si la cuenta no existe o si la contraseña es incorrecta.
            if (cuenta == null ||
                !VerificadorContrasena.Verificar(modelo.Contrasena, cuenta.ContrasenaHash))
            {
                ModelState.AddModelError("", "El usuario o la contraseña son incorrectos.");
                return View(modelo);
            }

            // Inicia la sesión del personal con una cookie no persistente.
            FormsAuthentication.SetAuthCookie(cuenta.NombreUsuario, false);
            return RedirectToAction("Index", "Libros");
        }

        // Elimina la cookie de autenticación y vuelve al formulario de acceso.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}