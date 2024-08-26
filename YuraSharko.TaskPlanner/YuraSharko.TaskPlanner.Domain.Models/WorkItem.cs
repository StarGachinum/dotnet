using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuraSharko.TaskPlanner.Domain.Models.Enums;

namespace YuraSharko.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public DateTime DueTime;
        public DateTime CreationDate;
        public Priority Priority;
        public Complexity Complexity;
        public string Title;
        public string Description;
        public bool IsCompleted;

        public override string ToString()
        {
            return String.Format($"{Title}: Due - {DueTime.ToString("dd.MM.yyyy")}; Priority - {Priority}");
        }
    }
}
