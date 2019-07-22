using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubtaskAPI.Models
{
    public class TaskItem
    {
        public int Id;
        public int ParentId;
        public string Title;
        public bool Done;
    }
}
