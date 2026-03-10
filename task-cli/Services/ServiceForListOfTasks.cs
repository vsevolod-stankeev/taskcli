using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TaskCli.Models;

namespace TaskCli.Services
{
    internal class ServiceForListOfTasks
    {
        private List<TaskElement>? listOfTasks = new List<TaskElement>();

        private const string jsonName = "listOfTasks.json";

        public void Load()
        {
            if (File.Exists(jsonName))
            {
                string list = File.ReadAllText(jsonName, Encoding.UTF8);

                try
                {
                    if (!string.IsNullOrWhiteSpace(list))
                    {
                        listOfTasks = JsonSerializer.Deserialize<List<TaskElement>>(list);
                    }
                    else
                    {
                        File.Copy(jsonName, "listOfTasks.json.backup", true);
                        File.Delete(jsonName);

                        Console.WriteLine("The list has been corrupted, and a backup copy of the list has been saved in a file: listOfTasks.json.backup");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Save()
        {
            string jsonOfTasks = JsonSerializer.Serialize(listOfTasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonName, jsonOfTasks, Encoding.UTF8);          
        }

        public bool Add(string description) 
        {
            if (listOfTasks is not null)
            {
                int idCount = 1;

                if (listOfTasks.Count > 0)
                {
                    idCount = listOfTasks[listOfTasks.Count - 1].ID + 1;
                }

                listOfTasks?.Add(new TaskElement()
                {
                    ID = idCount,
                    description = description,
                    status = Status.todo,
                    createdAt = DateTime.Now,
                    updatedAt = DateTime.Now
                });

                return true;
            }

            return false;
        }

        public TaskElement? Update(int id, string newDescription) 
        {
            if (listOfTasks is not null)
            {
                TaskElement? task = listOfTasks.Find(task => task.ID == id);
                if (task != null)
                {
                    task.description = newDescription;
                }

                return task;
            }

            return null;
        }

        public bool Delete(int id) 
        {
            if (listOfTasks is not null)
            {
                TaskElement? task = listOfTasks.Find(task => task.ID == id);
                if (task != null && listOfTasks.Count > 0)
                {
                    return listOfTasks.Remove(task);
                }

                return false;
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
                Console.WriteLine("The list is empty");
            }
        }

        public void Todo()
        {
            if (listOfTasks is not null)
            {
                if (listOfTasks.Count > 0)
                {
                    var tasksToDo = from task in listOfTasks
                                    where task.status == Status.todo
                                    select task;

                    if (tasksToDo.Count() > 0)
                    {
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
