using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CiklumTest.Models.Enums;

namespace CiklumTest.Models.DBModels
{
	public class ToDo 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Header { get; set; }
		public string Description { get; set; }
        public TaskState State { get; set; }

        public int UserId { get; set; }
		public User User { get; set; }
	}

}
