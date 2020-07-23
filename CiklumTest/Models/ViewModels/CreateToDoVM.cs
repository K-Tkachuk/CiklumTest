using System;
using CiklumTest.Models.Enums;

namespace CiklumTest.Models.ViewModels
{
    public class CreateToDoVM
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
    }
}
