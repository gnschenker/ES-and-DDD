namespace TaskManager.Domain
{
    public class CreateTaskCommand
    {
    }

    public class ChangeTaskNameCommand
    {
        public int TaskId { get; set; }
        public string NewName { get; set; }
    }
}