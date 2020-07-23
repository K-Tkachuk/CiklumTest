using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CiklumTest.Models.DBModels
{
	public static class CiklumDbInitializer
	{
		public static void Initialize(CiklumDbContext context)
		{
			context.Database.EnsureCreated();

			var userList = new List<User>()
				{
					new User(){  Name = "Luke", Email= "test@gmail.com", Password = "123456" },
					new User(){  Name = "Mike" ,Email= "test@mail.com", Password = "12345678"},
					new User(){  Name = "Joe" ,Email= "test@yandex.com", Password = "testpas"},
				};

			context.Users.AddRange(userList);

			var toDoList = new List<ToDo>()
				{
					new ToDo(){ Header="Task 1", Description="Task description 1", UserId = 1 },
					new ToDo(){ Header="Task 2", Description="Task description 2", UserId = 1 },
					new ToDo(){ Header="Task 3", Description="Task description 3", UserId = 1 },
					new ToDo(){ Header="Task 4", Description="Task description 4", UserId = 1 },
					new ToDo(){ Header="Task 5", Description="Task description 5", UserId = 2 },
					new ToDo(){ Header="Task 6", Description="Task description 6", UserId = 2 },
					new ToDo(){ Header="Task 7", Description="Task description 7", UserId = 3 },
					new ToDo(){ Header="Task 8", Description="Task description 8", UserId = 3 },
					new ToDo(){ Header="Task 9", Description="Task description 9", UserId = 3 }
				};

			context.ToDos.AddRange(toDoList);
			context.SaveChanges();
		}
	}
}