using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using SubtaskAPI.Models;

namespace SubtaskAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISession _session;

        public TaskRepository(ISession session)
        {
            this._session = session;
        }

        public void AddTaskItems(ICollection<TaskItem> taskItems)
        {
            using (var transaction = _session.BeginTransaction())
            {
                foreach (var taskItem in taskItems)
                {
                    this._session.SaveOrUpdate(taskItem);
                }
                transaction.Commit();
            }
            
            
        }

        public void DeleteTaskItems(IEnumerable<int> ids)
        {
            this._session.Query<TaskItem>()
                .Where(c => ids.Contains(c.Id)).Delete();
        }

        //private List<TaskItem> _entities;

        //private string _order;

        //public TaskRepository()
        //{
        //    this._entities = new List<TaskItem>();
        //    this._entities.Add(new TaskItem { Done = true, Id = 1, ParentId = 0, Title = "blabla lala"} );
        //    this._entities.Add(new TaskItem { Done = false, Id = 2, ParentId = 0, Title = "dubi dubi da" });
        //    this._entities.Add(new TaskItem { Done = false, Id = 3, ParentId = 2, Title = "podtask dubi" });
        //    this._entities.Add(new TaskItem { Done = false, Id = 4, ParentId = 2, Title = "podtask dubi 2" });
        //    this._entities.Add(new TaskItem { Done = false, Id = 5, ParentId = 1, Title = "podtask bla" });
        //    this._entities.Add(new TaskItem { Done = false, Id = 6, ParentId = 0, Title = "single element" });

        //    this._order = "1,2,6";

        //}

        public IEnumerable<TaskItem> GetAllTaskItems()
            => this._session.Query<TaskItem>().ToList();

        public string GetIdsString()
            => this._session.Query<TaskOrder>().Select(s => s.Ids).FirstOrDefault();

        public void SetTaskOrder(string newOrder)
        {
            this._session.Query<TaskOrder>().Where(t => t.Id == 1).UpdateBuilder().Set(c => c.Ids, newOrder).Update();
        }
    }
}
