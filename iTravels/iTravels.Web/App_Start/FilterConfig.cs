using System.Web;
using System.Web.Mvc;

namespace iTravels.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new iTAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
