namespace TaskManager.Controllers
{
    public class CreateTask
    {
    }

    public class ChangeTaskName
    {
        public int taskId { get; set; }
        public string newName { get; set; }
    }
}