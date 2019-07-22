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
        IEnumerable<FullTask> GetAllFullTasks();
        string Test();
    }
}
