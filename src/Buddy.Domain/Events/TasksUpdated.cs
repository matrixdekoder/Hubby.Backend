using Core.Domain;
using System.Collections.Generic;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class TasksUpdated: IEvent
    {
        public TasksUpdated(string id, IList<Task> tasks)
        {
            Id = id;
            Tasks = tasks;
        }

        public string Id { get; }
        public IList<Task> Tasks { get; }
    }
}
