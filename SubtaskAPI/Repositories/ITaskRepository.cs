using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubtaskAPI.Models;

namespace SubtaskAPI.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskItem> GetAllTaskItems();

        string GetIdsString();

        void DeleteTaskItems(IEnumerable<int> ids);
        void AddTaskItems(ICollection<TaskItem> taskItems);
        void SetTaskOrder(string newOrder);
    }
}
