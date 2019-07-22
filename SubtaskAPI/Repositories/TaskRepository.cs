using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubtaskAPI.Models;

namespace SubtaskAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private List<TaskItem> _entities;

        private string _order;

        public TaskRepository()
        {
            this._entities = new List<TaskItem>();
            this._entities.Add(new TaskItem { Done = true, Id = 1, ParentId = 0, Title = "blabla lala"} );
            this._entities.Add(new TaskItem { Done = false, Id = 2, ParentId = 0, Title = "dubi dubi da" });
            this._entities.Add(new TaskItem { Done = false, Id = 3, ParentId = 2, Title = "podtask dubi" });
            this._entities.Add(new TaskItem { Done = false, Id = 4, ParentId = 2, Title = "podtask dubi 2" });
            this._entities.Add(new TaskItem { Done = false, Id = 5, ParentId = 1, Title = "podtask bla" });
            this._entities.Add(new TaskItem { Done = false, Id = 6, ParentId = 0, Title = "single element" });

            this._order = "1,2";

        }

        public IEnumerable<TaskItem> GetAllTaskItems()
            => this._entities;

        public string GetIdsString()
            => this._order;
    }
}
