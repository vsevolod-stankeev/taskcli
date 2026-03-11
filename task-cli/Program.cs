using TaskCli.Models;
using TaskCli.Services;
using static System.Net.WebRequestMethods;

namespace TaskCli
{
    class Program
    {
        // Main entry point of the TaskCli application.
        // Handles command-line argument parsing and orchestrates service calls.
        static void Main(string[] args)
        {
            // Validate: Check if user provided any command-line arguments
            if (args.Length == 0) 
            {
                Console.WriteLine("You haven't entered any string arguments, repeat the request");
                return;
            }

            // Initialize the task service and load existing data from JSON file
            ServiceForListOfTasks service = new ServiceForListOfTasks();
            service.Load();

            // Extract the command from the first argument (e.g., "add", "delete", "list")
            string command = args[0];

            switch (command)
            {
                case "add":
                    // Command: Add a new task to the list
                    // Expected usage: add "Task description"
                    // Requires: Exactly 2 arguments (command + description)
                    if (args.Length == 2)
                    {
                        string description = args[1];
                        if (service.Add(description))
                        {
                            service.Save();  // Persist changes to JSON file
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
                    // Command: Update an existing task's description by ID
                    // Expected usage: update <ID> "New description"
                    // Requires: Exactly 3 arguments (command + ID + new description)
                    if (args.Length == 3)
                    {
                        // Parse ID from string to integer safely
                        if (int.TryParse(args[1], out int id))
                        {
                            string newDescription = args[2];

                            if (service.Update(id, newDescription) != null)
                            {
                                service.Save();  // Persist changes to JSON file
                                Console.WriteLine("The task has been updated");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            // Error: ID is not a valid integer
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
                    // Command: Delete a task from the list by ID
                    // Expected usage: delete <ID>
                    // Requires: Exactly 2 arguments (command + ID)
                    if (args.Length == 2)
                    {
                        // Parse ID from string to integer safely
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.Delete(id))
                            {
                                service.Save();  // Persist changes to JSON file
                                Console.WriteLine("Task deleted");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            // Error: ID is not a valid integer
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
                    // Command: Change task status to "In Progress"
                    // Expected usage: mark-in-progress <ID>
                    // Requires: Exactly 2 arguments (command + ID)
                    if (args.Length == 2)
                    {
                        // Parse ID from string to integer safely
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.MarkInProgress(id))
                            {
                                service.Save();  // Persist changes to JSON file
                                Console.WriteLine("The task is marked as in progress");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            // Error: ID is not a valid integer
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
                    // Command: Change task status to "Done" (completed)
                    // Expected usage: mark-done <ID>
                    // Requires: Exactly 2 arguments (command + ID)
                    if (args.Length == 2)
                    {
                        // Parse ID from string to integer safely
                        if (int.TryParse(args[1], out int id))
                        {
                            if (service.MarkDone(id))
                            {
                                service.Save();  // Persist changes to JSON file
                                Console.WriteLine("The task is marked as done");
                            }
                            else
                            {
                                Console.WriteLine("The task with this id is not in the list");
                            }
                        }
                        else
                        {
                            // Error: ID is not a valid integer
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
                    // Command: Display tasks from the list
                    // Usage variants:
                    //   - "list" (no args): Show all tasks
                    //   - "list todo": Show only tasks with status "todo"
                    //   - "list in-progress": Show only tasks in progress
                    //   - "list done": Show only completed tasks
                    if (args.Length == 1)
                    {
                        // No filter: display all tasks
                        service.List();
                    }
                    else if (args.Length == 2)
                    {
                        // Filter by status: extract status from second argument
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
                                // Error: Unknown status filter
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
                    // Handle unknown/invalid commands
                    Console.WriteLine("Unknown command");
                    return;
            }
        }
    }
}
