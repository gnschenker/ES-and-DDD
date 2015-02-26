using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TaskManager.Infrastructure
{
    public class MethodConstrainedRouteAttribute : RouteFactoryAttribute
    {
        public MethodConstrainedRouteAttribute(string template, HttpMethod method)
            : base(template)
        {
            Method = method;
        }

        public HttpMethod Method
        {
            get;
            private set;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                var constraints = new HttpRouteValueDictionary();
                constraints.Add("method", new MethodConstraint(Method));
                return constraints;
            }
        }
    }
}