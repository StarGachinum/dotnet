using YuraSharko.TaskPlanner.Domain.Models.Enums;
using YuraSharko.TaskPlanner.Domain.Models;
using YuraSharko.TaskPlanner.Domain.Logic;

internal static class Program
{
    public static List<WorkItem> Items { get; private set; }

    private static void Main(string[] args)
    {
        Items = new List<WorkItem>();
        Console.WriteLine("Press A to add new task.");
        Console.WriteLine("Press F to finish entering tasks.");
        while (true)
        {
            var key = Console.ReadKey(true);
            if (key.KeyChar.Equals('a'))
            {
                AddItem();
            }
            else if( key.KeyChar.Equals('f'))
            {
                break;
            }
            Console.WriteLine("Press A to add new task.");
            Console.WriteLine("Press F to finish entering tasks.");
        }
        var planner = new SimpleTaskPlanner();
        var plan = planner.CreatePlan(Items.ToArray());
        foreach(var task in plan)
        {
            Console.WriteLine(task);
        }  
    }
    private static void AddItem()
    {
        var workItem = new WorkItem();
         
        Console.Write("Enter the title: ");
        workItem.Title = Console.ReadLine();

        // Введення значення DueDate
        Console.Write("Enter the due date (dd-MM-yyyy): ");
        string dateInput = Console.ReadLine();

        // Перевірка на коректність дати
        while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out workItem.DueTime))
        {
            Console.Write("Invalid date. Please enter the due date (dd-MM-yyyy): ");
            dateInput = Console.ReadLine();
        }

        // Введення значення Priority
        Console.WriteLine("Enter the priority (None = 0, Low = 1, Medium = 2, High = 3, Urgent = 4): ");
        string priorityInput = Console.ReadLine();
        int priorityValue;

        // Перевірка на коректність вибору пріоритету
        while (!int.TryParse(priorityInput, out priorityValue) || !Enum.IsDefined(typeof(Priority), priorityValue))
        {
            Console.WriteLine("Invalid priority. Please enter the priority (None = 0, Low = 1, Medium = 2, High = 3, Urgent = 4): ");
            priorityInput = Console.ReadLine();
        }

        workItem.Priority = (Priority)priorityValue;

        Items.Add(workItem);
        Console.WriteLine($"Added {workItem}");
    }

    
}