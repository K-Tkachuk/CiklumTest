using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CiklumTest.Models.DBModels;

namespace CiklumTest.Models.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
		public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
