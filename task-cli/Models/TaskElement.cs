using System;
using System.Collections.Generic;
using System.Text;

namespace TaskCli.Models
{

    public enum Status
    {
        todo,
        inProgress,
        done
    }

    internal class TaskElement
    {
        public int ID { get; init; }

        public string? description { get; set; }

        public Status status { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public override string ToString()
        {
            return $"({ID}) [{status}] \"{description}\" (Created at: {createdAt:t}, {createdAt:d} | Updated at: {updatedAt:t}, {updatedAt:d})";
        }
    }
}