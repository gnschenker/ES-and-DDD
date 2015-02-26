using TaskManager.Contracts;
using TaskManager.Infrastructure;

namespace TaskManager.ReadModel
{
    public class TaskProjection
    {
        private readonly IAtomicWriter<TaskView> writer;

        public TaskProjection(IAtomicWriter<TaskView> writer)
        {
            this.writer = writer;
        }

        public void When(TaskCreated e)
        {
            writer.Add(e.Id, () => new TaskView
            {
                Id = e.Id,
                Name = e.DefaultName
            });
        }

        public void When(TaskNameChanged e)
        {
            writer.Update(e.Id, v =>
            {
                v.Name = e.NewName;
                return v;
            });
        }
    }
}