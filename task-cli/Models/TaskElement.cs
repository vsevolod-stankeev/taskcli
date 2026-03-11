using System;
using System.Collections.Generic;
using System.Text;

namespace TaskCli.Models
{
    // Task statuses representing the lifecycle of a task: from creation to completion
    public enum Status
    {
        todo,        // Task is created but not yet started
        inProgress,  // Task is currently being worked on
        done         // Task is completed
    }

    // Task model. Represents a single entry in the to-do list.
    internal class TaskElement
    {
        // Unique identifier for the task. Cannot be changed after creation.
        public int ID { get; init; }

        // Text/description of the task. Can be modified. May be null.
        public string? description { get; set; }

        // Current status of the task (todo / inProgress / done)
        public Status status { get; set; }

        // Date and time when the task was created
        public DateTime createdAt { get; set; }

        // Date and time of the last modification to the task
        public DateTime updatedAt { get; set; }

        // Overrides the default string representation for console output.
        // Format: (ID) [status] "description" (Created at: time, date | Updated at: time, date)
        // Example: (1) [done] "Buy milk" (Created at: 14:30, 03/15/2024 | Updated at: 15:45, 03/15/2024)
        public override string ToString()
        {
            return $"({ID}) [{status}] \"{description}\" (Created at: {createdAt:t}, {createdAt:d} | Updated at: {updatedAt:t}, {updatedAt:d})";
        }
    }
}