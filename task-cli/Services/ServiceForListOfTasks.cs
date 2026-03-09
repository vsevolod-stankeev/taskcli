using System;
using System.Collections.Generic;
using System.Text;
using TaskCli.Models;

namespace TaskCli.Services
{
    internal class ServiceForListOfTasks
    {
        List<TaskElement> ListOfTasks = new List<TaskElement>();

        public void Load()
        {

        }

        public void Save()
        {

        }

        public void Add(string description) {

        }

        public TaskElement? Update(int ID, string new_description) {
            return null;
        }

        public bool Delete(int ID) {
            return true;
        }

        public bool MarkInProgress(int ID) {
            return true;
        }

        public bool MarkDone(int ID) {
            return true;
        }

        public void List() { 

        }

        public void Todo() {

        }

        public void InProgress() {

        }

        public void Done() {

        }
    }
}
