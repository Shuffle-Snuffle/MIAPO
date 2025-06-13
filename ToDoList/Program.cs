using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Globalization;
class Program
{
    static List<Datebook> tasks = new List<Datebook>();
    static string dataFilePath = "tasks.json";

    static void Main()
    {
        if (File.Exists(dataFilePath))
        {
            string jsonData = File.ReadAllText(dataFilePath);
            tasks = JsonSerializer.Deserialize<List<Datebook>>(jsonData);
        }
        else
        {
            Console.WriteLine("File not found");
            return;
        }
        while (true)
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("1: Add task");
            Console.WriteLine("2: Delete task");
            Console.WriteLine("3: Edit task");
            Console.WriteLine("4: View tasks for today");
            Console.WriteLine("5: View tasks for tomorrow");
            Console.WriteLine("6: View tasks for the week");
            Console.WriteLine("7: View all tasks");
            Console.WriteLine("8: View upcoming tasks");
            Console.WriteLine("9: View past tasks");
            Console.WriteLine("0: Close the program");

            Console.WriteLine();
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    EditTask();
                    break;
                case "4":
                    ViewTasksForDay(DateTime.Today);
                    break;
                case "5":
                    ViewTasksForDay(DateTime.Today.AddDays(1));
                    break;
                case "6":
                    ViewTasksForWeek();
                    break;
                case "7":
                    ViewAllTasks();
                    break;
                case "8":
                    ViewUpcomingTasks();
                    break;
                case "9":
                    ViewPastTasks();
                    break;
                case "0":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Invalid input. Try again");
                    break;
            }
            Console.WriteLine("\n");
        }
    }

    static void ViewTasksForDay(DateTime date)
    {
        Console.WriteLine($"Tasks for the {date.ToShortDateString()}:");
        if (tasks.Count != 0)
        {
            foreach (var task in tasks)
            {
                if (task.TaskDeadline.Date == date.Date)
                {
                    Console.WriteLine($"- {task.Title}: ({task.Description})");
                }
            }
        }
        else
        {
            Console.WriteLine("There are no tasks in datebook");
        }
    }

    static void ViewTasksForWeek()
    {
        DateTime startDate = DateTime.Today;
        DateTime endDate = startDate.AddDays(7);

        Console.WriteLine($"Tasks from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}:");
        if (tasks.Count != 0)
        {
            foreach (var task in tasks)
            {
                if (task.TaskDeadline.Date >= startDate.Date && task.TaskDeadline.Date <= endDate.Date)
                {
                    Console.WriteLine($"- {task.Title}: ({task.Description})");
                }
            }
        }
        else
        {
            Console.WriteLine("There are no tasks in datebook");
        }
    }
    

    static void ViewUpcomingTasks()
    {
        DateTime currentDate = DateTime.Today;

        Console.WriteLine("Upcoming tasks:");
        if (tasks.Count != 0)
        {
            foreach (var task in tasks)
            {
                if (task.TaskDeadline.Date > currentDate.Date)
                {
                    Console.WriteLine($"- {task.Title}: ({task.Description}) - {task.TaskDeadline.ToShortDateString()}");
                }
            }
        }
        else
        {
            Console.WriteLine("There are no tasks in datebook");
        }
    }

    static void ViewPastTasks()
    {
        DateTime currentDate = DateTime.Today;

        Console.WriteLine("Past tasks are:");
        if (tasks.Count != 0)
        {
            foreach (var task in tasks)
            {
                if (task.TaskDeadline.Date < currentDate.Date)
                {
                    Console.WriteLine($"- {task.Title}: ({task.Description}) - {task.TaskDeadline.ToShortDateString()}");
                }
            }
        }
        else
        {
            Console.WriteLine("There are no tasks in datebook");
        }
    }

    static void SaveTasks()
    {
        string jsonData = JsonSerializer.Serialize(tasks);
        File.WriteAllText(dataFilePath, jsonData);
    }



}

class Datebook
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime TaskDeadline { get; set; }
}