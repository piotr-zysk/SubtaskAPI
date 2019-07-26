using System.Collections.Generic;
using AutoMapper;
using NUnit.Framework;
using SubtaskAPI.Logic;
using Moq;
using SubtaskAPI.Models;
using SubtaskAPI.Repositories;
using SubtaskAPI.Automapper;

namespace Tests
{
    public class LogicTest
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts =>
            {
                // Add your mapper profile configs or mappings here
                opts.CreateMap<TaskItem, TaskItemDTO>();
            });

            
            mapper = config.CreateMapper(); 
        }

        [Test]
        public void Test1()
        {
            var repo = new Mock<ITaskRepository>();

            var list = new List<TaskItem>();
            list.Add(new TaskItem());

            var list2 = new List<TaskItem>();
            list2.Add(new TaskItem());

            repo.Setup(x => x.GetAllTaskItemsDTO()).Returns(list);

            var result = new TaskLogic(repo.Object, mapper).GetAllTaskItemsDTO();

            Assert.That(result, Is.InstanceOf(typeof(List<TaskItemDTO>)));
        }
    }
}