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
    }
}
