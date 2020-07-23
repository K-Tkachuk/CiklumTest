using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CiklumTest.Models.DBModels
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Role { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

		public List<ToDo> Tasks { get; set; }
	}
}
