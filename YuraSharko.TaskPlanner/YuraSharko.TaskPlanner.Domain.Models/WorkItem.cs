using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YuraSharko.TaskPlanner.Domain.Models.Enums;

namespace YuraSharko.TaskPlanner.Domain.Models
{
    public class WorkItem : ICloneable
    {
        public DateTime DueTime;
        public DateTime CreationDate;
        public Priority Priority;
        public Complexity Complexity;
        public string Title;
        public string Description;
        public bool IsCompleted;
        public Guid Id;

        public object Clone()
        {
            WorkItem clone = new WorkItem();
            try
            {
                clone.Title = (string)Title.Clone();
                clone.DueTime = DueTime;
                clone.Priority = Priority;
            }
            catch { }
            try
            {
                clone.IsCompleted = IsCompleted;
                clone.Description = Description;
                clone.Complexity = Complexity;
                clone.CreationDate = CreationDate;
            }catch { }
            return clone;
        }

        public override string ToString()
        {
            return String.Format($"{Title}: Due - {DueTime.ToString("dd.MM.yyyy")}; Priority - {Priority}\n{Id}");
        }
    }
}
