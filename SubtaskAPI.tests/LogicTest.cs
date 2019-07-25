using System.Collections.Generic;
using NUnit.Framework;
using SubtaskAPI.Logic;
using Moq;
using SubtaskAPI.Models;
using SubtaskAPI.Repositories;

namespace Tests
{
    public class LogicTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var repo = new Mock<ITaskRepository>();

            var list = new List<TaskItem>();
            list.Add(new TaskItem());

            var list2 = new List<TaskItem>();
            list2.Add(new TaskItem());

            repo.Setup(x => x.GetAllTaskItems()).Returns(list);

            var result = new TaskLogic(repo.Object).GetAllTaskItems();

            Assert.That(result, Is.InstanceOf(typeof(List<TaskItem>)));
        }
    }
}