using System.Web.Http;
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

        [Route("create")]
        public object Post([FromBody]CreateTask command)
        {
            var result = service.When(command.ToInternal());
            return new
            {
                id = result.Id,
                defaultName = result.DefaultName
            };
        }
    }
}