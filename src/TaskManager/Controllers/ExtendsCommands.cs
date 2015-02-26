using TaskManager.Domain;

namespace TaskManager.Controllers
{
    public static class ExtendsCommands
    {
        public static CreateTaskCommand ToInternal(this CreateTask c)
        {
            return new CreateTaskCommand();
        }
        public static ChangeTaskNameCommand ToInternal(this ChangeTaskName c)
        {
            return new ChangeTaskNameCommand{TaskId = c.taskId, NewName = c.newName};
        }
    }
}