using TaskManager.Domain;

namespace TaskManager.Controllers
{
    public static class ExtendsCommands
    {
        public static CreateTaskCommand ToInternal(this CreateTask c)
        {
            return new CreateTaskCommand();
        }
    }
}