using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using TaskManager.Domain;
using TaskManager.Infrastructure;

namespace TaskManager.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private readonly ITaskApplicationService service;

        public TasksController()
        {
            var idGenerator = new IdGenerator();
            var repository = new GESRepository();
            service = new TaskApplicationService(repository, idGenerator);
        }

        public TaskInfo Get(int id)
        {
            return null;
        }

        [PostRoute("create")]
        public object Create([FromBody]CreateTask command)
        {
            var result = service.When(command.ToInternal());
            return new
            {
                id = result.Id,
                defaultName = result.DefaultName
            };
        }

        [PostRoute("changeName")]
        public void ChangeName([FromBody]ChangeTaskName command)
        {
            service.When(command.ToInternal());
        }
    }
}