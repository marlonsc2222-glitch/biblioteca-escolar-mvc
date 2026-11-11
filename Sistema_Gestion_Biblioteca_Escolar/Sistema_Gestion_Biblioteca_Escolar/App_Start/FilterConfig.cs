using System.Web;
using System.Web.Mvc;

namespace Sistema_Gestion_Biblioteca_Escolar
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
