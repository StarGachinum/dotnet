using YuraSharko.TaskPlanner.Domain.Models.Enums;
using YuraSharko.TaskPlanner.Domain.Models;
using YuraSharko.TaskPlanner.Domain.Logic;
using YuraSharko.TaskPlanner.DataAccess;

internal static class Program
{
    private static FileWorkItemsRepository _wir;
    private static Dictionary<char,Action> _actions;
    private static string menu = "[A]dd work item\n[B]uild a plan\n" +
        "[M]ark work item as completed\n[R]emove a work item\n[E]xit";

    private static void Main(string[] args)
    {
        
        _wir = new FileWorkItemsRepository();
        FillActions();
        Console.WriteLine(menu);
        while (true)
        {
            var key = Console.ReadKey(true);

            if(_actions.ContainsKey(key.KeyChar))
            {
                _actions[key.KeyChar]();
                Console.ReadKey(true);
            }
            
            Console.Clear();
            Console.WriteLine(menu);
        }
        _wir.SaveChanges();
        
    }

    private static void FillActions()
    {
        _actions = new Dictionary<char,Action>();
        _actions.Add('a', AddItem);
        _actions.Add('e', Exit);
        _actions.Add('b', BuildPlan);
        _actions.Add('r', RemoveItem);
        _actions.Add('m', MarkAsDone);
        
    }
    
    private static void MarkAsDone()
    {
        Console.Write("Enter task Id: ");
        var idString = Console.ReadLine();
        try
        {
            Guid id = Guid.Parse(idString);
            _wir.Get(id).IsCompleted = true;
            Console.WriteLine("Marked as done!");
        }
        catch
        {
            Console.WriteLine("Something gone wrong!");
        }
    }

    private static void RemoveItem()
    {
        Console.Write("Enter task Id: ");
        var idString = Console.ReadLine();
        try
        {
            Guid id = Guid.Parse(idString);
            _wir.Remove(id);
            Console.WriteLine("Successfully removed");
        }
        catch
        {
            Console.WriteLine("Something gone wrong!");
        }
    }
    private static void BuildPlan()
    {
        _wir.SaveChanges();
        var planner = new SimpleTaskPlanner();
        var plan = planner.CreatePlan(_wir.GetAll());
        foreach (var task in plan)
        {
            Console.WriteLine(task);
        }
    }
    private static void Exit()
    {
        _wir.SaveChanges();
        Environment.Exit(0);
    }

    private static void AddItem()
    {
        var workItem = new WorkItem();
         
        Console.Write("Enter the title: ");
        workItem.Title = Console.ReadLine();

        Console.Write("Enter the due date (dd-MM-yyyy): ");
        string dateInput = Console.ReadLine();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out workItem.DueTime))
        {
            Console.Write("Invalid date. Please enter the due date (dd-MM-yyyy): ");
            dateInput = Console.ReadLine();
        }

        Console.WriteLine("Enter the priority (None = 0, Low = 1, Medium = 2, High = 3, Urgent = 4): ");
        string priorityInput = Console.ReadLine();
        int priorityValue;

        while (!int.TryParse(priorityInput, out priorityValue) || !Enum.IsDefined(typeof(Priority), priorityValue))
        {
            Console.WriteLine("Invalid priority. Please enter the priority (None = 0, Low = 1, Medium = 2, High = 3, Urgent = 4): ");
            priorityInput = Console.ReadLine();
        }

        workItem.Priority = (Priority)priorityValue;

        _wir.Add(workItem);
        _wir.SaveChanges();
        Console.WriteLine($"Added {workItem}");
    }

    
}