using System.Linq;
using Sistema_Gestion_Biblioteca_Escolar.Models;

namespace Sistema_Gestion_Biblioteca_Escolar.Data
{
    public class CuentaPersonalRepository
    {
        // Consulta sugerida por ChatGPT y adaptada a BibliotecaContext.Cuentas.
        public CuentaPersonal BuscarPorNombreUsuario(string nombreUsuario)
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Cuentas
                    .SingleOrDefault(cuenta => cuenta.NombreUsuario == nombreUsuario);
            }
        }
    }
}