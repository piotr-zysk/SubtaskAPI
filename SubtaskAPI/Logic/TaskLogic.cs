using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SubtaskAPI.Models;
using SubtaskAPI.Repositories;

namespace SubtaskAPI.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private ITaskRepository _repo;

        public TaskLogic(ITaskRepository _repo)
        {
            this._repo = _repo;
        }

        public IEnumerable<FullTask> GetAllFullTasks()
        {
            var allTaskItems = _repo.GetAllTaskItems();

            var subtasks = allTaskItems.Where(t => t.ParentId > 0);

            var tasks = allTaskItems.Where(t => t.ParentId == 0).GroupJoin(subtasks,
                task => task.Id,
                subtask => subtask.ParentId,
                (task, subtaskList) =>
                    new FullTask
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Done = task.Done,
                        Items = subtaskList.ToList()
                    }).ToList();
            
            return tasks;
        }

        public IEnumerable<TaskItem> GetAllTaskItems()
        {
            return _repo.GetAllTaskItems();
        }

        public string Test()
        {
            return "dada";
        }
    }
}
