using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubtaskAPI.Models
{
    public class FullTask
    {
        public int Id;
        public bool Done;
        public string Title;
        public IList<TaskItem> Items;
    }
}
