using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubtaskAPI.Models
{
    public class TaskEntityState
    {
        public IDictionary<int,FullTask> Entities;
        public IList<int> Ids;
        public State State;
    }
}
