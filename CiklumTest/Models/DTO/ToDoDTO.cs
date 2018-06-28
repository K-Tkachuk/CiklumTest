using System;
using System.ComponentModel.DataAnnotations;

namespace CiklumTest.Models.DTO
{
    public class ToDoDTO
    {
		[Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
		public string Header { get; set; }
		public string Description { get; set; }
	}
}
