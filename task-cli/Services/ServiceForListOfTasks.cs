using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TaskCli.Models;

namespace TaskCli.Services
{
    // Service responsible for managing the task list.
    // Handles loading, saving, and CRUD operations (Create, Read, Update, Delete).
    internal class ServiceForListOfTasks
    {
        // In-memory list of tasks. Initialized as an empty list.
        // Note: Nullable (?) is used but initialized immediately.
        private List<TaskElement>? listOfTasks = new List<TaskElement>();

        // Filename for storing tasks in JSON format.
        private const string jsonName = "listOfTasks.json";

        public void Load()
        {
            // Check if the JSON file exists before attempting to read
            if (File.Exists(jsonName))
            {
                string list = File.ReadAllText(jsonName, Encoding.UTF8);

                try
                {
                    // If file is not empty, attempt to deserialize JSON into list
                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        // Warning: Deserialize can return null. Consider adding '?? new List<...>()'
                        listOfTasks = JsonSerializer.Deserialize<List<TaskElement>>(list);
                    }
                    else
                    {
                        // File is empty or corrupted: create backup and delete original
                        File.Copy(jsonName, "listOfTasks.json.backup", true);
                        File.Delete(jsonName);

                        Console.WriteLine("The list has been corrupted, and a backup copy of the list has been saved in a file: listOfTasks.json.backup");
                    }
                }
                catch (Exception ex)
                {
                    // Handle JSON parsing errors or file access issues
                    Console.WriteLine(ex.Message);
                }
            }
            // If file doesn't exist, list remains empty (expected behavior for first run)
        }

        public void Save()
        {
            // Serialize the task list to JSON string with indentation for readability
            string jsonOfTasks = JsonSerializer.Serialize(listOfTasks, new JsonSerializerOptions { WriteIndented = true });

            // Write JSON to file using UTF8 encoding (supports special characters)
            File.WriteAllText(jsonName, jsonOfTasks, Encoding.UTF8);          
        }

        public bool Add(string description) 
        {
            if (listOfTasks is not null)
            {
                // Generate new ID: start at 1, or increment based on the last task's ID
                int idCount = 1;

                if (listOfTasks.Count > 0)
                {
                    // Warning: If the last task was deleted, this logic might duplicate IDs.
                    // Better approach: listOfTasks.Max(t => t.ID) + 1
                    idCount = listOfTasks[listOfTasks.Count - 1].ID + 1;
                }

                // Create and add new task with default status 'todo'
                listOfTasks?.Add(new TaskElement()
                {
                    ID = idCount,
                    description = description,
                    status = Status.todo,        // Default status for new tasks
                    createdAt = DateTime.Now,    // Set creation timestamp
                    updatedAt = DateTime.Now     // Set last update timestamp
                });

                return true; // Successfully added
            }

            return false; // Should not happen if initialized correctly
        }

        public TaskElement? Update(int id, string newDescription) 
        {
            if (listOfTasks is not null)
            {
                // Find task by matching ID
                TaskElement? task = listOfTasks.Find(task => task.ID == id);

                if (task != null)
                {
                    // Update description and update time if task exists
                    task.description = newDescription;
                    task.updatedAt = DateTime.Now;
                }

                // Return the updated task object, or null if not found
                return task;
            }

            return null;
        }

        public bool Delete(int id) 
        {
            if (listOfTasks is not null)
            {
                // Find task by ID
                TaskElement? task = listOfTasks.Find(task => task.ID == id);

                if (task != null && listOfTasks.Count > 0)
                {
                    // Remove task from list. Returns true if successfully removed.
                    return listOfTasks.Remove(task);
                }

                return false; // Task not found
            }

            return false;
        }

        public bool MarkInProgress(int id) 
        {
            if (listOfTasks is not null)
            {
                TaskElement? task = listOfTasks.Find(task => task.ID == id);

                if (task != null)
                {
                    // Change status to 'inProgress'
                    task.status = Status.inProgress;

                    return true;
                }
            }

            return false;
        }

        public bool MarkDone(int id) 
        {
            if (listOfTasks is not null)
            {
                TaskElement? task = listOfTasks.Find(task => task.ID == id);

                if (task != null)
                {
                    // Change status to 'done'
                    task.status = Status.done;

                    return true;
                }
            }

            return false;
        }

        public void List() 
        {
            if (listOfTasks is not null)
            {
                if (listOfTasks.Count > 0)
                {
                    // Iterate through all tasks and print using overridden ToString()
                    foreach (TaskElement task in listOfTasks)
                    {
                        Console.WriteLine(task.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("The list is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty"); // Fallback for null safety
            }
        }

        public void Todo()
        {
            if (listOfTasks is not null)
            {
                if (listOfTasks.Count > 0)
                {
                    // LINQ Query: Filter tasks where status equals 'todo'
                    var tasksToDo = from task in listOfTasks
                                    where task.status == Status.todo
                                    select task;

                    if (tasksToDo.Count() > 0)
                    {
                        // Print filtered tasks
                        foreach (TaskElement task in tasksToDo)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("The list todo is empty");
                    }
                }
                else
                {
                    Console.WriteLine("The list is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty");
            }
        }

        public void InProgress() 
        {
            if (listOfTasks is not null)
            {
                if (listOfTasks.Count > 0)
                {
                    // Similar to Todo(), but filters by Status.inProgress
                    var tasksInProgress = from task in listOfTasks
                                          where task.status == Status.inProgress
                                          select task;

                    if (tasksInProgress.Count() > 0)
                    {
                        foreach (TaskElement task in tasksInProgress)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("The list in progress is empty");
                    }
                }
                else
                {
                    Console.WriteLine("The list is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty"); 
            }
        }

        public void Done() 
        {
            if (listOfTasks is not null)
            {
                if (listOfTasks.Count > 0)
                {
                    // Similar to Todo(), but filters by Status.done
                    var tasksDone = from task in listOfTasks
                                    where task.status == Status.done
                                    select task;

                    if (tasksDone.Count() > 0)
                    {
                        foreach (TaskElement task in tasksDone)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("The list done is empty");
                    }
                }
                else
                {
                    Console.WriteLine("The list is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty");
            }
        }
    }
}
