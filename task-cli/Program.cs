using TaskCli.Models;
using TaskCli.Services;

namespace TaskCli
{
    class Program
    {
        static void Main(string[] args)
        {
            // Checking for an empty string
            if (args.Length == 0) 
            {
                Console.WriteLine("You haven't entered any string arguments, repeat the request");
                return;
            }

            ServiceForListOfTasks service = new ServiceForListOfTasks();
            service.Load();

            string command = args[0];

            switch (command)
            {
                case "add":
                    if (args.Length == 2)
                    {
                        string description = args[1];
                        if (service.Add(description))
                        {
                            service.Save();
                            Console.WriteLine("Task added");
                        }
                        else
                        {
                            Console.WriteLine("Couldn't add task");
                        }
                    }
                    else if (args.Length > 2)
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }
                    else
                    {
                        Console.WriteLine("Missed entering the task name");
                    }

                    break;

                case "update":
                    if (args.Length == 3)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            string newDescription = args[2];

                            if (service.Update(id, newDescription) != null)
                            {
                                service.Save();
                                Console.WriteLine("The task has been updated");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The entered value does not match the id type");
                        }
                    }
                    else if (args.Length > 3)
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }
                    else
                    {
                        Console.WriteLine("Missed entering the task id and/or changes");
                    }

                    break;

                case "delete":
                    if (args.Length == 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.Delete(id))
                            {
                                service.Save();
                                Console.WriteLine("Task deleted");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The entered value does not match the id type");
                        }
                    }
                    else if (args.Length > 2)
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }
                    else
                    {
                        Console.WriteLine("Missed entering the task id");
                    }     
                    
                    break;

                case "mark-in-progress":
                    if (args.Length == 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.MarkInProgress(id))
                            {
                                service.Save();
                                Console.WriteLine("The task is marked as in progress");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The entered value does not match the id type");
                        }
                    }
                    else if (args.Length > 2)
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }
                    else
                    {
                        Console.WriteLine("Missed entering the task id");
                    }

                    break;

                case "mark-done":
                    if (args.Length == 2)
                    {
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.MarkDone(id))
                            {
                                service.Save();
                                Console.WriteLine("The task is marked as done");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            Console.WriteLine("The entered value does not match the id type");
                        }
                    }
                    else if (args.Length > 2)
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }
                    else
                    {
                        Console.WriteLine("Missed entering the task id");
                    }

                    break;

                case "list":
                    if (args.Length == 1)
                    {
                        service.List();
                    }
                    else if (args.Length == 2)
                    {
                        string status = args[1];

                        switch (status)
                        {
                            case "todo":
                                service.Todo();
                                break;

                            case "in-progress":
                                service.InProgress();
                                break;

                            case "done":
                                service.Done();
                                break;

                            default:
                                Console.WriteLine("There is no such type of task");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Exceeding the possible number of arguments");
                    }

                    break;

                default:
                    Console.WriteLine("Unknown command");
                    return;
            }
        }
    }
}
