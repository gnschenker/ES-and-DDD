using System;

namespace TaskManager.Controllers
{
    public class TaskInfo
    {
        public int taskId { get; set; }
        public string taskName { get; set; }
        public string instructions { get; set; }
        public string comments { get; set; }
        public int taskTypeId { get; set; }
        public int taskStatus { get; set; }
        public DateTime dueDate { get; set; }
    }
}