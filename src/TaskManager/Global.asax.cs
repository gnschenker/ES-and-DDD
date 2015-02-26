using System;
using System.Net.Http;
using System.Web.Http;
using TaskManager.Infrastructure;
using TaskManager.ReadModel;

namespace TaskManager
{
    //**********************************************************************
    // Create Folder:   c:\temp\mongo 
    // Start Mongo:     mongod --dbpath c:\temp\mongo
    // Start GES:       EventStore.ClusterNode.exe --db c:\temp\ES\TaskManger
    //**********************************************************************
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(SetupRouteInfo);

            new EventDispatcherBootstrapper(new ProjectionRegistry()).Initialize();
        }

        private void SetupRouteInfo(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("Api", "api/{controller}/{id}", new { id = RouteParameter.Optional }, new { id = @"\d+" });
            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");
            config.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", new { action = "Get" }, new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Get) });
            config.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", new { action = "Post" }, new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Post) });
        }
    }
}