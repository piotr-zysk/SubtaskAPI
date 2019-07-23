using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubtaskAPI.Models
{
    public class TaskItem
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string Title { get; set; }
        public virtual bool Done { get; set; }
    }
}
