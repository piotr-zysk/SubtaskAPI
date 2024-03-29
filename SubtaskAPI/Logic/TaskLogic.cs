﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AutoMapper;
using SubtaskAPI.Automapper;
using SubtaskAPI.Models;
using SubtaskAPI.Repositories;

namespace SubtaskAPI.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private ITaskRepository _repo;
        private IMapper _mapper;

        public TaskLogic(ITaskRepository _repo, IMapper _mapper)
        {
            this._repo = _repo;
            this._mapper = _mapper;
        }

        public (IDictionary<int, FullTask>, int) GetAllFullTasks()
        {
            var allTaskItems = _repo.GetAllTaskItemsDTO();

            int maxId = _repo.GetAllTaskItemsDTO().Max(t => t.Id);

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
                    }).ToDictionary(item => item.Id, item => item);

            return (tasks, maxId);
        }

        public IEnumerable<TaskItem> GetAllTaskItems()
        {
            return _repo.GetAllTaskItemsDTO();
        }

        public IEnumerable<TaskItemDTO> GetAllTaskItemsDTO()
        {
            return _mapper.Map<IEnumerable<TaskItem>, IEnumerable<TaskItemDTO>>(GetAllTaskItems());
        }

        public TaskEntityState GetAllTasks()
        {
            var te = new TaskEntityState();
            te.State = new State();
            (te.Entities, te.State.maxId) = this.GetAllFullTasks();
            te.Ids = _repo.GetIdsString().Split(',').Select(x => int.Parse(x)).ToArray();

            return te;
        }

        public IList<TaskItem> GetTaskItemsFromEntity(IDictionary<int,FullTask> td)
        {
            // var allTasks = this.GetAllFullTasks().Values.ToList();

            List<TaskItem> tasks = td.Values.Select(x => new TaskItem { Done = x.Done, Id = x.Id, ParentId = 0, Title = x.Title }).ToList();
            List<TaskItem> subTasks = td.Values.Select(i => i.Items.Select(t => new TaskItem
            {
                ParentId = i.Id,
                Title = t.Title,
                Id = t.Id,
                Done = t.Done
            } )).SelectMany(i => i).ToList();

            tasks.AddRange(subTasks);

            return tasks;
        }

        public void Save(TaskEntityState t)
        {
            this._repo.DeleteTaskItems(t.State.ItemsToDelete);
            this._repo.AddTaskItems(GetTaskItemsFromEntity(t.Entities));
            this._repo.SetTaskOrder(String.Join(",", t.Ids));
        }
    }
}
