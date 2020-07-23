using System;
using System.ComponentModel.DataAnnotations;
using CiklumTest.Models.Enums;

namespace CiklumTest.Models.ViewModels
{
    public class ToDoVM
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }

        public int UserId { get; set; }
    }
}
