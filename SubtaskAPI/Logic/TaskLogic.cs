using System;
using System.Collections.Generic;
using System.Linq;
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
