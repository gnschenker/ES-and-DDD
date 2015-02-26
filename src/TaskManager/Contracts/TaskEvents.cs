namespace TaskManager.Contracts
{
    public class TaskCreated
    {
        public int Id { get; set; }
        public string DefaultName { get; set; }
    }

    public class TaskNameChanged
    {
        public int Id { get; set; }
        public string NewName { get; set; }
    }
}