using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using YuraSharko.TaskPlanner.Domain.Models;
using YuraSharko.TaskPlanner.DataAccess.Abstractions;

namespace YuraSharko.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";
        private readonly Dictionary<Guid, WorkItem> _workItems;

        public FileWorkItemsRepository()
        {
            if (File.Exists(FileName))
            {
                var fileContent = File.ReadAllText(FileName);
                if (!string.IsNullOrEmpty(fileContent))
                {
                    var items = JsonConvert.DeserializeObject<WorkItem[]>(fileContent);
                    _workItems = new Dictionary<Guid, WorkItem>();
                    foreach (var item in items)
                    {
                        _workItems[item.Id] = item;
                    }
                }
                else
                {
                    _workItems = new Dictionary<Guid, WorkItem>();
                }
            }
            else
            {
                _workItems = new Dictionary<Guid, WorkItem>();
            }
        }

        public Guid Add(WorkItem workItem)
        {
            WorkItem clone = workItem.Clone() as WorkItem;
            clone.Id = Guid.NewGuid();
            _workItems[clone.Id] = clone;
            return workItem.Id;
        }

        public WorkItem Get(Guid id)
        {
            return _workItems.ContainsKey(id) ? _workItems[id] : null;
        }

        public WorkItem[] GetAll()
        {
            return new List<WorkItem>(_workItems.Values).ToArray();
        }

        public bool Update(WorkItem workItem)
        {
            if (_workItems.ContainsKey(workItem.Id))
            {
                _workItems[workItem.Id] = workItem;
                return true;
            }
            return false;
        }

        public bool Remove(Guid id)
        {
            return _workItems.Remove(id);
        }

        public void SaveChanges()
        {
            var json = JsonConvert.SerializeObject(_workItems.Values, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
