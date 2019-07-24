using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubtaskAPI.Models;

namespace SubtaskAPI.Logic
{
    public interface ITaskLogic
    {
        IEnumerable<TaskItem> GetAllTaskItems();
        IDictionary<int,FullTask> GetAllFullTasks();

        TaskEntityState GetAllTasks();
        IList<TaskItem> GetTaskItemsFromEntity(IDictionary<int, FullTask> td);

        void Save(TaskEntityState t);
    }
}
