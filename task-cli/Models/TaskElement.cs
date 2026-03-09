using System;
using System.Collections.Generic;
using System.Text;

namespace TaskCli.Models
{

    public enum Status
    {
        todo,
        in_progress,
        done
    }

    internal class TaskElement
    {
        public int ID { get; }

        public string? description { get; set; }

        public Status status { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }
}