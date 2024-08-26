using YuraSharko.TaskPlanner.Domain.Models;

namespace YuraSharko.TaskPlanner.Domain.Logic
{
    public class SimpleTaskPlanner
    {
        public WorkItem[] CreatePlan (WorkItem[] items)
        {
            var list = items.ToList();
            list.Sort(comparer);
            return list.ToArray();
        }

        private int comparer( WorkItem item1, WorkItem item2)
        {
            if (!item1.Priority.Equals(item2.Priority))
            {
                return item1.Priority > item2.Priority ? 1 : -1;
            }

            if (DateTime.Compare(item1.DueTime.Date, item2.DueTime.Date) != 0)
            {
                return DateTime.Compare(item1.DueTime.Date, item2.DueTime.Date) * -1;
            }

            return item1.Title.CompareTo(item2.Title);
        }
    }
}