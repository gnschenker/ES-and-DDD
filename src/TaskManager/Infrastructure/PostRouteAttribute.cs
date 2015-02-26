using System.Net.Http;

namespace TaskManager.Infrastructure
{
    public class PostRouteAttribute : MethodConstrainedRouteAttribute
    {
        public PostRouteAttribute(string template) : base(template, HttpMethod.Post) { }
    }
}