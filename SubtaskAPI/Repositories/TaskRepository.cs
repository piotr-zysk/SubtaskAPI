using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubtaskAPI.Models;

namespace SubtaskAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private List<TaskItem> _t;

        public TaskRepository()
        {
            this._t = new List<TaskItem>();
            this._t.Add(new TaskItem() { Done = true, Id = 1, ParentId = 0, Title = "blabla lala"} );
            this._t.Add(new TaskItem() { Done = false, Id = 2, ParentId = 0, Title = "dubi dubi da" });
            this._t.Add(new TaskItem() { Done = false, Id = 3, ParentId = 2, Title = "podtask dubi" });

        }

        public IEnumerable<TaskItem> GetAllTaskItems()
            => this._t;
    }
}
