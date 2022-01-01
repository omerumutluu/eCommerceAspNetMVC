using System.Web;
using System.Web.Mvc;

namespace eCommerce_Asp.Net_MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
