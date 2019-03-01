using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CiklumTest.Models.DTO;

namespace CiklumTest.Models.DBModels
{
	public class User : UserDTO
	{
		[Key]
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
