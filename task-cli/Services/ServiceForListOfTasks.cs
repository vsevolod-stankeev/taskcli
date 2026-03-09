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

        private int idCount = 1;

        public void Load()
        {
            if (File.Exists(jsonName))
            {
                string list = File.ReadAllText(jsonName, Encoding.UTF8);
                listOfTasks = JsonSerializer.Deserialize<List<TaskElement>>(list);
            }
        }

        public void Save()
        {
            string jsonOfTasks = JsonSerializer.Serialize(listOfTasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonName, jsonOfTasks, Encoding.UTF8);
        }

        public void Add(string description) 
        {
            if (listOfTasks is not null)
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
        }

        public TaskElement? Update(int id, string newDescription) 
        {
            TaskElement? task = listOfTasks?.Find(task => task.ID == id);
            if (task != null)
            {
                task.description = newDescription;
            }

            return task;
        }

        public bool Delete(int id) 
        {
            TaskElement? task = listOfTasks?.Find(task => task.ID == id);
            if (task != null && listOfTasks != null)
            {
                return listOfTasks.Remove(task);
            }

            return false;
        }

        public bool MarkInProgress(int id) 
        {
            TaskElement? task = listOfTasks?.Find(task => task.ID == id);
            if (task != null)
            {
                task.status = Status.inProgress;
                return true;
            }

            return false;
        }

        public bool MarkDone(int id) 
        {
            TaskElement? task = listOfTasks?.Find(task => task.ID == id);
            if (task != null)
            {
                task.status = Status.done;
                return true;
            }

            return false;
        }

        // LINQ!
        public void List() 
        {
            if (listOfTasks is not null)
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

        public void Todo()
        {
            int count = 0;

            if (listOfTasks is not null)
            {
                foreach (TaskElement task in listOfTasks)
                {
                    if (task.status == Status.todo)
                    {
                        Console.WriteLine(task.ToString());
                        count++;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine("The list todo is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty");
            }
        }

        public void InProgress() 
        {
            int count = 0;

            if (listOfTasks is not null)
            {
                foreach (TaskElement task in listOfTasks)
                {
                    if (task.status == Status.inProgress)
                    {
                        Console.WriteLine(task.ToString());
                        count++;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine("The list in progress is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty");
            }
        }

        public void Done() 
        {
            int count = 0;

            if (listOfTasks is not null)
            {
                foreach (TaskElement task in listOfTasks)
                {
                    if (task.status == Status.done)
                    {
                        Console.WriteLine(task.ToString());
                        count++;
                    }
                }
                if (count == 0)
                {
                    Console.WriteLine("The list done is empty");
                }
            }
            else
            {
                Console.WriteLine("The list is empty");
            }
        }
    }
}
