using System;
using System.ComponentModel.DataAnnotations;
using CiklumTest.Models.DTO;

namespace CiklumTest.Models.DBModels
{
    public class ToDo : ToDoDTO
    {

        public User User { get; set; }
    }
}
