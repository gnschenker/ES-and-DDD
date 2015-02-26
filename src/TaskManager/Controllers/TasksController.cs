using System.Web.Http;
using TaskManager.Domain;
using TaskManager.Infrastructure;

namespace TaskManager.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TasksController : ApiController
    {
        private static readonly IIdGenerator idGenerator;

        static TasksController()
        {
            idGenerator = new IdGenerator();
        }

        public TaskInfo Get(int id)
        {
            return null;
        }

        [Route("create")]
        public object Post([FromBody]CreateTask command)
        {
            var id = idGenerator.Next<TaskAggregate>();
            // do something with the command!
            return new
            {
                id = id,
                defaultName = "Task " + id
            };
        }
    }
}