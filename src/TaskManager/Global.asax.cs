using System;
using System.Web.Http;
using System.Web.Routing;

namespace TaskManager
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHttpRoute(
                     name: "Api",
                     routeTemplate: "api/{controller}/{id}",
                     defaults: new { id = RouteParameter.Optional }
                     );
        }
    }
}